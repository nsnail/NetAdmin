namespace NetAdmin.Host.Middlewares;

/// <summary>
///     输出环境信息到 http header
/// </summary>
public sealed class EnvironmentInfoMiddleware(RequestDelegate next)
{
    /// <summary>
    ///     主函数
    /// </summary>
    public Task InvokeAsync(HttpContext context) {
        context.Response.Headers.Append("X-Node", Environment.MachineName);
        return next(context);
    }
}