using NetAdmin.Application.Services;

namespace NetAdmin.Cache;

/// <summary>
///     分布式缓存
/// </summary>
public abstract class DistributedCache<TService> : CacheBase<IDistributedCache, TService>
    where TService : IService
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DistributedCache{TService}" /> class.
    /// </summary>
    protected DistributedCache(IDistributedCache cache, TService service) //
        : base(cache, service) { }

    /// <summary>
    ///     创建缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="createObj">创建对象</param>
    /// <param name="absLifeTime">绝对过期时间</param>
    /// <param name="slideLifeTime">滑动过期时间</param>
    /// <typeparam name="T">缓存对象类型</typeparam>
    /// <returns>缓存对象</returns>
    protected Task CreateAsync<T>(string key, T createObj, TimeSpan? absLifeTime = null, TimeSpan? slideLifeTime = null)
    {
        var cacheWrite = createObj.ToJson();

        var options = new DistributedCacheEntryOptions();
        if (absLifeTime is not null) {
            _ = options.SetAbsoluteExpiration(absLifeTime.Value);
        }

        if (slideLifeTime is not null) {
            _ = options.SetSlidingExpiration(slideLifeTime.Value);
        }

        return Cache.SetAsync(key, cacheWrite.Hex(), options);
    }

    /// <summary>
    ///     获取缓存
    /// </summary>
    protected async Task<T> GetAsync<T>(string key)
    {
        var cacheRead = await Cache.GetStringAsync(key);
        return cacheRead is not null ? cacheRead.ToObject<T>() : default;
    }

    /// <summary>
    ///     获取或创建缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="createProc">创建函数</param>
    /// <param name="absLifeTime">绝对过期时间</param>
    /// <param name="slideLifeTime">滑动过期时间</param>
    /// <typeparam name="T">缓存对象类型</typeparam>
    /// <returns>缓存对象</returns>
    protected async Task<T> GetOrCreateAsync<T>(string    key, Func<Task<T>> createProc, TimeSpan? absLifeTime = null
                                              , TimeSpan? slideLifeTime = null)
    {
        var cacheRead = await GetAsync<T>(key);
        if (cacheRead is not null) {
            return cacheRead;
        }

        var obj = await createProc.Invoke();

        var cacheWrite = obj?.ToJson();
        if (cacheWrite is null) {
            return obj;
        }

        await CreateAsync(key, obj, absLifeTime, slideLifeTime);
        return obj;
    }

    /// <summary>
    ///     删除缓存
    /// </summary>
    protected Task RemoveAsync(string key)
    {
        return Cache.RemoveAsync(key);
    }
}