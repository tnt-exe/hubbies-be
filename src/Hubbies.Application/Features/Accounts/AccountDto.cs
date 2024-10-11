namespace Hubbies.Application.Features.Accounts;

public record AccountDto
{
    public Guid Id { get; init; }

    /// <example>blaccmonkerox</example>
    public string? UserName { get; init; }

    /// <example>Monke</example>
    public string? FirstName { get; init; }

    /// <example>Black</example>
    public string? LastName { get; init; }

    /// <example>monke@mail.com</example>
    public string? Email { get; init; }

    /// <example>0942782980</example>
    public string? PhoneNumber { get; init; }

    /// <example>Q9, HCM</example>
    public string? Address { get; init; }

    /// <example>https://monke.com/avatar.jpg</example>
    public string? Avatar { get; init; }

    public DateTimeOffset? Dob { get; init; }
    public DateTimeOffset? LockoutEnd { get; init; }
    public bool IsLocked => LockoutEnd > DateTimeOffset.Now;
    public int LockoutCount { get; init; }
}
