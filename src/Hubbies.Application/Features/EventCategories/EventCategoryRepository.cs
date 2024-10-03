namespace Hubbies.Application.Features.EventCategories;

public class EventCategoryRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IEventCategoryService
{
    public async Task<EventCategoryDto> CreateEventCategoryAsync(CreateEventCategoryRequest request)
    {
        await ValidateAsync(request);

        var eventCategory = Mapper.Map<EventCategory>(request);

        await Context.EventCategories.AddAsync(eventCategory);

        await Context.SaveChangesAsync();

        return Mapper.Map<EventCategoryDto>(eventCategory);
    }

    public async Task DeleteEventCategoryAsync(Guid id)
    {
        var eventCategory = await Context.EventCategories
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(EventCategory), id);

        Context.EventCategories.Remove(eventCategory);

        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<EventCategoryDto>> GetEventCategoriesAsync()
    {
        var eventCategories = await Context.EventCategories
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<EventCategoryDto>>(eventCategories);
    }

    public async Task<EventCategoryDto> GetEventCategoryAsync(Guid id)
    {
        var eventCategory = await Context.EventCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(EventCategory), id);

        return Mapper.Map<EventCategoryDto>(eventCategory);
    }

    public async Task<EventCategoryDto> UpdateEventCategoryAsync(Guid id, UpdateEventCategoryRequest request)
    {
        await ValidateAsync(request);

        var eventCategory = await Context.EventCategories
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(EventCategory), id);

        Mapper.Map(request, eventCategory);

        await Context.SaveChangesAsync();

        return Mapper.Map<EventCategoryDto>(eventCategory);
    }
}
