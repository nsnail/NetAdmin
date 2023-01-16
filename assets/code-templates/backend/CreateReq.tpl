using {0}.DataContract.DbMaps.{1}.{2};

namespace {0}.DataContract.Dto.{1}.{2};

/// <summary>
///     请求：创建{3}
/// </summary>
public record Create{2}Req : Tb{1}{2} {{

    /// <inheritdoc cref="Tb{1}{2}.BitSet" />
    public override long BitSet {{
        get {{
            var ret = 0L;
            if (Enabled) {{
                ret |= (long)BitSets.Enabled;
            }}

            return ret;
        }}
    }}

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled {{ get; init; }} = true;

}}