using Hubbies.Application.Features.Accounts;

namespace Hubbies.Application.Common.Interfaces.Services;

public interface IAccountService
{
    /// <summary>
    /// Get all accounts with the given role
    /// </summary>
    /// <param name="role">The role of the accounts</param>
    /// <returns>
    /// The task result contains a collection of <see cref="AccountDto"/> objects.
    /// </returns>
    Task<IEnumerable<AccountDto>> GetAccountsAsync(string role);

    /// <summary>
    /// Get the account with the given ID
    /// </summary>
    /// <param name="id">The ID of the account</param>
    /// <returns>
    /// The task result contains an <see cref="AccountDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown when the account with the given ID is not found</exception>
    Task<AccountDto> GetAccountAsync(Guid id);

    /// <summary>
    /// Lock the account of the user with the given ID
    /// This operation will also increase the lockout count and set the lockout duration based on the lockout count
    /// </summary>
    /// <param name="userId">The ID of the customer</param>
    /// <exception cref="BadRequestException">Thrown when the account is already locked</exception>
    /// <exception cref="NotFoundException">Thrown when the customer with the given ID is not found</exception>
    Task LockAccountAsync(Guid userId);

    /// <summary>
    /// Unlock the account of the user with the given ID
    /// This operation will also reset the lockout count
    /// </summary>
    /// <param name="userId">The ID of the customer</param>
    /// <exception cref="BadRequestException">Thrown when the account is already unlocked or not locked</exception>
    /// <exception cref="NotFoundException">Thrown when the customer with the given ID is not found</exception>
    Task UnlockAccountAsync(Guid userId);
}
