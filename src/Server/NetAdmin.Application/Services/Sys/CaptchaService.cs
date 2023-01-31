using Microsoft.Extensions.Options;
using NetAdmin.Application.Services.Sys.Dependency;
using NetAdmin.Domain.Dto.Sys.Captcha;
using NSExt.Extensions;
using SixLabors.ImageSharp;
using Yitter.IdGenerator;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="ICaptchaService" />
public class CaptchaService : ServiceBase<ICaptchaService>, ICaptchaService
{
    private readonly CaptchaOptions _captchaOptions;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CaptchaService" /> class.
    /// </summary>
    public CaptchaService(IOptions<CaptchaOptions> captchaOptions)
    {
        _captchaOptions = captchaOptions.Value;
    }

    /// <inheritdoc />
    public async Task<GetCaptchaRsp> GetCaptchaImage()
    {
        var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _captchaOptions.ImageRelativePath);

        var (backgroundImage, sliderImage, offsetSaw) = await CaptchaImageHelper.CreateSawSliderImage(
            Path.Combine(baseDir, "background"), Path.Combine(baseDir, "template"), (1, 101), (1, 7), new Size(50, 50));

        var id = $"{nameof(GetCaptchaImage)}_{YitIdHelper.NextId()}";
        var captchaData = new GetCaptchaRsp {
                                                Id              = id
                                              , BackgroundImage = backgroundImage
                                              , SliderImage     = sliderImage
                                              , SawOffsetX      = offsetSaw.X
                                            };

        return captchaData;
    }

    /// <inheritdoc />
    public Task<bool> VerifyCaptcha(VerifyCaptchaReq req)
    {
        if (req.SawOffsetX is null) {
            return Task.FromResult(false);
        }

        bool ret;
        try {
            var aesKey = req.Id.Aes(_captchaOptions.SecretKey)[..32];
            ret = Math.Abs(req.SawOffsetX.Value - req.VerifyData.AesDe(aesKey).Float()) < 5f;
        }
        catch {
            ret = false;
        }

        return Task.FromResult(ret);
    }
}