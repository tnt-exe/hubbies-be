namespace Hubbies.Application.Features.Auths;

public record CreatePasswordRequest
{
    /// <example>P@ssword7</example>
    public string? Password { get; init; }

    /// <example>P@ssword7</example>
    public string? ConfirmPassword { get; init; }
}

public class CreatePasswordValidator : AbstractValidator<CreatePasswordRequest>
{
    public CreatePasswordValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non alphanumeric character.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Confirm password must match new password");
    }
}
