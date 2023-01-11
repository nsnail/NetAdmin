using System.ComponentModel.DataAnnotations;

namespace NetAdmin.DataContract.Dto.Sys.Role;

/// <summary>
///     请求：角色-菜单映射
/// </summary>
public record MapMenusReq : DataAbstraction
{
    /// <summary>
    ///     端点路径
    /// </summary>
    [Required]
    public IReadOnlyCollection<string> MenuNames { get; set; }

    /// <summary>
    ///     角色id
    /// </summary>
    [Required]
    public long RoleId { get; set; }
}