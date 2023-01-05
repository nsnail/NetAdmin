namespace NetAdmin.DataContract.Dto.Pub;

/// <summary>
///     请求：分页查询
/// </summary>
public record PagedQueryReq<T> : QueryReq<T>, IPagedInfo
    where T : DataAbstraction, new()
{
    /// <inheritdoc />
    public int Page { get; set; }

    /// <inheritdoc />
    public int PageSize { get; set; }
}