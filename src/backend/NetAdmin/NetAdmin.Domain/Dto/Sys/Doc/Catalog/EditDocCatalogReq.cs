namespace NetAdmin.Domain.Dto.Sys.Doc.Catalog;

/// <summary>
///     请求：编辑文档分类
/// </summary>
public sealed record EditDocCatalogReq : CreateDocCatalogReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}