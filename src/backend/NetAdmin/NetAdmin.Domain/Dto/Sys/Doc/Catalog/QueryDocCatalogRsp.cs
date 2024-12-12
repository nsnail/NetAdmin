namespace NetAdmin.Domain.Dto.Sys.Doc.Catalog;

/// <summary>
///     响应：查询文档分类
/// </summary>
public sealed record QueryDocCatalogRsp : Sys_DocCatalog
{
    /// <inheritdoc cref="Sys_DocCatalog.Children" />
    public new IEnumerable<QueryDocCatalogRsp> Children { get; init; }

    /// <inheritdoc cref="Sys_DocCatalog.Code" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Code { get; init; }

    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_DocCatalog.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Name { get; init; }

    /// <inheritdoc cref="Sys_DocCatalog.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long ParentId { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}