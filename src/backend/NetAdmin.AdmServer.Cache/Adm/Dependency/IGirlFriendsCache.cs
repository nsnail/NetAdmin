using NetAdmin.AdmServer.Application.Modules.Adm;
using NetAdmin.AdmServer.Application.Services.Adm.Dependency;
using NetAdmin.Cache;

namespace NetAdmin.AdmServer.Cache.Adm.Dependency;

/// <summary>
///     女朋友缓存
/// </summary>
public interface IGirlFriendsCache : ICache<IDistributedCache, IGirlFriendsService>, IGirlFriendsModule;