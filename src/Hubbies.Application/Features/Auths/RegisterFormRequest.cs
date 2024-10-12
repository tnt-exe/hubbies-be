namespace Hubbies.Application.Features.Auths;

public record RegisterFormRequest : RegisterRequest
{
    /// <example>Monke</example>
    public string? FirstName { get; init; }

    /// <example>Black</example>
    public string? LastName { get; init; }

    /// <example>0942782980</example>
    public string? PhoneNumber { get; init; }

    /// <example>Q9, HCM</example>
    public string? Address { get; init; }

    public DateTimeOffset? Dob { get; set; }
}

public class RegisterFormRequestValidator : AbstractValidator<RegisterFormRequest>
{
    public RegisterFormRequestValidator()
    {
        Include(new RegisterRequestValidator());

        RuleFor(x => x.FirstName)
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .MaximumLength(50);

        RuleFor(x => x.Address)
            .MaximumLength(500);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits");

        RuleFor(x => x.Dob)
            .LessThan(DateTimeOffset.Now.ToLocalTime());
    }
}
