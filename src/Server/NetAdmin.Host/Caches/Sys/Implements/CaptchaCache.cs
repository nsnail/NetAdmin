using Furion.FriendlyException;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Sys.Captcha;

namespace NetAdmin.Host.Caches.Sys.Implements;

/// <inheritdoc cref="ICaptchaCache" />
public class CaptchaCache : CacheBase<ICaptchaService>, IScoped, ICaptchaCache
{
    private readonly TimeSpan _absoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);

    /// <summary>
    ///     Initializes a new instance of the <see cref="CaptchaCache" /> class.
    /// </summary>
    public CaptchaCache(IMemoryCache cache, ICaptchaService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public async Task<GetCaptchaRsp> GetCaptchaImageAsync()
    {
        var ret = await Service.GetCaptchaImageAsync();
        _ = Cache.Set(ret.Id, ret.SawOffsetX, _absoluteExpirationRelativeToNow);
        return ret;
    }

    /// <inheritdoc />
    public void RemoveEntry(string id)
    {
        Cache.Remove(id);
    }

    /// <inheritdoc />
    public Task<bool> VerifyCaptchaAsync(VerifyCaptchaReq req)
    {
        _ = Cache.TryGetValue(req.Id, out var val);
        return Service.VerifyCaptchaAsync(req with { SawOffsetX = (int?)val });
    }

    /// <inheritdoc />
    async Task ICaptchaCache.VerifyCaptchaAndRemoveAsync(VerifyCaptchaReq req)
    {
        var ret = await VerifyCaptchaAsync(req);
        if (ret) {
            // 人机验证通过，删除人机验证缓存
            RemoveEntry(req.Id);
        }
        else {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.Man_machine_verification_failed);
        }
    }
}