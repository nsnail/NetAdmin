using {0}.DataContract.DbMaps.{1}.{2};

namespace {0}.DataContract.Dto.{1}.{2};

/// <summary>
///     响应：查询{3}
/// </summary>
public record Query{2}Rsp : Tb{1}{2} {{
    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled => BitSet.HasFlag(BitSets.Enabled);

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version {{ get; init; }}
}}