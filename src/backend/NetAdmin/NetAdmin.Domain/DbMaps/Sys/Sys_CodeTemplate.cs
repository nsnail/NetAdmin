namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     代码模板表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_CodeTemplate))]
public record Sys_CodeTemplate : VersionEntity, IFieldSort, IFieldSummary, IFieldEnabled, IFieldOwner
{
    /// <summary>
    ///     是否启用
    /// </summary>
    /// <example>true</example>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual bool Enabled { get; init; }

    /// <summary>
    ///     性别
    /// </summary>
    /// <example>Male</example>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual Genders? Gender { get; init; }

    /// <summary>
    ///     唯一编码
    /// </summary>
    /// <example>123456</example>
    [Column(IsIdentity = false, IsPrimary = true, Position = 1)]
    [CsvIgnore]
    [JsonIgnore]
    [Snowflake]
    public override long Id { get; init; }

    /// <summary>
    ///     名称
    /// </summary>
    /// <example>老王</example>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Name { get; init; }

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
    /// <example>370942943322181</example>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? OwnerDeptId { get; init; }

    /// <summary>
    ///     归属用户编号
    /// </summary>
    /// <example>370942943322181</example>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long? OwnerId { get; init; }

    /// <summary>
    ///     排序值，越大越前
    /// </summary>
    /// <example>100</example>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public virtual long Sort { get; init; }

    /// <summary>
    ///     备注
    /// </summary>
    /// <example>备注文字</example>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_255)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Summary { get; set; }
}