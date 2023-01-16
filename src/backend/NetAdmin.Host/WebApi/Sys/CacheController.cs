using NetAdmin.Application.Modules.Sys;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.DataContract.Dto.Sys.Cache;

namespace NetAdmin.Host.WebApi.Sys;

/// <summary>
///     缓存服务
/// </summary>
public class CacheController : ControllerBase<ICacheService>, ICacheModule
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