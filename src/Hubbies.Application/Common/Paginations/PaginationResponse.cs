namespace Hubbies.Application.Common.Paginations;

public record PaginationResponse<TDto>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int PageCount { get; init; }
    public int TotalCount { get; init; }

    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    public IEnumerable<TDto> Data { get; init; } = default!;
}
