using NetAdmin.Application.Services;
using NetAdmin.Domain.Dto.Sys.Captcha;
using NetAdmin.SysComponent.Application.Services.Sys.Dependency;
using SixLabors.ImageSharp;
using Yitter.IdGenerator;

namespace NetAdmin.SysComponent.Application.Services.Sys;

/// <inheritdoc cref="ICaptchaService" />
public sealed class CaptchaService : ServiceBase<ICaptchaService>, ICaptchaService
{
    private static readonly Assembly _entryAsm     = Assembly.GetEntryAssembly();
    private static readonly string   _entryAsmName = _entryAsm.FullName![.._entryAsm.FullName.IndexOf(',')];

    /// <summary>
    ///     Initializes a new instance of the <see cref="CaptchaService" /> class.
    /// </summary>
    public CaptchaService() { }

    /// <inheritdoc />
    public async Task<GetCaptchaRsp> GetCaptchaImageAsync()
    {
        var (backgroundImage, sliderImage, offsetSaw) = await CaptchaImageHelper.CreateSawSliderImageAsync(
                _entryAsm, $"{_entryAsmName}.Assets.Captcha.background", $"{_entryAsmName}.Assets.Captcha.template"
              , (1, 101), (1, 7), new Size(50, 50))
            .ConfigureAwait(false);

        var id = $"{nameof(GetCaptchaImageAsync)}_{YitIdHelper.NextId()}";
        return new GetCaptchaRsp {
                                     Id              = id
                                   , BackgroundImage = backgroundImage
                                   , SliderImage     = sliderImage
                                   , SawOffsetX      = offsetSaw.X
                                 };
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