using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hubbies.Infrastructure.PaymentService.PaymentOS;

public class SignatureControlTest
{
    private static string ConvertObjToQueryStr(JObject obj)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (JProperty item in obj.Properties())
        {
            string name = item.Name;
            JToken value = item.Value;
            string value2 = "";
            if (value.Type == JTokenType.Date)
            {
                DateTime value3 = (DateTime)value;
                value2 = DateTime.SpecifyKind(value3, DateTimeKind.Unspecified).ToString("yyyy-MM-ddTHH:mm:ss") + "+07:00";
            }
            else if (value.Type == JTokenType.String)
            {
                value2 = value.Value<string>()!;
            }
            else if (value.Type == JTokenType.Array)
            {
                value2 = "[";
                bool flag = false;
                foreach (JObject item2 in (IEnumerable<JToken>)value)
                {
                    if (flag)
                    {
                        value2 += ",";
                    }

                    value2 += SortObjDataByKey(item2).ToString(Formatting.None);
                    flag = true;
                }

                value2 += "]";
            }
            else if (value.Type != JTokenType.Null)
            {
                value2 = value.ToString();
            }

            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append('&');
            }

            stringBuilder.Append(name).Append('=').Append(value2);
        }

        return stringBuilder.ToString();
    }

    private static JObject SortObjDataByKey(JObject obj)
    {
        if (obj.Type != JTokenType.Object)
        {
            return obj;
        }

        return new JObject(from x in obj.Properties()
                           orderby x.Name
                           select x);
    }

    private static string GenerateHmacSHA256(string dataStr, string key)
    {
        using HMACSHA256 hMACSHA = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        byte[] array = hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(dataStr));
        StringBuilder stringBuilder = new StringBuilder();
        byte[] array2 = array;
        foreach (byte b in array2)
        {
            stringBuilder.Append(b.ToString("x2"));
        }

        return stringBuilder.ToString();
    }

    public static string CreateSignatureFromObj(JObject data, string key)
    {
        return GenerateHmacSHA256(ConvertObjToQueryStr(SortObjDataByKey(data)), key);
    }
}
