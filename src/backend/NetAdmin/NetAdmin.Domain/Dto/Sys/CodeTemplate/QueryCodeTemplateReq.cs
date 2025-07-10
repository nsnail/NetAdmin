using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.CodeTemplate;

/// <summary>
///     请求：查询代码模板
/// </summary>
public sealed record QueryCodeTemplateReq : Sys_CodeTemplate
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}