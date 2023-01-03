using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     角色表
/// </summary>
[Table]
public record TbSysRole : DefaultEntity
{
    /// <summary>
    ///     角色名
    /// </summary>
    [JsonIgnore]
    public virtual string RoleName { get; set; }
}