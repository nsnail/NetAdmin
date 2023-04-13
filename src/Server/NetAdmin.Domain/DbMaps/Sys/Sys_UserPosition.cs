using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户-岗位映射表
/// </summary>
[Table(Name = nameof(Sys_UserPosition))]
public record Sys_UserPosition : ImmutableEntity
{
    /// <summary>
    ///     关联的岗位
    /// </summary>
    [JsonIgnore]
    public Sys_Position Position { get; init; }

    /// <summary>
    ///     岗位id
    /// </summary>
    [JsonIgnore]
    public long PositionId { get; init; }

    /// <summary>
    ///     关联的用户
    /// </summary>
    [JsonIgnore]
    public Sys_User User { get; init; }

    /// <summary>
    ///     用户id
    /// </summary>
    [JsonIgnore]
    public long UserId { get; init; }
}