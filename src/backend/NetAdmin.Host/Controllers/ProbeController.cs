using NetAdmin.Application.Services;
using NetAdmin.Cache;
using NetAdmin.Host.Middlewares;

namespace NetAdmin.Host.Controllers;

/// <summary>
///     探针组件
/// </summary>
[ApiDescriptionSettings("Probe")]
public sealed class ProbeController : ControllerBase<ICache<IDistributedCache, IService>, IService>
{
    /// <summary>
    ///     退出程序
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    #pragma warning disable CA1822
    public ActionResult Exit(string token)
        #pragma warning restore CA1822
    {
        if (token != GlobalStatic.SecretKey) {
            return new UnauthorizedResult();
        }

        Environment.Exit(0);
        return new OkResult();
    }

    /// <summary>
    ///     健康检查
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    #pragma warning disable CA1822, S3400
    public object HealthCheck()
        #pragma warning restore S3400, CA1822
    {
        return new {
                       HostName           = Environment.MachineName
                     , CurrentConnections = SafetyShopHostMiddleware.Connections
                     , GlobalStatic.ProductVersion
                     , ThreadCounts = GlobalStatic.CurrentProcess.Threads.Count
                     , GlobalStatic.LatestLogTime
                   };
    }

    /// <summary>
    ///     系统是否已经安全停止
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    [NonUnify]
    #pragma warning disable CA1822, S3400
    public IActionResult IsSystemSafetyStopped(int logTimeoutSeconds = 15)
        #pragma warning restore S3400, CA1822
    {
        return new ContentResult {
                                     Content = (DateTime.Now - GlobalStatic.LatestLogTime).TotalSeconds >
                                               logTimeoutSeconds
                                         ? "1"
                                         : "0"
                                 };
    }

    /// <summary>
    ///     实例下线
    /// </summary>
    /// <remarks>
    ///     流量只出不进
    /// </remarks>
    [AllowAnonymous]
    [HttpGet]
    #pragma warning disable CA1822
    public ActionResult Offline(string token)
        #pragma warning restore CA1822
    {
        if (token != GlobalStatic.SecretKey) {
            return new UnauthorizedResult();
        }

        SafetyShopHostMiddleware.Stop();
        return new OkResult();
    }
}