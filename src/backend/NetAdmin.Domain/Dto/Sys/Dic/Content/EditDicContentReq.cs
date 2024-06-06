using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.Dto.Sys.Dic.Content;

/// <summary>
///     请求：编辑字典内容
/// </summary>
public sealed record EditDicContentReq : CreateDicContentReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}