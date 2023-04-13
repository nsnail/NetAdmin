using NetAdmin.Cache;
using NetAdmin.Domain.Dto.Sys.Captcha;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;

namespace NetAdmin.SysComponent.Cache.Sys.Implements;

/// <inheritdoc cref="ICaptchaCache" />
public sealed class CaptchaCache : DistributedCache<ICaptchaService>, IScoped, ICaptchaCache
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CaptchaCache" /> class.
    /// </summary>
    public CaptchaCache(IDistributedCache cache, ICaptchaService service) //
        : base(cache, service) { }

    /// <inheritdoc />
    public async Task<GetCaptchaRsp> GetCaptchaImageAsync()
    {
        var captchaRsp = await Service.GetCaptchaImageAsync();
        await CreateAsync(GetCacheKey(captchaRsp.Id), captchaRsp.SawOffsetX, TimeSpan.FromMinutes(1));
        return captchaRsp;
    }

    /// <inheritdoc />
    public async Task VerifyCaptchaAndRemoveAsync(VerifyCaptchaReq req)
    {
        var ret = await VerifyCaptchaAsync(req);
        if (ret) {
            // 人机验证通过，删除人机验证缓存
            await RemoveAsync(GetCacheKey(req.Id));
        }
        else {
            throw new LineInvalidOperationException(Ln.Man_machine_verification_failed);
        }
    }

    /// <inheritdoc />
    public async Task<bool> VerifyCaptchaAsync(VerifyCaptchaReq req)
    {
        var cacheKey = GetCacheKey(req.Id);
        var val      = await GetAsync<int?>(cacheKey);
        return await Service.VerifyCaptchaAsync(req with { SawOffsetX = val });
    }

    private string GetCacheKey(string id)
    {
        return $"{GetType().FullName}.{nameof(GetCaptchaImageAsync)}.{id}";
    }
}