using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Tpl;
using NSExt.Extensions;

namespace NetAdmin.Domain.Dto.Tpl.Example;

/// <summary>
///     响应：查询示例
/// </summary>
public record QueryExampleRsp : TbTplExample
{
    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled => BitSet.HasFlag(BitSets.Enabled);

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }
}