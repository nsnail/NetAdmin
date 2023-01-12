using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps.Dependency;

namespace NetAdmin.DataContract.Dto.Sys.Dept;

/// <summary>
///     请求：更新部门
/// </summary>
public record UpdateDeptReq : CreateDeptReq
{
    /// <inheritdoc cref="IFieldPrimary.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}