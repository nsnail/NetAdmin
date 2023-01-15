using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;

namespace NetAdmin.DataContract.Dto.Sys.Dic;

/// <summary>
///     请求：创建字典
/// </summary>
public record CreateDicReq : TbSysDic
{
    /// <inheritdoc cref="TbSysDic.BitSet" />
    public override long BitSet {
        get {
            var ret = 0L;
            if (Enabled) {
                ret |= (long)BitSets.Enabled;
            }

            return ret;
        }
    }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public override string Code { get; init; }

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled { get; init; } = true;

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public override string Label { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long ParentId { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public override string Value { get; init; }
}