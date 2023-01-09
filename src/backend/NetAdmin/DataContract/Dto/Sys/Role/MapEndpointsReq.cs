using System.ComponentModel.DataAnnotations;

namespace NetAdmin.DataContract.Dto.Sys.Role;

/// <summary>
///     请求：角色端点映射
/// </summary>
public record MapEndpointsReq : DataAbstraction
{
    /// <summary>
    ///     端点路径
    /// </summary>
    [Required]
    [MinLength(1)]
    public IReadOnlyCollection<string> Paths { get; set; }

    /// <summary>
    ///     角色id
    /// </summary>
    [Required]
    public long RoleId { get; set; }
}