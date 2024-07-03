namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     角色-接口映射表
/// </summary>
[Table(Name = Chars.FLG_DB_TABLE_NAME_PREFIX + nameof(Sys_RoleApi))]
public record Sys_RoleApi : ImmutableEntity
{
    /// <summary>
    ///     关联的接口
    /// </summary>
    [Ignore]
    [JsonIgnore]
    public Sys_Api Api { get; init; }

    /// <summary>
    ///     接口编号
    /// </summary>
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR_127)]
    [Ignore]
    [JsonIgnore]
    public string ApiId { get; init; }

    /// <summary>
    ///     关联的角色
    /// </summary>
    [Ignore]
    [JsonIgnore]
    public Sys_Role Role { get; init; }

    /// <summary>
    ///     角色编号
    /// </summary>
    [Column]
    [Ignore]
    [JsonIgnore]
    public long RoleId { get; init; }
}