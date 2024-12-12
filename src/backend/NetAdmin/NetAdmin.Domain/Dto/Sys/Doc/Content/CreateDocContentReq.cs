using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.Doc.Content;

/// <summary>
///     请求：创建文档内容
/// </summary>
public record CreateDocContentReq : Sys_DocContent
{
    /// <inheritdoc cref="Sys_DocContent.Body" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.文档内容不能为空))]
    public override string Body { get; init; }

    /// <inheritdoc cref="Sys_DocContent.CatalogId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.文档分类编号不能为空))]
    public override long CatalogId { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; } = true;

    /// <inheritdoc cref="Sys_DocContent.Title" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.文档标题不能为空))]
    public override string Title { get; init; }

    /// <inheritdoc cref="Sys_DocContent.Visibility" />
    [EnumDataType(typeof(ArchiveVisibilities), ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.档案可见性不正确))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override ArchiveVisibilities Visibility { get; init; }
}