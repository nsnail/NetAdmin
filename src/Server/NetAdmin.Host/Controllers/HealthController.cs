using NetAdmin.Application.Services;

namespace NetAdmin.Host.Controllers;

/// <summary>
///     健康控制器
/// </summary>
[ApiDescriptionSettings("Health")]
public sealed class HealthController : ControllerBase<IService>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HealthController" /> class.
    /// </summary>
    public HealthController(IService service) //
        : base(service) { }

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