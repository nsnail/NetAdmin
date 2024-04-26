using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     站内信-用户映射表
/// </summary>
[Index($"{Chars.FLG_DB_INDEX_PREFIX}{nameof(UserId)}_{nameof(SiteMsgId)}", $"{nameof(UserId)},{nameof(SiteMsgId)}"
     , true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_SiteMsgUser))]
public record Sys_SiteMsgUser : ImmutableEntity
{
    /// <summary>
    ///     关联的站内信
    /// </summary>
    [JsonIgnore]
    public Sys_SiteMsg SiteMsg { get; init; }

    /// <summary>
    ///     站内信编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public long SiteMsgId { get; init; }

    /// <summary>
    ///     关联的用户
    /// </summary>
    [JsonIgnore]
    public Sys_User User { get; init; }

    /// <summary>
    ///     用户编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public long UserId { get; init; }
}