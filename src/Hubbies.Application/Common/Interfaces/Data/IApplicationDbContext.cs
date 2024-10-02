namespace Hubbies.Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    //dbset

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
