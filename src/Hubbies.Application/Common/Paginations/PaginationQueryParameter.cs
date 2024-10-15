namespace Hubbies.Application.Common.Paginations;

public record PaginationQueryParameter
{
    /// <example>1</example>
    public int Page { get; init; } = 1;

    /// <example>10</example>
    public int PageSize { get; init; } = 10;
}

public class PaginationQueryParameterValidator : AbstractValidator<PaginationQueryParameter>
{
    public PaginationQueryParameterValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0);
    }
}
