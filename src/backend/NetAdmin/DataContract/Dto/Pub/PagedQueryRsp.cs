namespace NetAdmin.DataContract.Dto.Pub;

/// <summary>
///     响应：分页查询
/// </summary>
public record PagedQueryRsp<T> : IPagedInfo
    where T : DataAbstraction
{
    /// <inheritdoc />
    public int Page { get; set; }

    /// <inheritdoc />
    public int PageSize { get; set; }

    /// <summary>
    ///     数据行
    /// </summary>
    public IEnumerable<T> Rows { get; set; }

    /// <summary>
    ///     数据总条
    /// </summary>
    public long Total { get; set; }
}