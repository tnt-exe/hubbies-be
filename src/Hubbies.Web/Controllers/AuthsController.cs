using Hubbies.Application.Features.Auths;
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
    [HttpPost("customer/register", Name = "CustomerRegister")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RegisterCustomerAsync(RegisterRequest request)
    {
        await authService.RegisterCustomerAsync(request);

        return Ok();
    }

    [HttpPost("event-host/register", Name = "EventHostRegister")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RegisterEventHostAsync(RegisterRequest request)
    {
        await authService.RegisterEventHostAsync(request);

        return Ok();
    }

    [HttpPost("login", Name = "Login")]
    [ProducesResponseType(typeof(AccessTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var response = await authService.LoginAsync(request);

        return Ok(response);
    }

    [HttpPost("token-login", Name = "TokenLogin")]
    [ProducesResponseType(typeof(AccessTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> TokenLoginAsync(TokenLoginRequest request)
    {
        var response = await authService.TokenLoginAsync(request);

        return Ok(response);
    }

    [HttpPost("refresh-token", Name = "RefreshToken")]
    [ProducesResponseType(typeof(AccessTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RefreshTokenAsync(RefreshRequest request)
    {
        var response = await authService.RefreshTokenAsync(request);

        return Ok(response);
    }

    [Authorize]
    [HttpPut("change-password", Name = "ChangePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest request)
    {
        await authService.ChangePasswordAsync(request);

        return Ok();
    }

    [Authorize]
    [HttpPut("create-password", Name = "CreatePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreatePasswordAsync(CreatePasswordRequest request)
    {
        await authService.CreatePasswordAsync(request);

        return Ok();
    }
}
