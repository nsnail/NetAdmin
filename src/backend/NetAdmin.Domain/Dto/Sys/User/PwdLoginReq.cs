using System.ComponentModel.DataAnnotations;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：密码登录
/// </summary>
public record PwdLoginReq : DataAbstraction
{
    /// <summary>
    ///     用户名、手机号、邮箱
    /// </summary>
    [Required]
    public string Account { get; init; }

    /// <inheritdoc cref="TbSysUser.Password" />
    [Required]
    public string Password { get; init; }
}