using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Dependency;
using NetAdmin.Domain.DbMaps.Sys;
using NSExt.Extensions;

namespace NetAdmin.Domain.Dto.Sys.Dept;

/// <summary>
///     响应：查询部门
/// </summary>
public record QueryDeptRsp : TbSysDept
{
    /// <summary>
    ///     是否启用
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Enabled => BitSet.HasFlag(BitSets.Enabled);

    /// <summary>
    ///     子节点
    /// </summary>
    public new virtual List<QueryDeptRsp> Children { get; init; }

    /// <inheritdoc cref="IFieldAdd.CreatedTime" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override DateTime CreatedTime { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long Id { get; set; }

    /// <inheritdoc cref="TbSysDept.Name" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Name { get; init; }

    /// <inheritdoc cref="TbSysDept.ParentId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long ParentId { get; init; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Sort { get; init; }

    /// <inheritdoc cref="TbSysDept.Summary" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Summary { get; init; }

    /// <inheritdoc cref="IFieldUpdate.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}