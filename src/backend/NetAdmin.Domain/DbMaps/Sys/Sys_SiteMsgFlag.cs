using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     站内信标记表
/// </summary>
[Index($"{Chars.FLG_DB_INDEX_PREFIX}{nameof(SiteMsgId)}_{nameof(UserId)}", $"{nameof(SiteMsgId)},{nameof(UserId)}"
     , true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_SiteMsgFlag))]
public record Sys_SiteMsgFlag : MutableEntity
{
    /// <summary>
    ///     站内信编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long SiteMsgId { get; init; }

    /// <summary>
    ///     用户编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public long UserId { get; init; }

    /// <summary>
    ///     用户站内信状态
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual UserSiteMsgStatues UserSiteMsgStatus { get; init; }
}