using Hubbies.Application.Payments.ZaloPay;
using Hubbies.Infrastructure.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hubbies.Infrastructure.PaymentService.ZaloPay;

public class ZaloPayService(IOptions<ZaloPayConfiguration> configuration)
    : IZaloPayService
{
    private readonly ZaloPayConfiguration _configuration = configuration.Value;

    public async Task<(string? paymentUrl, string appTransId)> GetPaymentUrlAsync(
        long amount,
        string description)
    {
        var appId = _configuration.AppId!;
        var appUser = _configuration.AppUser!;
        var paymentUrl = _configuration.PaymentUrl!;
        var callbackUrl = _configuration.CallbackUrl!;
        var redirectUrl = _configuration.RedirectUrl!;
        var key1 = _configuration.Key1!;

        var embedData = new { redirecturl = redirectUrl };
        var items = new[] { new { } };
        var appTime = ((long)(DateTime.Now.ToUniversalTime() - DateTime.UnixEpoch).TotalMilliseconds).ToString();
        var appTransId = DateTime.Now.ToString("yyMMdd") + "_" + new Random().Next(900000000);

        var paramData = new Dictionary<string, string>()
        {
            { "app_id", appId },
            { "app_user", appUser },
            { "app_time", appTime },
            { "amount", amount.ToString() },
            { "app_trans_id", appTransId },
            { "embed_data", JsonConvert.SerializeObject(embedData) },
            { "item", JsonConvert.SerializeObject(items) },
            { "description", description },
            { "bank_code", "" },
            { "callback_url", callbackUrl}
        };

        var macData = paramData["app_id"] +
                "|" + paramData["app_trans_id"] +
                "|" + paramData["app_user"] +
                "|" + paramData["amount"] +
                "|" + paramData["app_time"] +
                "|" + paramData["embed_data"] +
                "|" + paramData["item"];

        paramData.Add("mac", HashHelper.HmacSHA256(macData, key1));

        var result = await HttpHelper.PostFormAsync(paymentUrl, paramData);

        if (result["return_code"].ToString() == "1")
        {
            return (result["order_url"].ToString(), appTransId);
        }

        return (null, appTransId);
    }

    public async Task<OrderStatus> VerifyPaymentAsync(string appTransId)
    {
        var appId = _configuration.AppId!;
        var key1 = _configuration.Key1!;
        var queryUrl = _configuration.QueryUrl!;

        var queryParams = new Dictionary<string, string>
        {
            { "app_id", appId },
            { "app_trans_id", appTransId }
        };

        var macData = appId +
                "|" + appTransId +
                "|" + key1;

        queryParams.Add("mac", HashHelper.HmacSHA256(macData, _configuration.Key1!));

        var result = await HttpHelper.PostFormAsync(queryUrl, queryParams);

        // return code 1: success
        // return code 2: failed
        // return code 3: pending or waiting for user input
        if (result["return_code"].ToString() == "1")
        {
            return OrderStatus.Finished;
        }

        if (result["return_code"].ToString() == "2")
        {
            return OrderStatus.Canceled;
        }

        return OrderStatus.Pending;
    }

    public Dictionary<string, object> CallbackPayment(dynamic callbackData)
    {
        var result = new Dictionary<string, object>();

        var key2 = _configuration.Key2!;

        JObject callbackDataObj = JObject.Parse(callbackData.ToString());

        var data = callbackDataObj["data"]!.ToString();
        var dataMac = callbackDataObj["mac"]!.ToString();

        var mac = HashHelper.HmacSHA256(data, key2);

        if (!dataMac.Equals(mac))
        {
            result["return_code"] = -1;
            result["return_message"] = "Invalid mac data";
        }
        else
        {
            result["return_code"] = 1;
            result["return_message"] = "Success";
        }

        return result;
    }
}
