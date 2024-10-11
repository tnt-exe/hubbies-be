namespace Hubbies.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var result = await _userManager.ChangePasswordAsync(user!, currentPassword, newPassword);

        if (!result.Succeeded)
        {
            throw new ConflictException(result.Errors);
        }
    }

    public async Task CreatePasswordAsync(string userId, string password)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (await _userManager.HasPasswordAsync(user!))
        {
            throw new BadRequestException("Password already created");
        }

        var result = await _userManager.AddPasswordAsync(user!, password);

        if (!result.Succeeded)
        {
            throw new ConflictException(result.Errors);
        }
    }

    public async Task<(bool, string roleId)> CreateRoleAsync(string roleName)
    {
        var roleExist = await _roleManager.FindByNameAsync(roleName);

        if (roleExist != null)
        {
            return (false, roleExist.Id.ToString());
        }

        var role = new IdentityRole<Guid>(roleName);

        var result = await _roleManager.CreateAsync(role);

        return (result.Succeeded, role.Id.ToString());
    }

    public async Task<string> CreateUserAsync(string email, string password, string role)
    {
        var user = new ApplicationUser
        {
            UserName = email.Split('@')[0],
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            throw new ConflictException(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, role);

        return user.Id.ToString();
    }

    public async Task<ApplicationUser> GetAccountAsync(Guid userId)
    {
        return await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new NotFoundException(nameof(ApplicationUser), userId);
    }

    public async Task<IEnumerable<ApplicationUser>> GetAccountsAsync(string role)
    {
        return await _userManager.GetUsersInRoleAsync(role);
    }

    public async Task<(ApplicationUser user, string role)> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            await _userManager.CreateAsync(new ApplicationUser
            {
                UserName = email.Split('@')[0],
                Email = email
            });

            user = await _userManager.FindByEmailAsync(email);

            await _userManager.AddToRoleAsync(user!, Role.Customer);
        }

        // check if user is locked
        await CheckAccountLockoutAsync(user!);

        var role = await _userManager.GetRolesAsync(user!)
            .ContinueWith(task => task.Result.FirstOrDefault());

        return (user!, role!);
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task LockAccountAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new NotFoundException(nameof(ApplicationUser), userId);

        if (await _userManager.IsLockedOutAsync(user!))
        {
            throw new BadRequestException("Account is already locked");
        }

        user.LockoutCount++;
        var lockoutDuration = user.LockoutCount switch
        {
            1 => DateTimeOffset.UtcNow.AddDays(1),
            2 => DateTimeOffset.UtcNow.AddDays(7),
            3 => DateTimeOffset.UtcNow.AddDays(30),
            4 => DateTimeOffset.MaxValue,
            _ => DateTimeOffset.MaxValue,
        };

        await _userManager.SetLockoutEndDateAsync(user!, lockoutDuration);
    }

    public async Task<(ApplicationUser user, string role)> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email)
            ?? throw new UnauthorizedAccessException($"Account with identifier '{email}' not found");

        // check if user is locked
        await CheckAccountLockoutAsync(user);

        var result = await _userManager.CheckPasswordAsync(user, password);

        if (!result)
        {
            // if user lockout is enabled
            // increment access failed count
            if (await _userManager.GetLockoutEnabledAsync(user))
            {
                // on failed login attempt, increment access failed count
                await _userManager.AccessFailedAsync(user);

                throw new UnauthorizedAccessException("Invalid password");
            }

            throw new UnauthorizedAccessException("Invalid password");
        }

        // reset access failed count
        await _userManager.ResetAccessFailedCountAsync(user);

        var role = await _userManager.GetRolesAsync(user)
            .ContinueWith(task => task.Result.FirstOrDefault());

        return (user!, role!);
    }

    private async Task CheckAccountLockoutAsync(ApplicationUser user)
    {
        if (await _userManager.IsLockedOutAsync(user))
        {
            var lockedOutEnd = await _userManager.GetLockoutEndDateAsync(user);

            throw new UnauthorizedAccessException($"Account is locked out until {lockedOutEnd.Value.LocalDateTime}");
        }
    }

    public async Task UnlockAccountAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())
            ?? throw new NotFoundException(nameof(ApplicationUser), userId);

        if (!await _userManager.IsLockedOutAsync(user!))
        {
            throw new BadRequestException("Account is not locked");
        }

        user.LockoutCount = 0;

        await _userManager.SetLockoutEndDateAsync(user!, null);
    }
}
