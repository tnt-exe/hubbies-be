namespace Hubbies.Application.Common.Paginations;

public static class PaginationExtensions
{
    /// <summary>
    /// Converts the query result to a paginated response.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TDto">The Dto type.</typeparam>
    /// <param name="query">The query to paginate.</param>
    /// <param name="queryParameter">The query parameters for pagination.</param>
    /// <param name="mapper">The mapper to map the entity to Dto.</param>
    /// <returns>A paginated response of Dto objects.</returns>
    public static async Task<PaginationResponse<TDto>> ToPaginationResponseAsync<TEntity, TDto>
        (this IQueryable<TEntity> query, PaginationQueryParameter queryParameter, IMapper mapper)
        where TEntity : BaseAuditableEntity
    {
        var data = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((queryParameter.Page - 1) * queryParameter.PageSize)
            .Take(queryParameter.PageSize)
            .ToListAsync();

        var totalCount = await query.CountAsync();

        var dtoList = mapper.Map<IEnumerable<TDto>>(data);

        return new PaginationResponse<TDto>
        {
            Page = queryParameter.Page,
            PageSize = queryParameter.PageSize,
            PageCount = dtoList.Count(),
            TotalCount = totalCount,
            Data = dtoList
        };
    }
}
