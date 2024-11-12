namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     HttpContext 扩展方法
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    ///     获取客户端真实IP
    /// </summary>
    public static IPAddress GetRealIpAddress(this HttpContext me)
    {
        #pragma warning disable IDE0046
        if (me.Request.Headers.TryGetValue(Chars.FLG_HTTP_HEADER_KEY_X_FORWARDED_FOR, out var ips1) &&
            #pragma warning restore IDE0046
            IPAddress.TryParse(ips1.FirstOrDefault()?.Split(',').FirstOrDefault(), out var ip1)) {
            return ip1;
        }

        return me.Request.Headers.TryGetValue(Chars.FLG_HTTP_HEADER_KEY_X_REAL_IP, out var ips2) &&
               IPAddress.TryParse(ips2.FirstOrDefault()?.Split(',').FirstOrDefault(), out var ip2)
            ? ip2
            : me.Connection.RemoteIpAddress;
    }

    /// <summary>
    ///     获取跟踪标识
    /// </summary>
    public static Guid GetTraceId(this HttpContext me)
    {
        return me.TraceIdentifier.Md5(Encoding.UTF8).Guid();
    }
}