using Hubbies.Application.Features.Auths;
using Hubbies.Domain.Enums;
using LoginRequest = Hubbies.Application.Features.Auths.LoginRequest;
using RegisterRequest = Hubbies.Application.Features.Auths.RegisterRequest;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class AuthsController(IAuthService authService)
    : ControllerBase
{
    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="request"></param>
    /// <param name="role"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/auths/register
    ///     {
    ///         "email": "monke@mail.com",
    ///         "password": "P@ssword7"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">The user was registered successfully</response>
    /// <response code="409">The user is already registered</response>
    /// <response code="422">The request is invalid</response>
    [HttpPost("register", Name = "Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RegisterAsync(
        [FromQuery] RegisterRole role,
        RegisterRequest request)
    {
        await authService.RegisterAsync(request, role);

        return Ok();
    }

    /// <summary>
    /// Register a new user with full details form
    /// </summary>
    /// <param name="request"></param>
    /// <param name="role"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/auths/register/details
    ///     {
    ///         "email": "monke@mail.com",
    ///         "password": "P@ssword7",
    ///         "firstName": "Monke",
    ///         "lastName": "Black",
    ///         "phoneNumber": "0942782980",
    ///         "address": "Q9, HCM",
    ///         "dob": "2002-09-25"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">The user was registered successfully</response>
    /// <response code="409">The user is already registered</response>
    /// <response code="422">The request is invalid</response>
    [HttpPost("register/details", Name = "RegisterDetailsForm")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RegisterWithFormAsync(
        [FromQuery] RegisterRole role,
        RegisterFormRequest request)
    {
        await authService.RegisterWithFormAsync(request, role);

        return Ok();
    }

    /// <summary>
    /// Login with the given email and password
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    /// For every 5 failed login attempts, the user will be locked for 30 minutes
    /// 
    ///     POST /api/auths/login
    ///     {
    ///         "email": "monke@mail.com",
    ///         "password": "P@ssword7"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the access token</response>
    /// <response code="422">The request is invalid</response>
    [HttpPost("login", Name = "Login")]
    [ProducesResponseType(typeof(AccessTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var response = await authService.LoginAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Login with Firebase token
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Incase user is not registered, the user will be registered as a customer
    /// 
    /// Sample request:
    /// 
    ///     POST /api/auths/token-login
    ///     {
    ///         "token": "token"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the access token</response>
    /// <response code="422">The request is invalid</response>
    [HttpPost("token-login", Name = "TokenLogin")]
    [ProducesResponseType(typeof(AccessTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> TokenLoginAsync(TokenLoginRequest request)
    {
        var response = await authService.TokenLoginAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Refresh the access token
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/auths/refresh-token
    ///     {
    ///         "expiredToken": "expiredToken",
    ///         "refreshToken": "refreshToken"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the access token</response>
    /// <response code="401">The token is invalid</response>
    /// <response code="422">The request is invalid</response>
    [HttpPost("refresh-token", Name = "RefreshToken")]
    [ProducesResponseType(typeof(AccessTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RefreshTokenAsync(RefreshRequest request)
    {
        var response = await authService.RefreshTokenAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Change the password of the user
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/auths/change-password
    ///     {
    ///         "currentPassword": "P@ssword7",
    ///         "newPassword": "P@ssword8",
    ///         "confirmPassword": "P@ssword8"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">The password was changed successfully</response>
    /// <response code="409">The operation failed</response>
    /// <response code="422">The request is invalid</response>
    [Authorize]
    [HttpPut("change-password", Name = "ChangePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest request)
    {
        await authService.ChangePasswordAsync(request);

        return Ok();
    }

    /// <summary>
    /// Create a new password for the user if user haven't have one
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/auths/create-password
    ///     {
    ///         "password": "P@ssword7",
    ///         "confirmPassword": "P@ssword7"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">The password was created successfully</response>
    /// <response code="409">The operation failed</response>
    /// <response code="422">The request is invalid</response>
    [Authorize]
    [HttpPut("create-password", Name = "CreatePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreatePasswordAsync(CreatePasswordRequest request)
    {
        await authService.CreatePasswordAsync(request);

        return Ok();
    }
}
