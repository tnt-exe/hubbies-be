namespace Hubbies.Application.Features.Accounts;

public record AccountDto
{
    public Guid Id { get; init; }

    /// <example>Monke</example>
    public string? UserName { get; init; }

    /// <example>monke@mail.com</example>
    public string? Email { get; init; }

    /// <example>094278290</example>
    public string? PhoneNumber { get; init; }

    /// <example>Q9, HCM</example>
    public string? Address { get; set; }

    public DateTimeOffset? Dob { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int LockoutCount { get; set; }
}
