using System.Diagnostics;
using Furion.DataEncryption;
using Furion.DynamicApiController;
using Furion.EventBus;
using Furion.SpecificationDocument;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using NetAdmin.DataContract.Dto.Sys.RequestLog;
using NetAdmin.Host.Events.Sources;
using NSExt.Extensions;
using Spectre.Console;

namespace NetAdmin.Host.Aop.Middlewares;

/// <summary>
///     请求审计中间件
/// </summary>
/// <remarks>
///     放在所有中间件最前面
/// </remarks>
public class RequestAuditMiddleware
{
    private readonly PathString                      _defaultRoutePrefix;
    private readonly IEventPublisher                 _eventPublisher;
    private readonly ILogger<RequestAuditMiddleware> _logger;
    private readonly RequestDelegate                 _next;
    private readonly int                             _tokenPrefxLength;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestAuditMiddleware" /> class.
    /// </summary>
    public RequestAuditMiddleware( //
        RequestDelegate next, IOptions<DynamicApiControllerSettingsOptions> dynamicApiControllerSettingsOptions
      , IEventPublisher eventPublisher
      , IOptions<SpecificationDocumentSettingsOptions> specificationDocumentSettingsOptions
      , ILogger<RequestAuditMiddleware> logger)
    {
        _next               = next;
        _eventPublisher     = eventPublisher;
        _logger             = logger;
        _defaultRoutePrefix = new PathString($"/{dynamicApiControllerSettingsOptions.Value.DefaultRoutePrefix}");
        _tokenPrefxLength
            = specificationDocumentSettingsOptions.Value.SecurityDefinitions.First().Scheme.Length + 1; // eg. "Bearer "
    }

    /// <summary>
    ///     InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        // 跳过处理的情况：
        if (!context.Request.Path.StartsWithSegments(_defaultRoutePrefix) // 非api请求
            || context.Request.Method == Chars.FLG_HTTP_METHOD_OPTIONS) { // is options 请求
            await _next(context);
            return;
        }

        using var ms     = new MemoryStream();
        var       stream = context.Response.Body;
        context.Response.Body = ms;

        // 在控制台上输出分割线，区分不同请求
        #if DEBUG
        AnsiConsole.Write(new Rule($"[{nameof(ConsoleColor.Yellow)} bold]{context.Request.Path}[/]")
                          .RuleStyle(nameof(ConsoleColor.Yellow))
                          .LeftJustified());
        #endif

        // 调用下一个中间件
        var sw = Stopwatch.StartNew();
        await _next(context);
        sw.Stop();

        ms.Seek(0, SeekOrigin.Begin);
        using var sr           = new StreamReader(ms);
        var       responseBody = await sr.ReadToEndAsync();
        ms.Seek(0, SeekOrigin.Begin);
        await ms.CopyToAsync(stream);
        context.Response.Body = stream;

        var exception = context.Features.Get<IExceptionHandlerFeature>();
        var auditData = new CreateRequestLogReq {
                                                    ClientIp            = context.GetRemoteIpAddressToIPv4()
                                                  , Duration            = (uint)sw.ElapsedMilliseconds
                                                  , Method              = context.Request.Method
                                                  , ReferUrl            = context.Request.GetRefererUrlAddress()
                                                  , RequestContentType  = context.Request.ContentType
                                                  , RequestBody         = await context.ReadBodyContentAsync()
                                                  , RequestUrl          = context.Request.GetRequestUrlAddress()
                                                  , ResponseBody        = responseBody
                                                  , ServerIp            = context.GetLocalIpAddressToIPv4()
                                                  , UserAgent           = context.Request.Headers["User-Agent"]
                                                  , ApiId               = context.Request.Path.Value?.TrimStart('/')
                                                  , RequestHeaders      = context.Request.Headers.Json()
                                                  , ResponseContentType = context.Response.ContentType
                                                  , ResponseHeaders     = context.Response.Headers.Json()
                                                  , StatusCode          = context.Response.StatusCode
                                                  , Exception           = exception?.Error.ToString()
                                                };

        // 从请求头中读取用户信息
        var token = context.Request.Headers.Authorization.FirstOrDefault();
        if (token is not null) {
            try {
                var jsonWebToken = JWTEncryption.ReadJwtToken(token[_tokenPrefxLength..]);
                var claim        = jsonWebToken?.Claims.FirstOrDefault(y => y.Type == nameof(ContextUser));
                var user         = claim?.Value.Object<ContextUser>();
                if (user is not null) {
                    auditData.CreatedUserId   = user.Id;
                    auditData.CreatedUserName = user.UserName;
                }
            }
            catch (Exception ex) {
                _logger.Error($"{Ln.Error_in_reading_the_user_token}: {ex}");
            }
        }

        // 发布审计事件
        await _eventPublisher.PublishAsync(new OperationEvent(auditData));
    }
}