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
    private static bool _hostStopping;

    /// <summary>
    ///     当前连接数
    /// </summary>
    public static long Connections => Interlocked.Read(ref _connections);

    /// <summary>
    ///     停机处理
    /// </summary>
    public static void OnStopping()
    {
        Volatile.Write(ref _hostStopping, true);
        while (Interlocked.Read(ref _connections) > 0) {
            Thread.Sleep(10);
        }
    }

    /// <summary>
    ///     主函数
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        if (Volatile.Read(ref _hostStopping)) {
            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            return;
        }

        _ = Interlocked.Increment(ref _connections);
        await next(context).ConfigureAwait(false);
        _ = Interlocked.Decrement(ref _connections);
    }
}