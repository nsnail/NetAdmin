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
        async void RequestHandle(HttpClient _, HttpRequestMessage req)
        {
            #pragma warning disable IDE0058
            await req.LogAsync(logger);
            #pragma warning restore IDE0058
        }

        async void ResponseHandle(HttpClient _, HttpResponseMessage rsp)
        {
            await rsp.LogAsync(logger, bodyHandle);
        }

        async void ExceptionHandle(HttpClient _, HttpResponseMessage rsp, string errors)
        {
            await rsp.LogExceptionAsync(errors, logger, bodyHandle);
        }

        return me.OnRequesting(RequestHandle).OnResponsing(ResponseHandle).OnException(ExceptionHandle);
    }
}