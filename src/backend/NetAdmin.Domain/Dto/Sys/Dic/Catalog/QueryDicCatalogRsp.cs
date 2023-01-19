using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Sys;
using NSExt.Extensions;

namespace NetAdmin.Domain.Dto.Sys.Dic.Catalog;

/// <summary>
///     响应：查询字典目录
/// </summary>
public record QueryDicCatalogRsp : TbSysDicCatalog
{
    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled => BitSet.HasFlag(BitSets.Enabled);

    /// <summary>
    ///     子节点
    /// </summary>
    public new IEnumerable<QueryDicCatalogRsp> Children { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Code { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long Id { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override string Name { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long ParentId { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}