using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Position;

/// <summary>
///     请求：创建岗位
/// </summary>
public record CreatePositionReq : Sys_Position
{
    /// <inheritdoc cref="Sys_Position.Name" />
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.岗位名称))]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Name { get; init; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Sort { get; init; } = Numbers.DEF_SORT_VAL;

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Summary { get; init; }
}