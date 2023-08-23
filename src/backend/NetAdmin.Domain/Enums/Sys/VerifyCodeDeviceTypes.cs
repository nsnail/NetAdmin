namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     验证码目标设备类型
/// </summary>
public enum VerifyCodeDeviceTypes
{
    /// <summary>
    ///     手机
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.手机))]
    Mobile = 1

   ,

    /// <summary>
    ///     电子邮箱
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.电子邮箱))]
    Email = 2
}