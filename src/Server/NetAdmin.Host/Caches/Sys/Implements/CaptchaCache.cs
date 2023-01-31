using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.Extensions.Caching.Memory;
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
    public async Task<GetCaptchaRsp> GetCaptchaImage()
    {
        var ret = await Service.GetCaptchaImage();
        Cache.Set(ret.Id, ret.SawOffsetX, _absoluteExpirationRelativeToNow);
        return ret;
    }

    /// <inheritdoc />
    public void RemoveEntry(string id)
    {
        Cache.Remove(id);
    }

    /// <inheritdoc />
    public Task<bool> VerifyCaptcha(VerifyCaptchaReq req)
    {
        Cache.TryGetValue(req.Id, out var val);
        return Service.VerifyCaptcha(req with { SawOffsetX = (int?)val });
    }

    /// <inheritdoc />
    public async Task VerifyCaptchaAndRemove(VerifyCaptchaReq req)
    {
        var ret = await VerifyCaptcha(req);
        if (ret) {
            // 人机验证通过，删除人机验证缓存
            RemoveEntry(req.Id);
        }
        else {
            throw Oops.Oh(Enums.RspCodes.InvalidOperation, Ln.Man_machine_verification_failed);
        }
    }
}