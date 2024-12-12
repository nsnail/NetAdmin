namespace NetAdmin.Domain.Dto.Sys.Doc.Catalog;

/// <summary>
///     请求：创建文档分类
/// </summary>
public record CreateDocCatalogReq : Sys_DocCatalog
{
    /// <inheritdoc cref="Sys_DocCatalog.Code" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.文档分类编码不能为空))]
    public override string Code { get; init; }

    /// <inheritdoc cref="Sys_DocCatalog.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.文档分类名称不能为空))]
    public override string Name { get; init; }

    /// <inheritdoc cref="Sys_DocCatalog.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long ParentId { get; init; }
}