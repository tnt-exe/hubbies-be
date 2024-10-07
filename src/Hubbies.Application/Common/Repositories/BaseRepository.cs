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
}
