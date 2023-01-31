namespace NetAdmin.Domain.Dto.Dependency;

/// <summary>
///     响应：分页查询
/// </summary>
public record PagedQueryRsp<T>(int Page, int PageSize, long Total, IEnumerable<T> Rows) : IPagedInfo
    where T : DataAbstraction
{
    /// <inheritdoc cref="IPagedInfo.Page" />
    public int Page { get; init; } = Page;

    /// <inheritdoc cref="IPagedInfo.PageSize" />
    public int PageSize { get; init; } = PageSize;

    /// <summary>
    ///     数据行
    /// </summary>
    public IEnumerable<T> Rows { get; init; } = Rows;

    /// <summary>
    ///     数据总条
    /// </summary>
    public long Total { get; init; } = Total;
}