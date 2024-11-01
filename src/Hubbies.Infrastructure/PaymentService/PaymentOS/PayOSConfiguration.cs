namespace Hubbies.Infrastructure.PaymentService.PaymentOS;

public class PayOSConfiguration
{
    public string? ClientId { get; set; }
    public string? ApiKey { get; set; }
    public string? ChecksumKey { get; set; }
    public string? CancelUrl { get; set; }
    public string? ReturnUrl { get; set; }
}
