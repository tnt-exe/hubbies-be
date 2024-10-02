namespace Hubbies.Application.Common.Repositories;

public class BaseRepository
{
    protected readonly IApplicationDbContext Context;
    protected readonly IServiceProvider ServiceProvider;
    protected readonly IMapper Mapper = null!;

    protected BaseRepository(
        IApplicationDbContext context,
        IMapper mapper,
        IServiceProvider serviceProvider)
    {
        Context = context;
        Mapper = mapper;
        ServiceProvider = serviceProvider;
    }

    protected BaseRepository(
        IApplicationDbContext context,
        IServiceProvider serviceProvider)
    {
        Context = context;
        ServiceProvider = serviceProvider;
    }

    /// <summary>
    /// Validate a DTO using its validator.
    /// </summary>
    /// <typeparam name="T">Type of DTO class, this class must have validator associate with</typeparam>
    /// <param name="dto">DTO object</param>
    /// <exception cref="ValidationException">
    /// Thrown when one or more validation failures have occurred.
    /// </exception>
    protected async Task ValidateAsync<T>(T dto)
    {
        var validator = ServiceProvider.GetRequiredService<IValidator<T>>();

        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }

    /// <summary>
    /// Validate a list of DTOs using their validators.
    /// </summary>
    /// <typeparam name="T">Type of DTO class, this class must have validator associate with</typeparam>
    /// <param name="dtos">List of DTOs</param>
    /// <exception cref="ValidationException">
    /// Thrown when one or more validation failures have occurred.
    /// Exception thrown as a dictionary with key is the name of the dto class and value is the array of error messages.
    /// </exception>
    protected async Task ValidateListAsync<T>(IEnumerable<T> dtos)
    {
        var validator = ServiceProvider.GetRequiredService<IValidator<T>>();

        var errorDict = new Dictionary<string, string[]>();

        // index for error key, which is the name of the dto class
        int index = 0;

        foreach (var dto in dtos)
        {
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                // key is the name of the dto class
                string key = $"{typeof(T).Name}[{index}]";

                // value is the array of error messages
                // adding by dto key with index
                errorDict[key] = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
            }

            index++;
        }

        if (errorDict.Any())
        {
            throw new ValidationException(errorDict);
        }
    }

    protected static async Task<List<T>> GetEntitiesByIdsAsync<T>(IEnumerable<Guid> ids, DbSet<T> dbSet)
        where T : class, IBaseEntity
    {
        return await dbSet
            .Where(e => ids.Contains(e.Id))
            .ToListAsync();
    }
}
