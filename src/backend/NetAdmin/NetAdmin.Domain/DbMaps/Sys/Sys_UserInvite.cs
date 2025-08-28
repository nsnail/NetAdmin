namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     用户邀请表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_UserInvite))]
public record Sys_UserInvite : VersionEntity, IFieldOwner
{
    /// <summary>
    ///     渠道
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(ChannelId))]
    public Sys_User Channel { get; init; }

    /// <summary>
    ///     渠道编号
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual long? ChannelId { get; init; }

    /// <summary>
    ///     子节点
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(OwnerId))]
    public IEnumerable<Sys_UserInvite> Children { get; init; }

    /// <summary>
    ///     返佣比率
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual int CommissionRatio { get; init; }

    /// <summary>
    ///     归属
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
    ///     允许自助充值
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual bool SelfDepositAllowed { get; init; }

    /// <summary>
    ///     用户
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(Id))]
    public Sys_User User { get; init; }
}