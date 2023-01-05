namespace NetAdmin.DataContract.DbMaps.Dependency;

/// <summary>
///     更新字段接口
/// </summary>
public interface IFieldUpdate
{
    /// <summary>
    ///     修改时间
    /// </summary>
    DateTime? ModifiedTime { get; set; }

    /// <summary>
    ///     修改者Id
    /// </summary>
    long? ModifiedUserId { get; set; }

    /// <summary>
    ///     修改者
    /// </summary>
    string ModifiedUserName { get; set; }

    /// <summary>
    ///     数据版本
    /// </summary>
    long Version { get; set; }
}