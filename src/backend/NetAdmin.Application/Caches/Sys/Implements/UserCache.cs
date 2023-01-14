using Furion.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using NetAdmin.DataContract;
using NetAdmin.DataContract.Dto.Sys.User;

namespace NetAdmin.Application.Caches.Sys.Implements;

/// <inheritdoc cref="NetAdmin.Application.Caches.Sys.IUserCache" />
public class UserCache : CacheBase, IScoped, IUserCache
{
    private readonly TimeSpan    _absoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
    private readonly TimeSpan    _slidingExpiration               = TimeSpan.FromMinutes(5);
    private readonly ContextUser _user;

    /// <summary>
    ///     Initializes a new instance of the <see cref="UserCache" /> class.
    /// </summary>
    public UserCache(IMemoryCache memoryCache, ContextUser user) //
        : base(memoryCache)
    {
        _user = user;
    }

    /// <inheritdoc />
    public async Task<QueryUserRsp> UserInfo(Func<Task<QueryUserRsp>> create)
    {
        return await Cache.GetOrCreateAsync($"{nameof(UserCache)}.{nameof(UserInfo)}.{_user.Id}", async entry => {
            SetExpires(entry);
            return await create();
        });
    }

    private void SetExpires(ICacheEntry entry)
    {
        entry.SetSlidingExpiration(_slidingExpiration);
        entry.AbsoluteExpirationRelativeToNow = _absoluteExpirationRelativeToNow;
    }
}