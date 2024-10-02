namespace Hubbies.Infrastructure.Persistence.Interceptors;

public class AuditableEntityInterceptor(IUser user)
    : SaveChangesInterceptor
{
    private readonly IUser _user = user;

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateEntites(eventData.Context);

        DeleteEntites(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntites(eventData.Context);

        DeleteEntites(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntites(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker.Entries<BaseAuditableEntity>();

        foreach (var entry in entries.Where(e => e.State is EntityState.Added or EntityState.Modified))
        {
            var utcNow = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _user.Id;
                entry.Entity.CreatedAt = utcNow;
            }

            entry.Entity.UpdatedBy = _user.Id;
            entry.Entity.UpdatedAt = utcNow;
        }
    }

    public void DeleteEntites(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker.Entries<ISoftDelete>();

        foreach (var entry in entries.Where(e => e.State is EntityState.Deleted))
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsDeleted = true;
            entry.Entity.DeletedAt = DateTime.UtcNow;
        }
    }
}
