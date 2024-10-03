using Hubbies.Application.Features.Auths;

namespace Hubbies.Application.Common.Interfaces.Auth;

public interface IAuthService
{
    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="request">The request to register</param>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    /// <exception cref="ConflictException">Thrown when the user is already registered</exception>
    public Task RegisterCustomerAsync(RegisterRequest request);

    /// <summary>
    /// Register a new event host
    /// </summary>
    /// <param name="request">The request to register</param>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    /// <exception cref="ConflictException">Thrown when the user is already registered</exception>
    public Task RegisterEventHostAsync(RegisterRequest request);

    /// <summary>
    /// Login with pregiven token
    /// </summary>
    /// <param name="request">The request to login</param>
    /// <returns>The response containing the access token</returns>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    public Task<AccessTokenResponse> TokenLoginAsync(TokenLoginRequest request);

    /// <summary>
    /// Login with the given email and password
    /// </summary>
    /// <param name="request">The request to login</param>
    /// <returns>The response containing the access token</returns>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    public Task<AccessTokenResponse> LoginAsync(LoginRequest request);

    /// <summary>
    /// Refresh the access token
    /// </summary>
    /// <param name="refreshToken">The request to refresh the token</param>
    /// <returns>The response containing the access token</returns>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when the token is invalid</exception>
    public Task<AccessTokenResponse> RefreshTokenAsync(RefreshRequest refreshToken);

    /// <summary>
    /// Change the password of the user
    /// </summary>
    /// <param name="request">The request to change the password</param>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    /// <exception cref="ConflictException">Thrown when the operation failed</exception>
    public Task ChangePasswordAsync(ChangePasswordRequest request);

    /// <summary>
    /// Create a new password for the user
    /// </summary>
    /// <param name="request">The request to create the password</param>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    /// <exception cref="ConflictException">Thrown when the operation failed</exception>
    /// <exception cref="BadRequestException">Thrown when the password is already created</exception>
    public Task CreatePasswordAsync(CreatePasswordRequest request);
}
