namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户钱包表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_UserWallet))]
public record Sys_UserWallet : LiteVersionEntity, IFieldOwner
{
    /// <summary>
    ///     可用余额
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long AvailableBalance { get; init; }

    /// <summary>
    ///     冻结余额
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long FrozenBalance { get; init; }

    /// <summary>
    ///     归属用户
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(OwnerId))]
    public Sys_User Owner { get; init; }

    /// <summary>
    ///     归属部门编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long? OwnerDeptId { get; init; }

    /// <summary>
    ///     归属用户编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long? OwnerId { get; init; }

    /// <summary>
    ///     总支出
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long TotalExpenditure { get; init; }

    /// <summary>
    ///     总收入
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long TotalIncome { get; init; }
}