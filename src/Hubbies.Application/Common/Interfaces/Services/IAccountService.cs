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
    /// <param name="userId">The ID of the account</param>
    /// <returns>
    /// The task result contains an <see cref="AccountDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown when the account with the given ID is not found</exception>
    Task<AccountDto> GetAccountAsync(Guid userId);

    /// <summary>
    /// Update the account information of the user
    /// </summary>
    /// <param name="request">The request object</param>
    /// <returns>
    /// The task result contains an <see cref="AccountDto"/> object.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown when the account is not found</exception>
    /// <exception cref="ValidationException">Thrown when the request is invalid</exception>
    Task<AccountDto> UpdateAccountInformationAsync(UpdateAccountInformationRequest request);

    /// <summary>
    /// Lock the account of the user with the given ID
    /// This operation will also increase the lockout count and set the lockout duration based on the lockout count
    /// </summary>
    /// <param name="userId">The ID of the account</param>
    /// <exception cref="BadRequestException">Thrown when the account is already locked</exception>
    /// <exception cref="NotFoundException">Thrown when the account with the given ID is not found</exception>
    Task LockAccountAsync(Guid userId);

    /// <summary>
    /// Unlock the account of the user with the given ID
    /// This operation will also reset the lockout count
    /// </summary>
    /// <param name="userId">The ID of the account</param>
    /// <exception cref="BadRequestException">Thrown when the account is already unlocked or not locked</exception>
    /// <exception cref="NotFoundException">Thrown when the account with the given ID is not found</exception>
    Task UnlockAccountAsync(Guid userId);
}
