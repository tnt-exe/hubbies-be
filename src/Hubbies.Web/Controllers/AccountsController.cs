using Hubbies.Application.Features.Accounts;

namespace Hubbies.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class AccountsController(IAccountService accountService)
    : ControllerBase
{
    // [Authorize(Policy.Admin)]
    [HttpGet("{accountId:guid}", Name = "GetAccountById")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountByIdAsync(Guid accountId)
    {
        var account = await accountService.GetAccountAsync(accountId);

        return Ok(account);
    }

    // [Authorize(Policy.Admin)]
    [HttpGet("event-hosts", Name = "GetEventHosts")]
    [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEventHostsAsync()
    {
        var eventHostAccounts = await accountService.GetAccountsAsync(Role.EventHost);

        return Ok(eventHostAccounts);
    }

    [Authorize(Policy.Admin)]
    [HttpPut("customer/{customerId:guid}/lock", Name = "LockCustomer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LockCustomerAsync(Guid customerId)
    {
        await accountService.LockAccountAsync(customerId);

        return NoContent();
    }

    // [Authorize(Policy.Admin)]
    [HttpPut("customer/{customerId:guid}/unlock", Name = "UnlockCustomer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnlockCustomerAsync(Guid customerId)
    {
        await accountService.UnlockAccountAsync(customerId);

        return NoContent();
    }
}
