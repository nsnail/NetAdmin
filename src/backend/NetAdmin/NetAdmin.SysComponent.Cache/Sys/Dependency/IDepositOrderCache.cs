namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     充值订单缓存
/// </summary>
public interface IDepositOrderCache : ICache<IDistributedCache, IDepositOrderService>, IDepositOrderModule;