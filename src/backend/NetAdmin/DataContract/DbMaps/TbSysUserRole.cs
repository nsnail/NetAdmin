using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     用户-角色映射表
/// </summary>
[Table]
public record TbSysUserRole : DefaultEntity
{
    /// <summary>
    ///     关联的角色
    /// </summary>
    [JsonIgnore]
    public virtual TbSysRole Role { get; set; }

    /// <summary>
    ///     角色id
    /// </summary>
    [JsonIgnore]
    public virtual long RoleId { get; set; }

    /// <summary>
    ///     关联的用户
    /// </summary>
    [JsonIgnore]
    public virtual TbSysUser User { get; set; }

    /// <summary>
    ///     用户id
    /// </summary>
    [JsonIgnore]
    public virtual long UserId { get; set; }
}