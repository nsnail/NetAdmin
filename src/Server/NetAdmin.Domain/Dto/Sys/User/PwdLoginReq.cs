using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：密码登录
/// </summary>
public sealed record PwdLoginReq : DataAbstraction
{
    /// <summary>
    ///     用户名、手机号、邮箱
    /// </summary>
    [Required]
    public string Account { get; init; }

    /// <inheritdoc cref="Sys_User.Password" />
    [Required]
    public string Password { get; init; }
}