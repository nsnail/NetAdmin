using System.Text.Json.Serialization;
using NetAdmin.DataContract.DbMaps;

namespace NetAdmin.DataContract.Dto.Sys.Role;

/// <summary>
///     响应： 获取角色菜单
/// </summary>
public record GetMenusRsp : TbSysRoleMenu
{
    /// <inheritdoc cref="TbSysRoleMenu.MenuName" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override string MenuName { get; set; }

    /// <inheritdoc cref="TbSysRoleMenu.RoleId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long RoleId { get; set; }
}