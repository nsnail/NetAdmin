using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.Dto.Sys.Dept;

/// <summary>
///     请求：更新部门
/// </summary>
public record UpdateDeptReq : CreateDeptReq
{
    /// <inheritdoc cref="TbSysDept.BitSet" />
    public override long BitSet {
        get {
            var ret = 0L;
            if (Enabled) {
                ret |= (long)Enums.SysDeptBits.Enabled;
            }

            return ret;
        }
    }

    /// <summary>
    ///     是否启用
    /// </summary>
    public bool Enabled { get; set; }

    /// <inheritdoc cref="IFieldPrimary.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; set; }

    /// <inheritdoc />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; set; }
}