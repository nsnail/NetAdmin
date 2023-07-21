using NetAdmin.Host.Utils;

namespace NetAdmin.Host.Middlewares;

/// <summary>
///     请求审计中间件
/// </summary>
/// <remarks>
///     放在所有中间件最前面
/// </remarks>
public sealed class RequestAuditMiddleware
{
    private readonly PathString      _defaultRoutePrefix;
    private readonly PathString      _healthCheckRoutePrefix;
    private readonly RequestDelegate _next;
    private readonly RequestLogger   _requestLogger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestAuditMiddleware" /> class.
    /// </summary>
    public RequestAuditMiddleware( //
        RequestDelegate next, IOptions<DynamicApiControllerSettingsOptions> dynamicApiControllerSettingsOptions
      , RequestLogger   requestLogger)
    {
        _next               = next;
        _requestLogger      = requestLogger;
        _defaultRoutePrefix = new PathString($"/{dynamicApiControllerSettingsOptions.Value.DefaultRoutePrefix}");
        _healthCheckRoutePrefix
            = new PathString($"/{dynamicApiControllerSettingsOptions.Value.DefaultRoutePrefix}/health/check");
    }

    /// <summary>
    ///     InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        // 跳过处理的情况：
        if (!context.Request.Path.StartsWithSegments(_defaultRoutePrefix)       // 非api请求
            || context.Request.Path.StartsWithSegments(_healthCheckRoutePrefix) // 健康检查
            || context.Request.Method == Chars.FLG_HTTP_METHOD_OPTIONS) {       // is options 请求
            await _next(context);
            return;
        }

        // Response.Body流默认是不可读的，将Response.Body流替换成MemoryStream 使其可读
        using var ms     = new MemoryStream();
        var       stream = context.Response.Body;
        context.Response.Body = ms;

        // 在控制台上输出分割线，区分不同请求

        // 调用下一个中间件
        var sw = Stopwatch.StartNew();
        await _next(context);
        sw.Stop();

        _ = ms.Seek(0, SeekOrigin.Begin);
        using var sr           = new StreamReader(ms);
        var       responseBody = await sr.ReadToEndAsync();
        _ = ms.Seek(0, SeekOrigin.Begin);
        await ms.CopyToAsync(stream);
        context.Response.Body = stream;

        var exception = context.Features.Get<IExceptionHandlerFeature>();
        var errorCode = context.Response.Headers[nameof(ErrorCodes)] //
                               .FirstOrDefault()
                               ?.Enum<ErrorCodes>() ?? 0;

        _ = await _requestLogger.LogAsync(context, (long)sw.Elapsed.TotalMicroseconds, responseBody, errorCode
                                        , exception);
    }
}