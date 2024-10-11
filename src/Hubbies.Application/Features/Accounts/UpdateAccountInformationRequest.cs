namespace Hubbies.Application.Features.Accounts;

public record UpdateAccountInformationRequest
{
    /// <example>blaccmonkerox</example>
    public string? UserName { get; init; }

    /// <example>Monke</example>
    public string? FirstName { get; init; }

    /// <example>Black</example>
    public string? LastName { get; init; }

    /// <example>Q9, HCM</example>
    public string? Address { get; init; }
    public DateTimeOffset? Dob { get; init; }

    /// <example>0942782980</example>
    public string? PhoneNumber { get; init; }

    /// <example>https://monke.com/avatar.jpg</example>
    public string? Avatar { get; init; }
}

public class UpdateAccountInformationRequestValidator : AbstractValidator<UpdateAccountInformationRequest>
{
    public UpdateAccountInformationRequestValidator()
    {
        RuleFor(x => x.UserName)
            .MaximumLength(256)
            .Must(x => !x.Contains(' '))
            .WithMessage("Username must not contain any spaces");

        RuleFor(x => x.FirstName)
            .MaximumLength(256);

        RuleFor(x => x.LastName)
            .MaximumLength(256);

        RuleFor(x => x.Address)
            .MaximumLength(500);

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10}$")
            .When(x => x.PhoneNumber is not null)
            .WithMessage("Phone number must be 10 digits");

        RuleFor(x => x.Dob)
            .LessThan(DateTimeOffset.Now.ToLocalTime());

        RuleFor(x => x.Avatar)
            .MaximumLength(500);
    }
}
