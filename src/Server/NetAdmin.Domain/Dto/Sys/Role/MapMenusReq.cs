namespace NetAdmin.Domain.Dto.Sys.Role;

/// <summary>
///     请求：角色-菜单映射
/// </summary>
public record MapMenusReq : DataAbstraction
{
    /// <summary>
    ///     菜单id
    /// </summary>
    [Required]
    public IReadOnlyCollection<long> MenuIds { get; init; }

    /// <summary>
    ///     角色id
    /// </summary>
    [Required]
    public long RoleId { get; init; }
}