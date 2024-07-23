namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     Api接口表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_Api))]
[SqlIndex(Chars.FLG_DB_INDEX_PREFIX          + nameof(PathCrc32), nameof(PathCrc32), true)]
public record Sys_Api : ImmutableEntity<string>, IFieldSummary
{
    /// <summary>
    ///     子节点
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(nameof(ParentId))]
    public IEnumerable<Sys_Api> Children { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127, IsIdentity = false, IsPrimary = true, Position = 1)]
    [CsvIgnore]
    [JsonIgnore]
    public override string Id { get; init; }

    /// <summary>
    ///     请求方式
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_15)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Method { get; init; }

    /// <summary>
    ///     服务名称
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Name { get; init; }

    /// <summary>
    ///     命名空间
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_31)]
    [CsvIgnore]
    [JsonIgnore]
    #pragma warning disable CA1716
    public virtual string Namespace { get; init; }
    #pragma warning restore CA1716

    /// <summary>
    ///     父编号
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string ParentId { get; init; }

    /// <summary>
    ///     路径CRC32
    /// </summary>
    [Column]
    [CsvIgnore]
    [JsonIgnore]
    public int PathCrc32 { get; init; }

    /// <summary>
    ///     角色集合
    /// </summary>
    [CsvIgnore]
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_RoleApi))]
    public ICollection<Sys_Role> Roles { get; init; }

    /// <summary>
    ///     服务描述
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_63)]
    [CsvIgnore]
    [JsonIgnore]
    public virtual string Summary { get; init; }
}