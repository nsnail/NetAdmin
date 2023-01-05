using FreeSql.Internal.Model;

namespace NetAdmin.DataContract.Dto.Pub;

/// <summary>
///     请求：查询
/// </summary>
public record QueryReq<T> : DataAbstraction
    where T : DataAbstraction, new()
{
    /// <summary>
    ///     动态查询条件
    /// </summary>
    public DynamicFilterInfo DynamicFilter { get; set; }

    /// <summary>
    ///     查询条件
    /// </summary>
    public T Filter { get; set; }
}