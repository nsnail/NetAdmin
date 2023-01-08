using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;
using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.DataContract.Dto.Sys.Role;

/// <summary>
///     请求：创建角色
/// </summary>
public record CreateRoleReq : TbSysRole
{
    /// <inheritdoc />
    public override long BitSet =>
        (long)(IgnorePermissionControl
            ? Enums.SysRoleBits.IgnorePermissionControl | Enums.SysRoleBits.Enabled
            : Enums.SysRoleBits.Enabled);

    /// <summary>
    ///     是否忽略权限控制
    /// </summary>
    [Required]
    public bool IgnorePermissionControl { get; set; }

    /// <inheritdoc cref="TbSysRole.Label" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Required]
    public override string Label { get; set; }
}