using NetAdmin.SysComponent.Domain.Dto.Sys.Captcha;

namespace UnitTests.Sys;

/// <summary>
///     人机验证测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class CaptchaTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), ICaptchaModule
{
    /// <inheritdoc />
    [Fact]
    public async Task<GetCaptchaRsp> GetCaptchaImageAsync()
    {
        var rsp = await PostJsonAsync(typeof(CaptchaController));
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<bool> VerifyCaptchaAsync(VerifyCaptchaReq req)
    {
        var rsp = await PostJsonAsync(typeof(CaptchaController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }
}