using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户-岗位映射表
/// </summary>
[Table]
public record TbSysUserPosition : ImmutableEntity
{
    /// <summary>
    ///     关联的岗位
    /// </summary>
    [JsonIgnore]
    public virtual TbSysPosition Position { get; init; }

    /// <summary>
    ///     岗位id
    /// </summary>
    [JsonIgnore]
    public virtual long PositionId { get; init; }

    /// <summary>
    ///     关联的用户
    /// </summary>
    [JsonIgnore]
    public virtual TbSysUser User { get; init; }

    /// <summary>
    ///     用户id
    /// </summary>
    [JsonIgnore]
    public virtual long UserId { get; init; }
}