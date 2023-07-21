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

    /// <summary>
    ///     获取人机校验图
    /// </summary>
    public async Task<GetCaptchaRsp> GetCaptchaImageAsync()
    {
        var captchaRsp = await Service.GetCaptchaImageAsync();
        await CreateAsync(GetCacheKey(captchaRsp.Id, nameof(CaptchaCache)), captchaRsp.SawOffsetX
,                                                                           TimeSpan.FromMinutes(1));
        return captchaRsp;
    }

    /// <summary>
    ///     完成人机校验 ，并删除缓存项
    /// </summary>
    /// <exception cref="NetAdminInvalidOperationException">人机验证未通过</exception>
    public async Task VerifyCaptchaAndRemoveAsync(VerifyCaptchaReq req)
    {
        var ret = await VerifyCaptchaAsync(req);
        if (ret) {
            // 人机验证通过，删除人机验证缓存
            await RemoveAsync(GetCacheKey(req.Id, nameof(CaptchaCache)));
        }
        else {
            throw new NetAdminInvalidOperationException(Ln.人机校验未通过);
        }
    }

    /// <summary>
    ///     完成人机校验
    /// </summary>
    public async Task<bool> VerifyCaptchaAsync(VerifyCaptchaReq req)
    {
        var val = await GetAsync<int?>(GetCacheKey(req.Id, nameof(CaptchaCache)));
        return await Service.VerifyCaptchaAsync(req with { SawOffsetX = val });
    }
}