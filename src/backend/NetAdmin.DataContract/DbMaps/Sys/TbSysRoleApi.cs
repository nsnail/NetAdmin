using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps.Sys;

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