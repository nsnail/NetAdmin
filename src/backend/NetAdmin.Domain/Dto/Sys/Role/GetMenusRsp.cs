using System.Text.Json.Serialization;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     响应： 获取角色菜单
/// </summary>
public record GetMenusRsp : TbSysRoleMenu
{
    /// <inheritdoc cref="TbSysRoleMenu.MenuId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long MenuId { get; init; }

    /// <inheritdoc cref="TbSysRoleMenu.RoleId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public override long RoleId { get; init; }
}