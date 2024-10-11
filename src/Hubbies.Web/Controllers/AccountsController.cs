using Hubbies.Application.Features.Accounts;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class AccountsController(IAccountService accountService)
    : ControllerBase
{
    /// <summary>
    /// Get accounts with the given id
    /// </summary>
    /// <param name="accountId"></param>
    /// <response code="200">The account was found</response>
    /// <response code="404">The account was not found</response>
    [Authorize(Policy.Admin)]
    [HttpGet("{accountId:guid}", Name = "GetAccountById")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountByIdAsync(Guid accountId)
    {
        var account = await accountService.GetAccountAsync(accountId);

        return Ok(account);
    }

    /// <summary>
    /// Get all customer accounts
    /// </summary>
    /// <response code="200">The customer accounts list</response>
    [Authorize(Policy.Admin)]
    [HttpGet("customers", Name = "GetCustomers")]
    [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomersAsync()
    {
        var customerAccounts = await accountService.GetAccountsAsync(Role.Customer);

        return Ok(customerAccounts);
    }

    /// <summary>
    /// Get all event hosts
    /// </summary>
    /// <response code="200">The event hosts list</response>
    [Authorize(Policy.Admin)]
    [HttpGet("event-hosts", Name = "GetEventHosts")]
    [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEventHostsAsync()
    {
        var eventHostAccounts = await accountService.GetAccountsAsync(Role.EventHost);

        return Ok(eventHostAccounts);
    }

    /// <summary>
    /// Update account information
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/accounts
    ///     {
    ///         "address": "Q9, HCM",
    ///         "dob": "2002-09-25"
    ///         "phoneNumber": "0942782940"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">The account was updated</response>
    /// <response code="404">The account was not found</response>
    /// <response code="422">The request is invalid</response>
    [Authorize]
    [HttpPut(Name = "UpdateAccountInformation")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateAccountInformationAsync(UpdateAccountInformationRequest request)
    {
        var account = await accountService.UpdateAccountInformationAsync(request);

        return Ok(account);
    }

    /// <summary>
    /// Lock an account
    /// </summary>
    /// <remarks>
    /// Locking period will increase on lockout count as:
    /// 1. A day for the first lockout
    /// 2. A week for the second lockout
    /// 3. A month for the third lockout
    /// 4. Forever for the fourth lockout
    /// </remarks>
    /// <param name="accountId"></param>
    /// <response code="204">The account was locked</response>
    /// <response code="404">The account was not found</response>
    [Authorize(Policy.Admin)]
    [HttpPut("{accountId:guid}/lock", Name = "LockAccount")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LockAccountAsync(Guid accountId)
    {
        await accountService.LockAccountAsync(accountId);

        return NoContent();
    }

    /// <summary>
    /// Unlock an account
    /// </summary>
    /// <remarks>
    /// This will unlock even if account is permanently locked, caution is advised
    /// </remarks>
    /// <param name="accountId"></param>
    /// <response code="204">The account was unlocked</response>
    /// <response code="404">The account was not found</response>
    [Authorize(Policy.Admin)]
    [HttpPut("{accountId:guid}/unlock", Name = "UnlockAccount")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnlockAccountAsync(Guid accountId)
    {
        await accountService.UnlockAccountAsync(accountId);

        return NoContent();
    }
}
