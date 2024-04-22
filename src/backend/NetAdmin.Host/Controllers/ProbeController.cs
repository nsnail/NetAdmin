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
                   };
    }
}