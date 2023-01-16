using System.Text.Json.Serialization;

namespace NetAdmin.DataContract.Dto.Sys.Dic.Catalog;

/// <summary>
///     请求：更新字典目录
/// </summary>
public record UpdateDicCatalogReq : CreateDicCatalogReq
{
    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}