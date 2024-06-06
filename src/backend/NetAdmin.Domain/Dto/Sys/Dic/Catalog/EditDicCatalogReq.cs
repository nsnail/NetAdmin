using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.Dto.Sys.Dic.Catalog;

/// <summary>
///     请求：编辑字典目录
/// </summary>
public sealed record EditDicCatalogReq : CreateDicCatalogReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}