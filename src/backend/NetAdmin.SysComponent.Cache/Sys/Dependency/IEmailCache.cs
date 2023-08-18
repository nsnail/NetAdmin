using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Sys.Email;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Dependency;

/// <summary>
///     邮件缓存
/// </summary>
public interface IEmailCache : ICache<IDistributedCache, IEmailService>, IEmailModule
{
    Task StoreEmailCodeInfoAsync(EmailCodeStoreInfo info);
}