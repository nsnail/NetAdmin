using NetAdmin.Domain.DbMaps.Dependency.Fields;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Dept;

/// <summary>
///     响应：查询部门
/// </summary>
public record QueryDeptRsp : Sys_Dept
{
    /// <summary>
    ///     子节点
    /// </summary>
    public new virtual IEnumerable<QueryDeptRsp> Children { get; init; }

    /// <inheritdoc cref="IFieldCreatedTime.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc cref="IFieldEnabled.Enabled" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override bool Enabled { get; init; }

    /// <inheritdoc cref="IFieldPrimary{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc cref="Sys_Dept.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Name { get; init; }

    /// <inheritdoc cref="Sys_Dept.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long ParentId { get; init; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Sort { get; init; } = Numbers.DEF_SORT_VAL;

    /// <inheritdoc cref="IFieldSummary.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}