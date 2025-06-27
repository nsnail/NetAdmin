namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     配置表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_Config))]
public record Sys_Config : VersionEntity, IFieldEnabled
{
    /// <summary>
    ///     人民币兑点数比率
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int CnyToPointRate { get; init; }

    /// <summary>
    ///     是否启用
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     Trc20收款地址
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_CHAR_34)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Trc20ReceiptAddress { get; init; }

    /// <summary>
    ///     美元兑点数比率
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual int UsdToPointRate { get; init; }

    /// <summary>
    ///     用户注册是否需要人工确认
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool UserRegisterConfirm { get; init; }

    /// <summary>
    ///     用户注册默认部门
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(UserRegisterDeptId))]
    public Sys_Dept UserRegisterDept { get; init; }

    /// <summary>
    ///     用户注册默认部门编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long UserRegisterDeptId { get; init; }

    /// <summary>
    ///     用户注册默认角色
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(UserRegisterRoleId))]
    public Sys_Role UserRegisterRole { get; init; }

    /// <summary>
    ///     用户注册默认角色编号
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long UserRegisterRoleId { get; init; }
}