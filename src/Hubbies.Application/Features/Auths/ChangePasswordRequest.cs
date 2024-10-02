namespace Hubbies.Application.Features.Auths;

public record ChangePasswordRequest
{
    /// <example>P@ssword7</example>
    public string? CurrentPassword { get; init; }

    /// <example>P@ssword8</example>
    public string? NewPassword { get; init; }

    /// <example>P@ssword8</example>
    public string? ConfirmPassword { get; init; }
}

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(6)
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non alphanumeric character.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword)
            .WithMessage("Confirm password must match new password");
    }
}
