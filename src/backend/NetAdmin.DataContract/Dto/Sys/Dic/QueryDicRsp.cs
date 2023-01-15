using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NSExt.Extensions;

namespace NetAdmin.DataContract.Dto.Sys.Dic;

/// <summary>
///     响应：查询菜单
/// </summary>
public record QueryDicRsp : TbSysDic
{
    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled => BitSet.HasFlag(BitSets.Enabled);

    /// <summary>
    ///     子节点
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public new IEnumerable<QueryDicRsp> Children { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Code { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Label { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long ParentId { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Value { get; init; }
}