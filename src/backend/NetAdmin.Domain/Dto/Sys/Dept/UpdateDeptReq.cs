using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Dependency;

namespace NetAdmin.Domain.Dto.Sys.Dept;

/// <summary>
///     请求：更新部门
/// </summary>
public record UpdateDeptReq : CreateDeptReq
{
    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}