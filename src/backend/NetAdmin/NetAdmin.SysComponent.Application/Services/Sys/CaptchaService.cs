using NetAdmin.Domain.Dto.Sys.Captcha;
using SixLabors.ImageSharp;
using Yitter.IdGenerator;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ICaptchaService" />
public sealed class CaptchaService : ServiceBase<ICaptchaService>, ICaptchaService
{
    private static readonly Assembly _currAsm     = Assembly.GetAssembly(typeof(CaptchaService));
    private static readonly string   _currAsmName = _currAsm.FullName![.._currAsm.FullName.IndexOf(',')];

    /// <inheritdoc />
    public async Task<GetCaptchaRsp> GetCaptchaImageAsync()
    {
        var (backgroundImage, sliderImage, offsetSaw) = await CaptchaImageHelper.CreateSawSliderImageAsync(
                                                                                    _currAsm, $"{_currAsmName}.Assets.Captcha.background"
                                                                                  , $"{_currAsmName}.Assets.Captcha.template", (1, 101), (1, 7)
                                                                                  , new Size(50, 50))
                                                                                .ConfigureAwait(false);

        var id = $"{nameof(GetCaptchaImageAsync)}_{YitIdHelper.NextId()}";
        return new GetCaptchaRsp { Id = id, BackgroundImage = backgroundImage, SliderImage = sliderImage, SawOffsetX = offsetSaw.X };
    }

    /// <inheritdoc />
    public Task<bool> VerifyCaptchaAsync(VerifyCaptchaReq req)
    {
        req.ThrowIfInvalid();
        if (req.SawOffsetX == null) {
            return Task.FromResult(false);
        }

        bool ret;
        try {
            var aesKey = req.Id.Aes(CaptchaOptions.SecretKey)[..32];
            ret = Math.Abs(req.SawOffsetX.Value - req.VerifyData.AesDe(aesKey).Float()) < 5f;
        }
        catch {
            ret = false;
        }

        return Task.FromResult(ret);
    }
}