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
        #pragma warning disable S1172

        Task RequestHandle(HttpClient _, HttpRequestMessage req)
        {
            return req.LogAsync(logger);
        }

        Task ExceptionHandle(HttpClient _, HttpResponseMessage rsp, string errors)
        {
            return rsp.LogExceptionAsync(errors, logger, bodyHandle);
        }

        Task ResponseHandle(HttpClient _, HttpResponseMessage rsp)
        {
            return rsp.LogAsync(logger, bodyHandle);
        }

        #pragma warning restore S1172

        return me.OnRequesting(RequestHandle).OnResponsing(ResponseHandle).OnException(ExceptionHandle);
    }
}