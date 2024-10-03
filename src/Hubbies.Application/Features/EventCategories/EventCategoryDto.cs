namespace Hubbies.Application.Features.EventCategories;

public record EventCategoryDto
{
    public Guid Id { get; init; }
    public string? Address { get; init; }
    public string? Name { get; init; }
}
