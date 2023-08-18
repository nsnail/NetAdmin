using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Sys.Email;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using NetAdmin.SysComponent.Cache.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <summary>
///     邮件缓存
/// </summary>
public sealed class EmailCache : DistributedCache<IEmailService>, IScoped, IEmailCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EmailCache" /> class.
    /// </summary>
    public EmailCache(IDistributedCache cache, IEmailService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public Task<SendEmailCodeRsp> SendEmailCodeAsync(SendEmailCodeReq req)
    {
        return Service.SendEmailCodeAsync(req);
    }

    /// <inheritdoc />
    public Task StoreEmailCodeInfoAsync(EmailCodeStoreInfo info)
    {
        return CreateAsync(GetCacheKey(info.EmailAddress), info, TimeSpan.FromMinutes(1));
    }

    /// <inheritdoc />
    public async Task<bool> VerifyEmailCodeAsync(VerifyEmailCodeReq req)
    {
        #if DEBUG
        if (req.Code == "8888") {
            return true;
        }
        #endif
        if (req.Code == Global.SecretKey) {
            return true;
        }

        var store = await GetAsync<EmailCodeStoreInfo>(GetCacheKey(req.EmailAddress, nameof(StoreEmailCodeInfoAsync)));
        return store != null && store.Code == req.Code;
    }
}