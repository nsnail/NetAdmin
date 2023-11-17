using NetAdmin.Application.Services;
using NetAdmin.Cache;

namespace NetAdmin.Host.Controllers;

/// <summary>
///     健康控制器
/// </summary>
[ApiDescriptionSettings("Health")]
public sealed class HealthController
    (ICache<IDistributedCache, IService> cache) : ControllerBase<ICache<IDistributedCache, IService>, IService>(cache)
{
    /// <summary>
    ///     健康检查
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    #pragma warning disable CA1822, S3400
    public string Check()
        #pragma warning restore S3400, CA1822
    {
        return GlobalStatic.ProductVersion;
    }
}