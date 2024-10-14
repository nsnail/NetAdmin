using NetAdmin.Domain.Contexts;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.Domain.Dto.Sys.RequestLogDetail;
using NetAdmin.Domain.Events.Sys;
using Yitter.IdGenerator;
using HttpMethods = NetAdmin.Domain.Enums.HttpMethods;

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
    public async Task<CreateRequestLogReq> LogAsync(HttpContext              context, long duration, string responseBody, ErrorCodes errorCode
                                                  , IExceptionHandlerFeature exception)
    {
        // 从请求头中读取用户信息
        var associatedUser = GetAssociatedUser(context);
        var id             = YitIdHelper.NextId();
        var requestBody = Array.Exists( //
            _textContentTypes, x => context.Request.ContentType?.Contains(x, StringComparison.OrdinalIgnoreCase) ?? false)
            ? await context.ReadBodyContentAsync().ConfigureAwait(false)
            : string.Empty;
        var apiId = context.Request.Path.Value!.TrimStart('/');
        var auditData = new CreateRequestLogReq //
                        {
                            Detail = new CreateRequestLogDetailReq //
                                     {
                                         Id                  = id
                                       , CreatedUserAgent    = context.Request.Headers.UserAgent.ToString()
                                       , ErrorCode           = errorCode
                                       , Exception           = exception?.Error.ToString()
                                       , RequestBody         = requestBody
                                       , RequestContentType  = context.Request.ContentType
                                       , RequestHeaders      = context.Request.Headers.Json()
                                       , RequestUrl          = context.Request.GetRequestUrlAddress()
                                       , ResponseBody        = responseBody
                                       , ResponseContentType = context.Response.ContentType
                                       , ResponseHeaders     = context.Response.Headers.Json()
                                       , ServerIp            = context.GetLocalIpAddressToIPv4()?.IpV4ToInt32()
                                     }
                          , Duration        = (int)duration
                          , HttpMethod      = Enum.Parse<HttpMethods>(context.Request.Method, true)
                          , ApiPathCrc32    = apiId.Crc32()
                          , HttpStatusCode  = context.Response.StatusCode
                          , CreatedClientIp = context.GetRealIpAddress()?.MapToIPv4().ToString().IpV4ToInt32()
                          , OwnerId         = associatedUser?.UserId
                          , OwnerDeptId     = associatedUser?.DeptId
                          , Id              = id
                          , TraceId         = context.GetTraceId()
                        };

        // ReSharper disable once InvertIf
        if (!GlobalStatic.LoggerIgnoreApiIds.Contains(apiId)) {
            // 打印日志
            logger.Info(auditData);

            // 发布请求日志事件
            await eventPublisher.PublishAsync(new RequestLogEvent(auditData)).ConfigureAwait(false);
        }

        return auditData;
    }

    private (long UserId, long DeptId, string UserName)? GetAssociatedUser(HttpContext context)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault();
        if (token == null) {
            return null;
        }

        ContextUserToken userToken = null;
        try {
            userToken = ContextUserToken.Create(token);
        }
        catch (Exception ex) {
            logger.Warn($"{Ln.读取用户令牌出错} {ex}");
        }

        return userToken == null ? null : (userToken.Id, userToken.DeptId, userToken.UserName);
    }
}