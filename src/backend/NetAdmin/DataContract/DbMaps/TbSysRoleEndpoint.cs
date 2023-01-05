using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     角色-端点映射表
/// </summary>
[Table]
public record TbSysRoleEndpoint : DefaultEntity
{
    /// <summary>
    ///     端点路径
    /// </summary>
    [JsonIgnore]
    public virtual string Path { get; set; }

    /// <summary>
    ///     角色id
    /// </summary>
    [JsonIgnore]
    public virtual long RoleId { get; set; }
}