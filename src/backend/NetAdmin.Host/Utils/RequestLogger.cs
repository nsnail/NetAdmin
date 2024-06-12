using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.Domain.Events.Sys;

namespace NetAdmin.Host.Utils;

/// <summary>
///     请求日志记录器
/// </summary>
public sealed class RequestLogger(ILogger<RequestLogger> logger, IEventPublisher eventPublisher) : ISingleton
{
    private static readonly string[] _textContentTypes = ["text", "json", "xml", "urlencoded"];

    /// <summary>
    ///     生成审计数据
    /// </summary>
    public async Task<CreateRequestLogReq> LogAsync(HttpContext context,   long duration, string responseBody
                                                  , ErrorCodes  errorCode, IExceptionHandlerFeature exception)
    {
        // 从请求头中读取用户信息
        var associatedUser = GetAssociatedUser(context);

        var auditData = new CreateRequestLogReq {
                                                    Duration           = duration
                                                  , Method             = context.Request.Method
                                                  , RequestContentType = context.Request.ContentType
                                                  , RequestBody = Array.Exists( //
                                                        _textContentTypes
                                                      , x => context.Request.ContentType?.Contains(
                                                            x, StringComparison.OrdinalIgnoreCase) ?? false)
                                                        ? (await context.ReadBodyContentAsync().ConfigureAwait(false))
                                                        ?.Sub(0, Numbers.MAX_LIMIT_PRINT_LEN_CONTENT)
                                                        : string.Empty
                                                  , RequestUrl = context.Request.GetRequestUrlAddress()
                                                  , ResponseBody
                                                        = responseBody?.Sub(0, Numbers.MAX_LIMIT_PRINT_LEN_CONTENT)
                                                  , ServerIp = context.GetLocalIpAddressToIPv4()?.IpV4ToInt32()
                                                  , ApiId = context.Request.Path.Value?.TrimStart('/')
                                                  , RequestHeaders = context.Request.Headers.Json()
                                                  , ResponseContentType = context.Response.ContentType
                                                  , ResponseHeaders = context.Response.Headers.Json()
                                                  , HttpStatusCode = context.Response.StatusCode
                                                  , ErrorCode = errorCode
                                                  , Exception = exception?.Error.ToString()
                                                  , UserId = associatedUser?.UserId
                                                  , CreatedUserAgent = context.Request.Headers.UserAgent.ToString()
                                                  , CreatedClientIp = context.GetRealIpAddress()
                                                                             ?.MapToIPv4()
                                                                             .ToString()
                                                                             .IpV4ToInt32()
                                                };

        // 打印日志
        logger.Info(auditData);

        // 发布请求日志事件
        await eventPublisher.PublishAsync(new RequestLogEvent(auditData)).ConfigureAwait(false);

        return auditData;
    }

    private (long UserId, string UserName)? GetAssociatedUser(HttpContext context)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault();
        if (token == null) {
            return null;
        }

        ContextUserToken userToken = null;
        try {
            var jsonWebToken
                = JWTEncryption.ReadJwtToken(token.TrimStart($"{Chars.FLG_HTTP_HEADER_VALUE_AUTH_SCHEMA} "));
            var claim = jsonWebToken?.Claims.FirstOrDefault(y => y.Type == nameof(ContextUserToken));
            userToken = claim?.Value.ToObject<ContextUserToken>();
        }
        catch (Exception ex) {
            logger.Warn($"{Ln.读取用户令牌出错}: {ex}");
        }

        return userToken == null ? null : (userToken.Id, userToken.UserName);
    }
}