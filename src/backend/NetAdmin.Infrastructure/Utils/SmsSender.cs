using Furion.DependencyInjection;

namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     短信发送工具
/// </summary>
public class SmsSender : IScoped
{
    /// <summary>
    ///     发送短信
    /// </summary>
    /// <param name="mobile">手机号</param>
    /// <param name="content">短信内容</param>
    public void Send(long mobile, string content)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     发送短信验证码
    /// </summary>
    /// <param name="mobile">手机号</param>
    /// <param name="code">短信码</param>
    public void SendCode(long mobile, string code)
    {
        throw new NotImplementedException();
    }
}