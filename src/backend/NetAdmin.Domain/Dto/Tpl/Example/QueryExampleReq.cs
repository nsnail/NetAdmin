using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Tpl;

namespace NetAdmin.Domain.Dto.Tpl.Example;

/// <summary>
///     请求：查询示例
/// </summary>
public sealed record QueryExampleReq : Tpl_Example
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }
}