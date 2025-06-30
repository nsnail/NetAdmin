namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户邀请表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_UserInvite))]
public record Sys_UserInvite : VersionEntity, IFieldOwner
{
    /// <summary>
    ///     子节点
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(OwnerId))]
    public IEnumerable<Sys_UserInvite> Children { get; init; }

    /// <summary>
    ///     归属
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(OwnerId))]
    public Sys_User Owner { get; init; }

    /// <summary>
    ///     归属部门编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? OwnerDeptId { get; init; }

    /// <summary>
    ///     归属用户编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? OwnerId { get; init; }

    /// <summary>
    ///     用户
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(Id))]
    public Sys_User User { get; init; }
}