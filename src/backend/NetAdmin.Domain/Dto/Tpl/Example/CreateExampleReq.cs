using NetAdmin.Domain.DbMaps.Tpl;

namespace NetAdmin.Domain.Dto.Tpl.Example;

/// <summary>
///     请求：创建示例
/// </summary>
public record CreateExampleReq : TbTplExample
{
    /// <inheritdoc />
    public override long BitSet {
        get {
            var ret = 0L;
            if (Enabled) {
                ret |= (long)BitSets.Enabled;
            }

            return ret;
        }
    }

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled { get; init; } = true;
}