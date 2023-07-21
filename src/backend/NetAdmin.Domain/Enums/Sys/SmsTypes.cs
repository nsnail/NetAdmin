namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     短信类型
/// </summary>
public enum SmsTypes
{
    /// <summary>
    ///     绑定手机号
    /// </summary>
    LinkMobile = 1

   ,

    /// <summary>
    ///     登录
    /// </summary>
    Login = 2

   ,

    /// <summary>
    ///     解绑手机号
    /// </summary>
    UnlinkMobile = 3

   ,

    /// <summary>
    ///     注册
    /// </summary>
    Register = 4

   ,

    /// <summary>
    ///     重设密码
    /// </summary>
    ResetPassword = 5
}