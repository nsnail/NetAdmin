using NetAdmin.Domain.Attributes.DataValidation;
using NetAdmin.Domain.DbMaps.Sys;

namespace NetAdmin.Domain.Dto.Sys.User;

/// <summary>
///     请求：密码登录
/// </summary>
public record LoginByPwdReq : DataAbstraction
{
    /// <summary>
    ///     用户名、手机号、邮箱
    /// </summary>
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.账号))]
    public string Account { get; init; }

    /// <inheritdoc cref="Sys_User.Password" />
    [CultureRequired(ErrorMessageResourceType = typeof(Ln), ErrorMessageResourceName = nameof(Ln.密码))]
    public string Password { get; init; }
}