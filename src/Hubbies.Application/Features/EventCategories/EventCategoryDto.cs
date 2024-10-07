namespace Hubbies.Application.Features.EventCategories;

public record EventCategoryDto
{
    public Guid Id { get; init; }

    /// <example>HCM</example>
    public string? Address { get; init; }

    /// <example>Cofi</example>
    public string? Name { get; init; }
}
