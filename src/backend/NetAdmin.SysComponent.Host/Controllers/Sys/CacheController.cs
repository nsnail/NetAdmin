using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.Host.Controllers;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     缓存服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
public sealed class CacheController : ControllerBase<ICacheService>, ICacheModule
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CacheController" /> class.
    /// </summary>
    public CacheController(ICacheService service) //
        : base(service) { }

    /// <summary>
    ///     缓存统计
    /// </summary>
    public CacheStatisticsRsp CacheStatistics()
    {
        return Service.CacheStatistics();
    }

    /// <summary>
    ///     清空缓存
    /// </summary>
    public void Clear()
    {
        Service.Clear();
    }
}