using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.DbMaps;

/// <summary>
///     端点表
/// </summary>
[Table]
public record TbSysEndpoint : SimpleEntity
{
    /// <summary>
    ///     端点描述
    /// </summary>
    [JsonIgnore]
    public virtual string Label { get; set; }

    /// <summary>
    ///     端点路径
    /// </summary>
    [JsonIgnore]
    [Column(IsPrimary = true)]
    public virtual string Path { get; set; }
}