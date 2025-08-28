using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     钱包冻结表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_WalletFrozen))]
public record Sys_WalletFrozen : LiteVersionEntity, IFieldOwner, IFieldSummary
{
    /// <summary>
    ///     冻结金额
    /// </summary>
    /// <example>100</example>
    [Column]
    [JsonIgnore]
    public virtual long Amount { get; init; }

    /// <summary>
    ///     冻结前数值
    /// </summary>
    /// <example>100</example>
    [Column]
    [JsonIgnore]
    public virtual long FrozenBalanceBefore { get; init; }

    /// <summary>
    ///     冻结编号
    /// </summary>
    /// <example>123456</example>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [JsonIgnore]
    [Snowflake]
    public override long Id { get; init; }

    /// <summary>
    ///     归属用户
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(OwnerId))]
    public Sys_User Owner { get; init; }

    /// <summary>
    ///     归属部门编号
    /// </summary>
    /// <example>370942943322181</example>
    [Column]
    [JsonIgnore]
    public virtual long? OwnerDeptId { get; init; }

    /// <summary>
    ///     归属用户编号
    /// </summary>
    /// <example>370942943322181</example>
    [Column]
    [JsonIgnore]
    public virtual long? OwnerId { get; init; }

    /// <summary>
    ///     冻结原因
    /// </summary>
    /// <example>Trade</example>
    [Column]
    [JsonIgnore]
    public virtual WalletFrozenReasons Reason { get; init; }

    /// <summary>
    ///     冻结状态
    /// </summary>
    /// <example>Frozen</example>
    [Column]
    [JsonIgnore]
    public virtual WalletFrozenStatues Status { get; init; }

    /// <summary>
    ///     备注
    /// </summary>
    /// <example>备注文字</example>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public virtual string Summary { get; set; }

    /// <summary>
    ///     钱包
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(OwnerId))]
    public Sys_UserWallet Wallet { get; init; }
}