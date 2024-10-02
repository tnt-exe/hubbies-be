namespace Hubbies.Application.Common.Interfaces.Auth;

public class JwtSettings
{
    public string? SecretKey { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public double Expires { get; set; }
    public double RefreshExpires { get; set; }
}

public interface IJwtService
{
    /// <summary>
    /// Generate a JWT token for the given user and role
    /// </summary>
    /// <param name="user">The user to generate the token for</param>
    /// <param name="role">The role of the user</param>
    /// <returns>
    /// A tuple containing:
    /// <para>Item1 (string): The access token</para>
    /// <para>Item2 (int): The expiration time of the token in minutes</para>
    /// </returns>
    public (string accessToken, int expired) GenerateJwtToken(ApplicationUser user, string role);

    /// <summary>
    /// Generate a refresh token for the given token
    /// </summary>
    /// <param name="token">The token to generate the refresh token for</param>
    /// <returns>The refresh token</returns>
    public string GenerateRefreshToken(string token);

    /// <summary>
    /// Validate the refresh token
    /// </summary>
    /// <param name="token">The token to validate</param>
    /// <param name="refreshToken">The refresh token to validate</param>
    /// <returns>
    /// A tuple containing:
    /// <para>Item1 (bool): Whether the token is valid</para>
    /// <para>Item2 (string): The message if the token is invalid</para>
    /// </returns>
    public (bool isValid, string message) ValidateRefreshToken(string token, string refreshToken);

    /// <summary>
    /// Get the principal from the expired token
    /// </summary>
    /// <param name="token">The token to get the principal from</param>
    /// <returns>The principal from the token</returns>
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
