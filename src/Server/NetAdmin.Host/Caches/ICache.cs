using NetAdmin.Application.Services;

namespace NetAdmin.Host.Caches;

/// <summary>
///     缓存接口
/// </summary>
public interface ICache<out TService>
    where TService : IService
{
    /// <summary>
    ///     缓存对象
    /// </summary>
    IMemoryCache Cache { get; }

    /// <summary>
    ///     关联的服务
    /// </summary>
    public TService Service { get; }
}