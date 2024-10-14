using System.Diagnostics.CodeAnalysis;
using NetAdmin.AdmServer.Host;
using NetAdmin.Domain.Dto.Sys.Tool;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Host.Controllers.Sys;
using NetAdmin.Tests;
using Xunit;
using Xunit.Abstractions;

namespace NetAdmin.SysComponent.Tests.Sys;

/// <summary>
///     工具测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class ToolsTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IToolsModule
{
    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public string AesDecode(AesDecodeReq req)
    {
        #pragma warning disable xUnit1031
        var rsp = PostJsonAsync(typeof(ToolsController), req).Result;
        #pragma warning restore xUnit1031
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<object[][]> ExecuteSqlAsync(ExecuteSqlReq req)
    {
        var rsp = await PostJsonAsync(typeof(ToolsController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<string> GetChangeLogAsync()
    {
        var rsp = await PostJsonAsync(typeof(ToolsController));
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<IEnumerable<GetModulesRsp>> GetModulesAsync()
    {
        var rsp = await PostJsonAsync(typeof(ToolsController));
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
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
        return default;
    }
}