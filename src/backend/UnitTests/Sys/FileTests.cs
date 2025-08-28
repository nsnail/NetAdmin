using NetAdmin.Domain.Dto.Sys.File;

namespace UnitTests.Sys;

/// <summary>
///     文件测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class FileTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IFileModule
{
    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<UploadFileRsp> UploadAsync(IFormFile file) {
        var rsp = await PostJsonAsync(typeof(FileController), file);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }
}