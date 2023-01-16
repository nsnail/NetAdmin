using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps.Sys;
using NSExt.Extensions;

namespace NetAdmin.DataContract.Dto.Sys.Dic.Content;

/// <summary>
///     响应：查询字典内容
/// </summary>
public record QueryDicContentRsp : TbSysDicContent
{
    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled => BitSet.HasFlag(BitSets.Enabled);

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long CatalogId { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Key { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Value { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}