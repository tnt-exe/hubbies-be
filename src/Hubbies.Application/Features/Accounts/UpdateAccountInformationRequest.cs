namespace Hubbies.Application.Features.Accounts;

public record UpdateAccountInformationRequest
{
    public string? Address { get; init; }
    public DateTimeOffset? Dob { get; init; }
    public string? PhoneNumber { get; set; }
}

public class UpdateAccountInformationRequestValidator : AbstractValidator<UpdateAccountInformationRequest>
{
    public UpdateAccountInformationRequestValidator()
    {
        RuleFor(x => x.Address)
            .MaximumLength(500);

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10}$")
            .When(x => x.PhoneNumber is not null)
            .WithMessage("Phone number must be 10 digits");

        RuleFor(x => x.Dob)
            .LessThan(DateTimeOffset.Now.ToLocalTime());
    }
}
