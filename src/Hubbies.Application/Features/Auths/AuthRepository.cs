namespace Hubbies.Application.Features.Auths;

public class AuthRepository(
    IApplicationDbContext context,
    IServiceProvider serviceProvider,
    IIdentityService identityService,
    IFirebaseService firebaseService,
    IJwtService jwtService,
    IUser user)
    : BaseRepository(context, serviceProvider), IAuthService
{
    public async Task ChangePasswordAsync(ChangePasswordRequest request)
    {
        await ValidateAsync(request);

        await identityService.ChangePasswordAsync(user.Id!, request.CurrentPassword!, request.NewPassword!);
    }

    public async Task CreatePasswordAsync(CreatePasswordRequest request)
    {
        await ValidateAsync(request);

        await identityService.CreatePasswordAsync(user.Id!, request.Password!);
    }

    public async Task<AccessTokenResponse> LoginAsync(LoginRequest request)
    {
        await ValidateAsync(request);

        var authenticatedUser = await identityService.LoginAsync(request);

        var (accessToken, expired) = jwtService.GenerateJwtToken(authenticatedUser.user, authenticatedUser.role);

        var refreshToken = jwtService.GenerateRefreshToken(accessToken);

        return new AccessTokenResponse
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            RefreshToken = refreshToken,
            ExpiresIn = expired
        };
    }

    public async Task<AccessTokenResponse> RefreshTokenAsync(RefreshRequest refreshToken)
    {
        await ValidateAsync(refreshToken);

        var (isTokenValid, tokenValidationMessage) = jwtService.ValidateRefreshToken(refreshToken.ExpiredToken!, refreshToken.RefreshToken!);

        if (!isTokenValid)
        {
            throw new UnauthorizedAccessException(tokenValidationMessage);
        }

        var claimsPrincipal = jwtService.GetPrincipalFromExpiredToken(refreshToken.ExpiredToken!);

        var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;

        var retrievedUser = await identityService.GetUserByEmailAsync(email!);

        var (accessToken, expired) = jwtService.GenerateJwtToken(retrievedUser.user, retrievedUser.role);

        var newRefreshToken = jwtService.GenerateRefreshToken(accessToken);

        return new AccessTokenResponse
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            RefreshToken = newRefreshToken,
            ExpiresIn = expired
        };
    }

    public async Task RegisterAsync(RegisterRequest request, RegisterRole role)
    {
        await ValidateAsync(request);

        await identityService.CreateUserAsync(request, role.ToString());
    }

    public async Task RegisterWithFormAsync(RegisterFormRequest request, RegisterRole role)
    {
        await ValidateAsync(request);

        await identityService.CreateUserWithFormAsync(request, role.ToString());
    }

    public async Task<AccessTokenResponse> TokenLoginAsync(TokenLoginRequest request)
    {
        await ValidateAsync(request);

        var email = await firebaseService.GetEmailFromTokenAsync(request.Token!);

        var userByEmail = await identityService.GetUserByEmailAsync(email);

        var (accessToken, expired) = jwtService.GenerateJwtToken(userByEmail.user, userByEmail.role);

        var refreshToken = jwtService.GenerateRefreshToken(accessToken);

        return new AccessTokenResponse
        {
            AccessToken = accessToken,
            TokenType = "Bearer",
            RefreshToken = refreshToken,
            ExpiresIn = expired
        };
    }
}
