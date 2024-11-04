namespace NetAdmin.SysComponent.Domain.Enums.Sys;

/// <summary>
///     验证码类型
/// </summary>
[Export]
public enum VerifyCodeTypes
{
    /// <summary>
    ///     绑定手机号码
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.绑定手机号码))]
    LinkMobile = 1

   ,

    /// <summary>
    ///     登录
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.登录))]
    Login = 2

   ,

    /// <summary>
    ///     解绑手机号码
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.解绑手机号码))]
    UnlinkMobile = 3

   ,

    /// <summary>
    ///     注册
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.注册))]
    Register = 4

   ,

    /// <summary>
    ///     重设密码
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.重设密码))]
    ResetPassword = 5
}