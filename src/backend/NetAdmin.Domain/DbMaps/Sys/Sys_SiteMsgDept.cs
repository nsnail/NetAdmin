namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     站内信-部门映射表
/// </summary>
[SqlIndex($"{Chars.FLG_DB_INDEX_PREFIX}{nameof(DeptId)}_{nameof(SiteMsgId)}", $"{nameof(DeptId)},{nameof(SiteMsgId)}", true)]
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_SiteMsgDept))]
public record Sys_SiteMsgDept : ImmutableEntity
{
    /// <summary>
    ///     关联的部门
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    public Sys_Dept Dept { get; init; }

    /// <summary>
    ///     部门编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public long DeptId { get; init; }

    /// <summary>
    ///     关联的站内信
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    public Sys_SiteMsg SiteMsg { get; init; }

    /// <summary>
    ///     站内信编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public long SiteMsgId { get; init; }
}