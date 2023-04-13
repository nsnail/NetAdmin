using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     响应： 获取角色菜单
/// </summary>
public sealed record GetMenusRsp : Sys_RoleMenu
{
    /// <inheritdoc cref="Sys_RoleMenu.MenuId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long MenuId { get; init; }

    /// <inheritdoc cref="Sys_RoleMenu.RoleId" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long RoleId { get; init; }
}