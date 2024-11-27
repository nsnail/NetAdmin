using NetAdmin.Domain.DbMaps.Sys;
using NetAdmin.Domain.Enums.Sys;

namespace NetAdmin.Domain.Dto.Sys.Doc.Content;

/// <summary>
///     响应：查询文档内容
/// </summary>
public record QueryDocContentRsp : Sys_DocContent
{
    /// <inheritdoc cref="Sys_DocContent.Body" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Body { get; init; }

    /// <inheritdoc cref="Sys_DocContent.CatalogId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long CatalogId { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="Sys_DocContent.Title" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Title { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }

    /// <inheritdoc cref="Sys_DocContent.Visibility" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override ArchiveVisibilities Visibility { get; init; }
}