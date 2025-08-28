using NetAdmin.Domain.Dto.Sys.Dev;

namespace UnitTests.Sys;

/// <summary>
///     开发测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class DevTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IDevModule
{
    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task GenerateCsCodeAsync(GenerateCsCodeReq req) {
        var rsp = await PostJsonAsync(typeof(DevController), req);
        Assert.True(rsp.IsSuccessStatusCode);
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task GenerateIconCodeAsync(GenerateIconCodeReq req) {
        var rsp = await PostJsonAsync(typeof(DevController), req);
        Assert.True(rsp.IsSuccessStatusCode);
    }

    /// <inheritdoc />
    [Fact]
    public async Task GenerateJsCodeAsync() {
        var rsp = await PostJsonAsync(typeof(DevController));
        Assert.True(rsp.IsSuccessStatusCode);
    }

    /// <inheritdoc />
    [Fact]
    public async Task<IEnumerable<Tuple<string, string>>> GetDomainProjectsAsync() {
        var rsp = await PostJsonAsync(typeof(DevController));
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    public IEnumerable<string> GetDotnetDataTypes(GetDotnetDataTypesReq req) {
        #pragma warning disable xUnit1031
        var rsp = PostJsonAsync(typeof(DevController)).GetAwaiter().GetResult();
        #pragma warning restore xUnit1031
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [Fact]
    public IEnumerable<Tuple<string, string>> GetEntityBaseClasses() {
        #pragma warning disable xUnit1031
        var rsp = PostJsonAsync(typeof(DevController)).GetAwaiter().GetResult();
        #pragma warning restore xUnit1031
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [Fact]
    public IEnumerable<Tuple<string, string>> GetFieldInterfaces() {
        #pragma warning disable xUnit1031
        var rsp = PostJsonAsync(typeof(DevController)).GetAwaiter().GetResult();
        #pragma warning restore xUnit1031
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }
}