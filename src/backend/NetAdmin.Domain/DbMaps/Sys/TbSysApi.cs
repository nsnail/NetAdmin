using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.DbMaps.Sys;

/// <summary>
///     Api接口表
/// </summary>
[Table]
public record TbSysApi : ImmutableEntity<string>, IFieldSummary
{
    /// <summary>
    ///     子节点
    /// </summary>
    [JsonIgnore]
    [Navigate(nameof(ParentId))]
    public virtual IEnumerable<TbSysApi> Children { get; init; }

    /// <inheritdoc />
    [JsonIgnore]
    [MaxLength(127)]
    public override string Id { get; set; }

    /// <summary>
    ///     请求方式
    /// </summary>
    [JsonIgnore]
    [MaxLength(15)]
    public virtual string Method { get; init; }

    /// <summary>
    ///     服务名称
    /// </summary>
    [JsonIgnore]
    [MaxLength(63)]
    public virtual string Name { get; init; }

    /// <summary>
    ///     命名空间
    /// </summary>
    [JsonIgnore]
    [MaxLength(31)]
    public virtual string Namespace { get; init; }

    /// <summary>
    ///     父id
    /// </summary>
    [JsonIgnore]
    [MaxLength(127)]
    public virtual string ParentId { get; init; }

    /// <summary>
    ///     角色集合
    /// </summary>
    [JsonIgnore]
    [Navigate(ManyToMany = typeof(TbSysRoleApi))]
    public virtual ICollection<TbSysRole> Roles { get; init; }

    /// <summary>
    ///     服务描述
    /// </summary>
    [JsonIgnore]
    [MaxLength(63)]
    public virtual string Summary { get; init; }
}