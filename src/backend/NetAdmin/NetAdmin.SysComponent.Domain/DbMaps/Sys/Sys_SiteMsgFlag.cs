namespace NetAdmin.SysComponent.Domain.DbMaps.Sys;

/// <summary>
///     站内信标记表
/// </summary>
[SqlIndex($"{Chars.FLG_DB_INDEX_PREFIX}{nameof(SiteMsgId)}_{nameof(UserId)}", $"{nameof(SiteMsgId)},{nameof(UserId)}", true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_SiteMsgFlag))]
public record Sys_SiteMsgFlag : MutableEntity
{
    /// <summary>
    ///     站内信编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long SiteMsgId { get; init; }

    /// <summary>
    ///     用户编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public long UserId { get; init; }

    /// <summary>
    ///     用户站内信状态
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual UserSiteMsgStatues UserSiteMsgStatus { get; init; }
}