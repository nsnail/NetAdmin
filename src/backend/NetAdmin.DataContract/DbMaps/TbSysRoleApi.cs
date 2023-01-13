using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     角色-接口映射表
/// </summary>
[Table]
public record TbSysRoleApi : ImmutableEntity
{
    /// <summary>
    ///     接口id
    /// </summary>
    [JsonIgnore]
    public virtual string ApiId { get; init; }

    /// <summary>
    ///     角色id
    /// </summary>
    [JsonIgnore]
    public virtual long RoleId { get; init; }
}