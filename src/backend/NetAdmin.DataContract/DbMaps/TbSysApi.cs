using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     Api接口表
/// </summary>
[Table]
public record TbSysApi : ImmutableEntity<string>
{
    /// <summary>
    ///     子节点
    /// </summary>
    [Navigate(nameof(ParentId))]
    [JsonIgnore]
    public virtual IEnumerable<TbSysApi> Children { get; init; }

    /// <summary>
    ///     请求方式
    /// </summary>
    [JsonIgnore]
    public virtual string Method { get; init; }

    /// <summary>
    ///     服务名称
    /// </summary>
    [JsonIgnore]
    public virtual string Name { get; init; }

    /// <summary>
    ///     命名空间
    /// </summary>
    [JsonIgnore]
    public virtual string Namespace { get; init; }

    /// <summary>
    ///     父id
    /// </summary>
    [JsonIgnore]
    public virtual string ParentId { get; init; }

    /// <summary>
    ///     服务描述
    /// </summary>
    [JsonIgnore]
    public virtual string Summary { get; init; }
}