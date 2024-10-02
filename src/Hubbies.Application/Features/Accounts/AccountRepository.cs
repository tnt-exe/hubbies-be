namespace Hubbies.Application.Features.Accounts;

public class AccountRepository(
    IApplicationDbContext context,
    IServiceProvider serviceProvider,
    IIdentityService identityService,
    IMapper mapper,
    IUser user)
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

    public async Task LockAccountAsync(Guid customerId)
    {
        // var customerAccount = await Context.Customers
        //     .Include(c => c.IdentityUser)
        //     .FirstOrDefaultAsync(c => c.Id == customerId)
        //     ?? throw new NotFoundException(nameof(Customer), customerId);

        // // increase lockout count
        // customerAccount.LockoutCount++;
        // var lockoutDuration = customerAccount.LockoutCount switch
        // {
        //     1 => DateTimeOffset.UtcNow.AddDays(1),
        //     2 => DateTimeOffset.UtcNow.AddDays(7),
        //     3 => DateTimeOffset.UtcNow.AddDays(30),
        //     4 => DateTimeOffset.MaxValue,
        //     _ => DateTimeOffset.MaxValue,
        // };
        // await identityService.LockAccountAsync(customerAccount.IdentityUser!.Id, lockoutDuration);

        // await Context.SaveChangesAsync();
    }

    public async Task UnlockAccountAsync(Guid customerId)
    {
        // var customerAccount = await Context.Customers
        //     .Include(c => c.IdentityUser)
        //     .FirstOrDefaultAsync(c => c.Id == customerId)
        //     ?? throw new NotFoundException(nameof(Customer), customerId);

        // await identityService.UnlockAccountAsync(customerAccount.IdentityUser!.Id);

        // // reset lockout count
        // customerAccount.LockoutCount = 0;

        // await Context.SaveChangesAsync();
    }
}
