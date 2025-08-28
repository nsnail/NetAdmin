using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     充值订单表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_DepositOrder))]
[SqlIndex(
    $"{Chars.FLG_DB_INDEX_PREFIX}{nameof(ActualPayAmount)}_{nameof(FinishTimestamp)}", $"{nameof(ActualPayAmount)},{nameof(FinishTimestamp)}", true
)]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX + nameof(PaymentFinger), nameof(PaymentFinger), true, WhenNotNull = true)]
public record Sys_DepositOrder : LiteVersionEntity, IFieldOwner
{
    /// <summary>
    ///     实际支付金额
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long ActualPayAmount { get; init; }

    /// <summary>
    ///     订单状态
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual DepositOrderStatues DepositOrderStatus { get; init; }

    /// <summary>
    ///     充值点数
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long DepositPoint { get; init; }

    /// <summary>
    ///     完成时间戳
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long FinishTimestamp { get; init; }

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
    ///     付款账号
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [JsonIgnore]
    public virtual string PaidAccount { get; init; }

    /// <summary>
    ///     付款时间
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual DateTime? PaidTime { get; init; }

    /// <summary>
    ///     付款指纹
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public virtual string PaymentFinger { get; init; }

    /// <summary>
    ///     支付方式
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual PaymentModes PaymentMode { get; init; }

    /// <summary>
    ///     收款账号
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [JsonIgnore]
    public virtual string ReceiptAccount { get; init; }

    /// <summary>
    ///     兑点数比率
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual int ToPointRate { get; init; }
}