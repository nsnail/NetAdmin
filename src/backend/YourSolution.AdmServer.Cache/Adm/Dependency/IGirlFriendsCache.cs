using NetAdmin.Cache;
using YourSolution.AdmServer.Application.Modules.Adm;
using YourSolution.AdmServer.Application.Services.Adm.Dependency;

namespace YourSolution.AdmServer.Cache.Adm.Dependency;

/// <summary>
///     女朋友缓存
/// </summary>
public interface IGirlFriendsCache : ICache<IDistributedCache, IGirlFriendsService>, IGirlFriendsModule;