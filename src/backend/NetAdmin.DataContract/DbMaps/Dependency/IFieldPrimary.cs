namespace NetAdmin.DataContract.DbMaps.Dependency;

/// <summary>
///     主键字段接口
/// </summary>
public interface IFieldPrimary<T>
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    T Id { get; set; }
}