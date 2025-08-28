using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     验证码表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_VerifyCode))]
public record Sys_VerifyCode : VersionEntity
{
    /// <summary>
    ///     验证码
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_7)]
    [JsonIgnore]
    public virtual string Code { get; init; }

    /// <summary>
    ///     目标设备
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [JsonIgnore]
    public virtual string DestDevice { get; init; }

    /// <summary>
    ///     设备类型
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual VerifyCodeDeviceTypes DeviceType { get; init; }

    /// <summary>
    ///     发送报告
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [JsonIgnore]
    public string Report { get; init; }

    /// <summary>
    ///     验证码状态
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual VerifyCodeStatues Status { get; init; }

    /// <summary>
    ///     验证码类型
    /// </summary>
    [Column]
    [JsonIgnore]
    public virtual VerifyCodeTypes Type { get; init; }
}