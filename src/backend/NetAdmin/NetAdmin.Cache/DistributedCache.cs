using System.Runtime.CompilerServices;
using NetAdmin.Application.Services;

namespace NetAdmin.Cache;

/// <summary>
///     分布式缓存
/// </summary>
public abstract class DistributedCache<TService>(IDistributedCache cache, TService service) : CacheBase<IDistributedCache, TService>(cache, service)
    where TService : IService
{
    /// <summary>
    ///     创建缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <param name="createObj">创建对象</param>
    /// <param name="absLifeTime">绝对过期时间</param>
    /// <param name="slideLifeTime">滑动过期时间</param>
    /// <typeparam name="T">缓存对象类型</typeparam>
    /// <returns>缓存对象</returns>
    protected Task CreateAsync<T>(
        string key
        , T createObj
        , TimeSpan? absLifeTime = null
        , TimeSpan? slideLifeTime = null
    ) {
        var cacheWrite = createObj.ToJson();

        var options = new DistributedCacheEntryOptions();
        if (absLifeTime != null) {
            _ = options.SetAbsoluteExpiration(absLifeTime.Value);
        }

        if (slideLifeTime != null) {
            _ = options.SetSlidingExpiration(slideLifeTime.Value);
        }

        return Cache.SetAsync(key, cacheWrite.Hex(), options);
    }

    /// <summary>
    ///     获取缓存
    /// </summary>
    protected async Task<T> GetAsync<T>(string key) {
        var cacheRead = await Cache.GetStringAsync(key).ConfigureAwait(false);
        try {
            return cacheRead != null ? cacheRead.ToObject<T>() : default;
        }
        catch (JsonException) {
            return default;
        }
    }

    /// <summary>
    ///     获取缓存键
    /// </summary>
    protected string GetCacheKey(
        string id = "0"
        , [CallerMemberName] string memberName = null
    ) {
        return $"{GetType().FullName}.{memberName}.{id}";
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
    protected async Task<T> GetOrCreateAsync<T>(
        string key
        , Func<Task<T>> createProc
        , TimeSpan? absLifeTime = null
        , TimeSpan? slideLifeTime = null
    ) {
        var cacheRead = await GetAsync<T>(key).ConfigureAwait(false);
        if (cacheRead is not null && App.HttpContext?.Request.Headers.CacheControl.FirstOrDefault() != Chars.FLG_HTTP_HEADER_VALUE_NO_CACHE) {
            return cacheRead;
        }

        var obj = await createProc.Invoke().ConfigureAwait(false);

        var cacheWrite = obj?.ToJson();
        if (cacheWrite == null) {
            return obj;
        }

        await CreateAsync(key, obj, absLifeTime, slideLifeTime).ConfigureAwait(false);
        return obj;
    }

    /// <summary>
    ///     删除缓存
    /// </summary>
    protected Task RemoveAsync(string key) {
        return Cache.RemoveAsync(key);
    }
}