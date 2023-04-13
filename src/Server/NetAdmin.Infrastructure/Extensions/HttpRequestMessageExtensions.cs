namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     HttpRequestMessage 扩展方法
/// </summary>
public static class HttpRequestMessageExtensions
{
    /// <summary>
    ///     将Http请求的Uri、Header、Body打包成Json字符串
    /// </summary>
    public static async Task<string> BuildJsonAsync(this HttpRequestMessage me)
    {
        var body = me?.Content is null ? null : await me.Content!.ReadAsStringAsync();
        return new { Uri = me?.RequestUri, Header = me?.ToString(), Body = body }.ToJson();
    }

    /// <summary>
    ///     记录日志
    /// </summary>
    public static async Task<HttpRequestMessage> LogAsync<T>(this HttpRequestMessage me, ILogger<T> logger)
    {
        logger.Info($"{Ln.Request}: {await me.BuildJsonAsync()}");
        return me;
    }
}