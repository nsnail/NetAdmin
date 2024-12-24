using NetAdmin.Domain.Dto.Sys.Tool;

namespace UnitTests.Sys;

/// <summary>
///     工具测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class ToolsTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IToolsModule
{
    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public string AesDecode(AesDecodeReq req)
    {
        #pragma warning disable xUnit1031
        var rsp = PostJsonAsync(typeof(ToolsController), req).Result;
        #pragma warning restore xUnit1031
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<object[][]> ExecuteSqlAsync(ExecuteSqlReq req)
    {
        var rsp = await PostJsonAsync(typeof(ToolsController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<string> GetChangeLogAsync()
    {
        var rsp = await PostJsonAsync(typeof(ToolsController));
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<IEnumerable<GetModulesRsp>> GetModulesAsync()
    {
        var rsp = await PostJsonAsync(typeof(ToolsController));
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<DateTime> GetServerUtcTimeAsync()
    {
        var rsp = await PostJsonAsync(typeof(ToolsController));
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<string> GetVersionAsync()
    {
        var rsp = await PostJsonAsync(typeof(ToolsController));
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }
}