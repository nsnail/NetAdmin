namespace NetAdmin.Host.Middlewares;

/// <summary>
///     安全停机中间件
/// </summary>
/// <remarks>
///     放在所有中间件最前面
/// </remarks>
public sealed class SafetyShopHostMiddleware(RequestDelegate next)
{
    private static long _connections;
    private static bool _trafficOff;

    /// <summary>
    ///     当前连接数
    /// </summary>
    public static long Connections => Interlocked.Read(ref _connections);

    /// <summary>
    ///     是否已停机
    /// </summary>
    public static bool IsShutdown => Volatile.Read(ref _trafficOff);

    /// <summary>
    ///     停机处理
    /// </summary>
    public static void OnStopping()
    {
        Stop();
        while (Interlocked.Read(ref _connections) > 0) {
            Thread.Sleep(10);
        }
    }

    /// <summary>
    ///     系统启机
    /// </summary>
    public static void Start()
    {
        Volatile.Write(ref _trafficOff, false);
    }

    /// <summary>
    ///     系统停机
    /// </summary>
    public static void Stop()
    {
        Volatile.Write(ref _trafficOff, true);
    }

    /// <summary>
    ///     主函数
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        if (Volatile.Read(ref _trafficOff) &&
            !context.Request.Path.StartsWithSegments($"/{Chars.FLG_PATH_API_RPOBE}")) {
            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            return;
        }

        // webSocket链接不参与计算
        if (context.Request.Path.StartsWithSegments($"/{Chars.FLG_PATH_WEBSOCKET_PREFIX}")) {
            await next(context).ConfigureAwait(false);
        }
        else {
            _ = Interlocked.Increment(ref _connections);
            await next(context).ConfigureAwait(false);
            _ = Interlocked.Decrement(ref _connections);
        }
    }
}