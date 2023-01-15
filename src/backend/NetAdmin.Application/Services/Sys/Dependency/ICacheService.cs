using NetAdmin.Application.Modules.Sys;

namespace NetAdmin.Application.Services.Sys.Dependency;

/// <summary>
///     缓存服务
/// </summary>
public interface ICacheService : IService, ICacheModule
{
    /// <summary>
    ///     获取所有缓存项
    /// </summary>
    List<GetAllEntrysRsp> GetAllEntrys();
}