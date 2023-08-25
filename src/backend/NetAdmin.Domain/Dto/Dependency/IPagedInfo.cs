namespace NetAdmin.Domain.Dto.Dependency;

/// <summary>
///     信息：分页
/// </summary>
public interface IPagedInfo
{
    /// <summary>
    ///     当前页码
    /// </summary>
    int Page { get; init; }

    /// <summary>
    ///     页容量
    /// </summary>
    int PageSize { get; init; }
}