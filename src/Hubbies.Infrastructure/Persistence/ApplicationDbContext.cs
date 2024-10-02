namespace Hubbies.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options), IApplicationDbContext
{
    //dbset

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region identity
        builder.Entity<ApplicationUser>(entity => entity.ToTable("Account"));
        builder.Entity<IdentityRole<Guid>>(entity => entity.ToTable("Role"));
        builder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable("AccountRole"));

        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        #endregion

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
