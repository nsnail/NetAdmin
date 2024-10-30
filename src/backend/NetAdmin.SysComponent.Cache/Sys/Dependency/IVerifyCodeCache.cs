namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     验证码缓存
/// </summary>
public interface IVerifyCodeCache : ICache<IDistributedCache, IVerifyCodeService>, IVerifyCodeModule;