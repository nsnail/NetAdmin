namespace NetAdmin.DataContract.Dto.Pub;

/// <summary>
///     信息：分页
/// </summary>
public interface IPagedInfo
{
    /// <summary>
    ///     当前页码
    /// </summary>
    int Page { get; set; }

    /// <summary>
    ///     页容量
    /// </summary>
    int PageSize { get; set; }
}