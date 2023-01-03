namespace NetAdmin.DataContract.DbMaps.Dependency;

/// <summary>
///     排序字段接口
/// </summary>
public interface IFieldSort
{
    /// <summary>
    ///     排序字段
    /// </summary>
    int Sort { get; set; }
}