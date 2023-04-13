using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.Domain.Events;

namespace NetAdmin.Host.Utils;

/// <summary>
///     请求日志记录器
/// </summary>
public sealed class RequestLogger : ISingleton
{
    private readonly IEventPublisher        _eventPublisher;
    private readonly ILogger<RequestLogger> _logger;
    private readonly int                    _tokenPrefixLength;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RequestLogger" /> class.
    /// </summary>
    public RequestLogger( //
        ILogger<RequestLogger>                         logger
      , IOptions<SpecificationDocumentSettingsOptions> specificationDocumentSettingsOptions
      , IEventPublisher                                eventPublisher)
    {
        _logger         = logger;
        _eventPublisher = eventPublisher;
        _tokenPrefixLength = specificationDocumentSettingsOptions?.Value.SecurityDefinitions?[0]?.Scheme?.Length + 1 ??
                             0; // eg. "Bearer "
    }

    /// <summary>
    ///     生成审计数据
    /// </summary>
    public async Task<CreateRequestLogReq> LogAsync(HttpContext context,   long duration, string responseBody
                                                  , ErrorCodes  errorCode, IExceptionHandlerFeature exception)
    {
        // 从请求头中读取用户信息
        var associatedUser = GetAssociatedUser(context);

        var auditData = new CreateRequestLogReq {
                                                    ClientIp            = context.GetRemoteIpAddressToIPv4()
                                                  , Duration            = duration
                                                  , Method              = context.Request.Method
                                                  , ReferUrl            = context.Request.GetRefererUrlAddress()
                                                  , RequestContentType  = context.Request.ContentType
                                                  , RequestBody         = await context.ReadBodyContentAsync()
                                                  , RequestUrl          = context.Request.GetRequestUrlAddress()
                                                  , ResponseBody        = responseBody
                                                  , ServerIp            = context.GetLocalIpAddressToIPv4()
                                                  , UserAgent           = context.Request.Headers["User-Agent"]
                                                  , ApiId               = context.Request.Path.Value?.TrimStart('/')
                                                  , RequestHeaders      = context.Request.Headers.ToJson()
                                                  , ResponseContentType = context.Response.ContentType
                                                  , ResponseHeaders     = context.Response.Headers.ToJson()
                                                  , HttpStatusCode      = context.Response.StatusCode
                                                  , ErrorCode           = errorCode
                                                  , Exception           = exception?.Error.ToString()
                                                  , CreatedUserId       = associatedUser?.UserId
                                                  , CreatedUserName     = associatedUser?.UserName
                                                };

        // 打印日志
        _logger.Info(auditData);

        // 发布请求日志事件
        await _eventPublisher.PublishAsync(new RequestLogEvent(auditData));

        return auditData;
    }

    private (long UserId, string UserName)? GetAssociatedUser(HttpContext context)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault();
        if (token is null) {
            return null;
        }

        ContextUserToken userToken = null;
        try {
            var jsonWebToken = JWTEncryption.ReadJwtToken(token[_tokenPrefixLength..]);
            var claim        = jsonWebToken?.Claims.FirstOrDefault(y => y.Type == nameof(ContextUserToken));
            userToken = claim?.Value.ToObject<ContextUserToken>();
        }
        catch (Exception ex) {
            _logger.Warn($"{Ln.Error_in_reading_the_user_token}: {ex}");
        }

        return userToken is null ? null : (userToken.Id, userToken.UserName);
    }
}