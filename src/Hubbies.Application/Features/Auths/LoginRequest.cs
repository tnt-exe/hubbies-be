namespace Hubbies.Application.Features.Auths;

public record LoginRequest
{
    /// <example>monke@mail.com</example>
    public string? Email { get; init; }

    /// <example>P@ssword7</example>
    public string? Password { get; init; }
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
