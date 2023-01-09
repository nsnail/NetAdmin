using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.DataContract.DbMaps.Dependency;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.Dto.Sys.Role;

/// <summary>
///     请求：创建角色
/// </summary>
public record CreateRoleReq : TbSysRole
{
    /// <inheritdoc cref="TbSysRole.BitSet" />
    public override long BitSet {
        get {
            var ret = 0L;
            if (Enabled) {
                ret |= (long)Enums.SysRoleBits.Enabled;
            }

            if (IgnorePermissionControl) {
                ret |= (long)Enums.SysRoleBits.IgnorePermissionControl;
            }

            return ret;
        }
    }

    /// <summary>
    ///     是否启用
    /// </summary>
    [Required]
    public bool Enabled { get; set; }

    /// <summary>
    ///     是否忽略权限控制
    /// </summary>
    [Required]
    public bool IgnorePermissionControl { get; set; }

    /// <inheritdoc cref="TbSysRole.Label" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [Required]
    public override string Label { get; set; }

    /// <inheritdoc cref="TbSysRole.Remark" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string Remark { get; set; }

    /// <inheritdoc cref="IFieldSort.Sort" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override int Sort { get; set; }
}