namespace NetAdmin.Infrastructure.Rpc.TronScan;

/// <summary>
///     云码平台 客户端
/// </summary>
public interface ITronScanClient : IHttpDispatchProxy
{
    private static readonly ILogger<ITronScanClient> _logger = App.GetService<ILogger<ITronScanClient>>();

    /// <summary>
    ///     异常拦截
    /// </summary>
    [Interceptor(InterceptorTypes.Exception)]
    static Task OnExceptionAsync(HttpClient _, HttpResponseMessage rsp, string errors)
    {
        return rsp.LogExceptionAsync(errors, _logger);
    }

    /// <summary>
    ///     请求拦截
    /// </summary>
    [Interceptor(InterceptorTypes.Request)]
    static Task OnRequestAsyncAsync(HttpClient _, HttpRequestMessage req)
    {
        return req.LogAsync(_logger);
    }

    /// <summary>
    ///     响应拦截
    /// </summary>
    [Interceptor(InterceptorTypes.Response)]
    static Task OnResponsingAsync(HttpClient _, HttpResponseMessage rsp)
    {
        return rsp.LogAsync(_logger);
    }

    /// <summary>
    ///     获取交易记录
    /// </summary>
    [Client(nameof(TronScanOptions))]
    [Get("api/filter/trc20/transfers?limit={limit}&toAddress={toAddress}")]
    Task<TransfersRsp> TransfersAsync([Headers(Chars.FLG_HTTP_HEADER_KEY_TRON_PRO_API_KEY)] string token, int limit, string toAddress);
}