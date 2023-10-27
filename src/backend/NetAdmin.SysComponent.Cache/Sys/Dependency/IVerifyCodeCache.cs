using NetAdmin.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     验证码缓存
/// </summary>
public interface IVerifyCodeCache : ICache<IDistributedCache, IVerifyCodeService>, IVerifyCodeModule { }