using NetAdmin.SysComponent.Host.Utils;

namespace NetAdmin.SysComponent.Host.Middlewares;

/// <summary>
///     请求审计中间件
/// </summary>
public sealed class RequestAuditMiddleware(
    RequestDelegate next
    , IOptions<DynamicApiControllerSettingsOptions> dynamicApiControllerSettingsOptions
    , RequestLogger requestLogger)
{
    private readonly PathString _defaultRoutePrefix = new($"/{dynamicApiControllerSettingsOptions.Value.DefaultRoutePrefix}");

    private readonly PathString _healthCheckRoutePrefix = new($"/{dynamicApiControllerSettingsOptions.Value.DefaultRoutePrefix}/probe/health.check");

    /// <summary>
    ///     主函数
    /// </summary>
    public async Task InvokeAsync(HttpContext context) {
        // 跳过处理的情况：
        if (!context.Request.Path.StartsWithSegments(_defaultRoutePrefix) // 非api请求
            || context.Request.Path.StartsWithSegments(_healthCheckRoutePrefix) // 健康检查
            || context.Request.Method == Chars.FLG_HTTP_METHOD_OPTIONS // is options 请求
            || (context.Request.ContentType?.StartsWith("multipart/form-data", true, CultureInfo.InvariantCulture) ?? false) // 文件上传
            #pragma warning disable SA1009
           ) {
            #pragma warning restore SA1009
            await next(context).ConfigureAwait(false);

            return;
        }

        // Response.Body流默认是不可读的，将Response.Body流替换成MemoryStream 使其可读
        await using var ms = new MemoryStream();
        var stream = context.Response.Body;
        context.Response.Body = ms;

        // 在控制台上输出分割线，区分不同请求

        // 调用下一个中间件
        var sw = Stopwatch.StartNew();
        await next(context).ConfigureAwait(false);
        sw.Stop();

        _ = ms.Seek(0, SeekOrigin.Begin);
        using var sr = new StreamReader(ms);
        var responseBody = await sr.ReadToEndAsync().ConfigureAwait(false);
        _ = ms.Seek(0, SeekOrigin.Begin);
        await ms.CopyToAsync(stream).ConfigureAwait(false);
        context.Response.Body = stream;

        var exception = context.Features.Get<IExceptionHandlerFeature>();
        var errorCode = context.Response.Headers[nameof(ErrorCodes)].FirstOrDefault()?.Enum<ErrorCodes>() ?? 0;

        _ = await requestLogger.LogAsync(context, (long)sw.Elapsed.TotalMilliseconds, responseBody, errorCode, exception).ConfigureAwait(false);
    }
}