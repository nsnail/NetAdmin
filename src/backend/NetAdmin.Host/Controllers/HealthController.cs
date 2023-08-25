using NetAdmin.Application.Services;
using NetAdmin.Cache;

namespace NetAdmin.Host.Controllers;

/// <summary>
///     健康控制器
/// </summary>
[ApiDescriptionSettings("Health")]
public sealed class HealthController : ControllerBase<ICache<IDistributedCache, IService>, IService>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HealthController" /> class.
    /// </summary>
    public HealthController(ICache<IDistributedCache, IService> cache) //
        : base(cache) { }

    /// <summary>
    ///     健康检查
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    #pragma warning disable CA1822, S3400
    public string Check()
        #pragma warning restore S3400, CA1822
    {
        return Global.ProductVersion;
    }
}