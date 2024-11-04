using NetAdmin.SysComponent.Domain.Dto.Sys.Captcha;

namespace NetAdmin.SysComponent.Cache.Sys;

/// <inheritdoc cref="ICaptchaCache" />
public sealed class CaptchaCache(IDistributedCache cache, ICaptchaService service) //
    : DistributedCache<ICaptchaService>(cache, service), IScoped, ICaptchaCache
{
    /// <inheritdoc />
    public async Task<GetCaptchaRsp> GetCaptchaImageAsync()
    {
        var captchaRsp = await Service.GetCaptchaImageAsync().ConfigureAwait(false);
        await CreateAsync(GetCacheKey(captchaRsp.Id, nameof(CaptchaCache)), captchaRsp.SawOffsetX
,                                                                           TimeSpan.FromSeconds(SysNumbers.SECS_CACHE_DEFAULT))
            .ConfigureAwait(false);
        return captchaRsp;
    }

    /// <inheritdoc />
    /// <exception cref="NetAdminInvalidOperationException">人机验证未通过</exception>
    public async Task VerifyCaptchaAndRemoveAsync(VerifyCaptchaReq req)
    {
        var ret = await VerifyCaptchaAsync(req).ConfigureAwait(false);
        if (ret) {
            // 人机验证通过，删除人机验证缓存
            await RemoveAsync(GetCacheKey(req.Id, nameof(CaptchaCache))).ConfigureAwait(false);
        }
        else {
            throw new NetAdminInvalidOperationException(Ln.人机验证未通过);
        }
    }

    /// <inheritdoc />
    public async Task<bool> VerifyCaptchaAsync(VerifyCaptchaReq req)
    {
        var val = await GetAsync<int?>(GetCacheKey(req.Id, nameof(CaptchaCache))).ConfigureAwait(false);
        return await Service.VerifyCaptchaAsync(req with { SawOffsetX = val }).ConfigureAwait(false);
    }
}