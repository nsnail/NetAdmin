namespace NetAdmin.SysComponent.Tests.Sys;

/// <summary>
///     文件测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class FileTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IFileModule
{
    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<string> UploadAsync(IFormFile file)
    {
        var rsp = await PostJsonAsync(typeof(FileController), file);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }
}