namespace Hubbies.Application.Common.Interfaces.Auth;

public class FirebaseSettings
{
    public string? ProjectId { get; set; }
    public string? ServiceKey { get; set; }
}

public interface IFirebaseService
{
    /// <summary>
    /// Get the email from the firebase token
    /// </summary>
    /// <param name="token">The token to get the email from</param>
    /// <returns>The email from the token</returns>
    Task<string> GetEmailFromTokenAsync(string token);
}
