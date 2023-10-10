namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     请求：角色-菜单映射
/// </summary>
public sealed record MapMenusReq : DataAbstraction
{
    /// <summary>
    ///     菜单编号
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.菜单编号不能为空))]
    public IReadOnlyCollection<long> MenuIds { get; init; }

    /// <summary>
    ///     角色编号
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.角色编号不能为空))]
    public long RoleId { get; init; }
}