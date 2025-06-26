namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     用户钱包缓存
/// </summary>
public interface IUserWalletCache : ICache<IDistributedCache, IUserWalletService>, IUserWalletModule;