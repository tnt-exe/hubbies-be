namespace Hubbies.Application.Features.Auths;

public record AccessTokenResponse
{
    /// <example>accessToken</example>
    public string? AccessToken { get; init; }

    /// <example>refreshToken</example>
    public string? RefreshToken { get; init; }

    /// <example>Bearer</example>
    public string? TokenType { get; init; }

    /// <example>60</example>
    public int ExpiresIn { get; init; }
}
