using System.ComponentModel.DataAnnotations;

namespace NetAdmin.DataContract.Dto.Pub;

/// <summary>
///     请求：分页查询
/// </summary>
public record PagedQueryReq<T> : QueryReq<T>, IPagedInfo
    where T : DataAbstraction, new()
{
    /// <inheritdoc cref="IPagedInfo.Page" />
    [Range(1, Numbers.QUERY_MAX_PAGENO)]
    public int Page { get; init; }

    /// <inheritdoc cref="IPagedInfo.PageSize" />
    [Range(1, Numbers.QUERY_MAX_PAGESIZE)]
    public int PageSize { get; init; }
}