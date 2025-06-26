namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     钱包交易缓存
/// </summary>
public interface IWalletTradeCache : ICache<IDistributedCache, IWalletTradeService>, IWalletTradeModule;