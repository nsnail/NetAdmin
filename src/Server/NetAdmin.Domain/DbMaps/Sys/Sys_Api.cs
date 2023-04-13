using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     Api接口表
/// </summary>
[Table(Name = nameof(Sys_Api))]
public record Sys_Api : ImmutableEntity<string>, IFieldSummary
{
    /// <summary>
    ///     子节点
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(ParentId))]
    public IEnumerable<Sys_Api> Children { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR127)]
    public override string Id { get; init; }

    /// <summary>
    ///     请求方式
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR15)]
    public virtual string Method { get; init; }

    /// <summary>
    ///     服务名称
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR63)]
    public virtual string Name { get; init; }

    /// <summary>
    ///     命名空间
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR31)]
    public virtual string Namespace { get; init; }

    /// <summary>
    ///     父id
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR127)]
    public virtual string ParentId { get; init; }

    /// <summary>
    ///     角色集合
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(Sys_RoleApi))]
    public ICollection<Sys_Role> Roles { get; init; }

    /// <summary>
    ///     服务描述
    /// </summary>
    [JsonIgnore]
    [Column(DbType = Chars.FLG_DB_FIELD_TYPE_VARCHAR63)]
    public virtual string Summary { get; init; }
}