using Furion.RemoteRequest;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     HttpRequestPart 扩展方法
/// </summary>
public static class HttpRequestPartExtensions
{
    /// <summary>
    ///     设置日志
    /// </summary>
    public static HttpRequestPart SetLog<T>(this HttpRequestPart me, ILogger<T> logger
                                          , Func<string, string> bodyHandle = null)
    {
        return me.OnRequesting(RequestHandleAsync).OnResponsing(ResponseHandleAsync).OnException(ExceptionHandleAsync);

        Task ExceptionHandleAsync(HttpClient _, HttpResponseMessage rsp, string errors)
        {
            return rsp.LogExceptionAsync(errors, logger, bodyHandle);
        }

        Task ResponseHandleAsync(HttpClient _, HttpResponseMessage rsp)
        {
            return rsp.LogAsync(logger, bodyHandle);
        }

        Task RequestHandleAsync(HttpClient _, HttpRequestMessage req)
        {
            return req.LogAsync(logger);
        }
    }
}