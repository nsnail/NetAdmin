namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     钱包交易表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_WalletTrade))]
public record Sys_WalletTrade : ImmutableEntity, IFieldOwner, IFieldSummary
{
    /// <summary>
    ///     交易金额
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long Amount { get; set; }

    /// <summary>
    ///     交易前余额
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long BalanceBefore { get; init; }

    /// <summary>
    ///     业务订单号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? BusinessOrderNumber { get; init; }

    /// <summary>
    ///     归属用户
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
    ///     备注
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Summary { get; set; }

    /// <summary>
    ///     交易方向
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual TradeDirections TradeDirection { get; init; }

    /// <summary>
    ///     交易类型
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual TradeTypes TradeType { get; init; }
}