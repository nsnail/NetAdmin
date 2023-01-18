using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     角色-接口映射表
/// </summary>
[Table]
public record TbSysRoleApi : ImmutableEntity
{
    /// <summary>
    ///     关联的接口
    /// </summary>
    [JsonIgnore]
    public TbSysApi Api { get; init; }

    /// <summary>
    ///     接口id
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    public virtual string ApiId { get; init; }

    /// <summary>
    ///     关联的角色
    /// </summary>
    [JsonIgnore]
    public TbSysRole Role { get; init; }

    /// <summary>
    ///     角色id
    /// </summary>
    [JsonIgnore]
    public virtual long RoleId { get; init; }
}