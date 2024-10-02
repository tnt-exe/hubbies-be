namespace Hubbies.Application.Features.Auths;

public record RefreshRequest
{
    /// <example>expiredToken</example>
    public string? ExpiredToken { get; init; }

    /// <example>refreshToken</example>
    public string? RefreshToken { get; init; }
}

public class RefreshRequestValidator : AbstractValidator<RefreshRequest>
{
    public RefreshRequestValidator()
    {
        RuleFor(x => x.ExpiredToken)
            .NotEmpty();

        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}
