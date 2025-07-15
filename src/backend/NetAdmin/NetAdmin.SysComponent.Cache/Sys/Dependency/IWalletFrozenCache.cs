namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     钱包冻结缓存
/// </summary>
public interface IWalletFrozenCache : ICache<IDistributedCache, IWalletFrozenService>, IWalletFrozenModule;