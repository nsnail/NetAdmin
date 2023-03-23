using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Dic.Content;

/// <summary>
///     请求：创建字典内容
/// </summary>
public record CreateDicContentReq : TbSysDicContent
{
    /// <inheritdoc cref="TbSysDicCatalog.BitSet" />
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
    public override long CatalogId { get; init; }

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled { get; init; } = true;

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public override string Key { get; init; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public override string Value { get; init; }
}