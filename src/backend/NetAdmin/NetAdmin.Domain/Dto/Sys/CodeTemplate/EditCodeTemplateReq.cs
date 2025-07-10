using NetAdmin.Domain.DbMaps.Dependency.Fields;

namespace NetAdmin.Domain.Dto.Sys.CodeTemplate;

/// <summary>
///     请求：编辑代码模板
/// </summary>
public sealed record EditCodeTemplateReq : CreateCodeTemplateReq
{
    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}