namespace Hubbies.Infrastructure.Auth;

public class JwtService : IJwtService
{
    private readonly JwtSettings _settings;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public JwtService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
        _tokenHandler = new();
    }

    public (string accessToken, int expired) GenerateJwtToken(ApplicationUser user, string role)
    {
        var signingCredential = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.SecretKey!)), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Role, role)
            ]),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(_settings.Expires),
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            SigningCredentials = signingCredential
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);

        return (_tokenHandler.WriteToken(token), (int)_settings.Expires);
    }

    public string GenerateRefreshToken(string token)
    {
        var refreshExpires = DateTimeOffset.UtcNow.AddMinutes(_settings.RefreshExpires)
            .ToUnixTimeSeconds().ToString();

        var hash = SHA256.HashData(Encoding.ASCII.GetBytes(token + refreshExpires));

        var tokenPart = Convert.ToBase64String(hash);

        return $"{tokenPart}.{refreshExpires}";
    }

    public (bool, string) ValidateRefreshToken(string token, string refreshToken)
    {
        var parts = refreshToken.Split('.');

        if (parts.Length != 2)
        {
            return (false, "Invalid refresh token, refresh token must contain two parts");
        }

        if (long.TryParse(parts[1], out long expires) &&
            DateTimeOffset.FromUnixTimeSeconds(expires) < DateTimeOffset.UtcNow)
        {
            return (false, "Invalid refresh token, refresh token has expired");
        }

        var hash = SHA256.HashData(Encoding.ASCII.GetBytes(token + parts[1]));

        var tokenPart = Convert.ToBase64String(hash);

        return (tokenPart == parts[0], "Invalid refresh token, tokens does not match");
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _settings.Issuer,
            ValidAudience = _settings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.SecretKey!))
        };

        var principal = _tokenHandler.ValidateToken(token, TokenValidationParameters, out SecurityToken securityToken);

        var jwtToken = securityToken as JwtSecurityToken;

        if (jwtToken is null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}
