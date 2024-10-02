namespace Hubbies.Application.Features.Accounts;

public record AccountDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>Monke</example>
    public string? UserName { get; init; }

    /// <example>monke@mail.com</example>
    public string? Email { get; init; }

    /// <example>094278290</example>
    public string? PhoneNumber { get; init; }

    /// <example>2024-10-10T00:00:00+00:00</example>
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <example>False</example>
    public bool LockoutEnabled { get; set; }
}
