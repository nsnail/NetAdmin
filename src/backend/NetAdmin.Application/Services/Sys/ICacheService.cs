using NetAdmin.DataContract.Dto.Sys.Cache;

namespace NetAdmin.Application.Services.Sys;

/// <summary>
///     缓存服务
/// </summary>
public interface ICacheService : IService
{
    /// <summary>
    ///     缓存统计
    /// </summary>
    CacheStatisticsRsp CacheStatistics();

    /// <summary>
    ///     清空缓存
    /// </summary>
    void Clear();

    /// <summary>
    ///     获取所有缓存项
    /// </summary>
    List<GetAllEntrysRsp> GetAllEntrys();
}