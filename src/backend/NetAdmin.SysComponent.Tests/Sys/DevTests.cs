using NetAdmin.Domain.Dto.Sys.Dev;

namespace NetAdmin.SysComponent.Tests.Sys;

/// <summary>
///     开发测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class DevTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IDevModule
{
    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task GenerateCsCodeAsync(GenerateCsCodeReq req)
    {
        var rsp = await PostJsonAsync(typeof(DevController), req);
        Assert.True(rsp.IsSuccessStatusCode);
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task GenerateIconCodeAsync(GenerateIconCodeReq req)
    {
        var rsp = await PostJsonAsync(typeof(DevController), req);
        Assert.True(rsp.IsSuccessStatusCode);
    }

    /// <inheritdoc />
    [Fact]
    public async Task GenerateJsCodeAsync()
    {
        var rsp = await PostJsonAsync(typeof(DevController));
        Assert.True(rsp.IsSuccessStatusCode);
    }
}