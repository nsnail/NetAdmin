namespace NetAdmin.Domain.Dto.Sys.Doc.Content;

/// <summary>
///     请求：编辑文档内容
/// </summary>
public sealed record EditDocContentReq : CreateDocContentReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}