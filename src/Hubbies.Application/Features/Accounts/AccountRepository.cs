namespace Hubbies.Application.Features.Accounts;

public class AccountRepository(
    IApplicationDbContext context,
    IServiceProvider serviceProvider,
    IIdentityService identityService,
    IMapper mapper)
    : BaseRepository(context, mapper, serviceProvider), IAccountService
{
    public async Task<AccountDto> GetAccountAsync(Guid id)
    {
        var account = await identityService.GetAccountAsync(id);

        return Mapper.Map<AccountDto>(account);
    }

    public async Task<IEnumerable<AccountDto>> GetAccountsAsync(string role)
    {
        var accounts = await identityService.GetAccountsAsync(role);

        return Mapper.Map<IEnumerable<AccountDto>>(accounts);
    }

    public async Task LockAccountAsync(Guid userId)
    {
        await identityService.LockAccountAsync(userId);

        await Context.SaveChangesAsync();
    }

    public async Task UnlockAccountAsync(Guid userId)
    {
        await identityService.UnlockAccountAsync(userId);

        await Context.SaveChangesAsync();
    }
}
