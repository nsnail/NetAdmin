using NetAdmin.Application.Services;

namespace NetAdmin.Cache;

/// <summary>
///     缓存接口
/// </summary>
public interface ICache<out TCacheLoad, out TService>
    where TService : IService
{
    /// <summary>
    ///     缓存对象
    /// </summary>
    TCacheLoad Cache { get; }

    /// <summary>
    ///     关联的服务
    /// </summary>
    public TService Service { get; }
}