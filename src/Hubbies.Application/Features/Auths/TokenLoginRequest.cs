namespace Hubbies.Application.Features.Auths;

public class TokenLoginRequest
{
    /// <example>token</example>
    public string? Token { get; init; }
}

public class TokenLoginRequestValidator : AbstractValidator<TokenLoginRequest>
{
    public TokenLoginRequestValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}
