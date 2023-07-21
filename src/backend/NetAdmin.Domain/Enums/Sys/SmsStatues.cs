namespace NetAdmin.Domain.Enums.Sys;

/// <summary>
///     短信状态
/// </summary>
public enum SmsStatues
{
    /// <summary>
    ///     等待发送
    /// </summary>
    Waiting = 1

   ,

    /// <summary>
    ///     已发送
    /// </summary>
    Sent = 2

   ,

    /// <summary>
    ///     发送失败
    /// </summary>
    Failed = 3

   ,

    /// <summary>
    ///     已校验
    /// </summary>
    Verified = 4
}