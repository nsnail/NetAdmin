namespace NetAdmin.DataContract.DbMaps.Dependency;

/// <summary>
///     主键字段接口
/// </summary>
public interface IFieldPrimary
{
    /// <summary>
    ///     唯一编码
    /// </summary>
    long Id { get; set; }
}