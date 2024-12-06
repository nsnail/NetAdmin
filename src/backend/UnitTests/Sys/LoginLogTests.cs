using NetAdmin.Domain.Dto.Sys.LoginLog;

namespace UnitTests.Sys;

/// <summary>
///     登录日志测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class LoginLogTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), ILoginLogModule
{
    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var rsp = await PostJsonAsync(typeof(LoginLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<long> CountAsync(QueryReq<QueryLoginLogReq> req)
    {
        var rsp = await PostJsonAsync(typeof(LoginLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryLoginLogRsp> CreateAsync(CreateLoginLogReq req)
    {
        var rsp = await PostJsonAsync(typeof(LoginLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> DeleteAsync(DelReq req)
    {
        var rsp = await PostJsonAsync(typeof(LoginLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryLoginLogRsp> EditAsync(EditLoginLogReq req)
    {
        var rsp = await PostJsonAsync(typeof(LoginLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IActionResult> ExportAsync(QueryReq<QueryLoginLogReq> req)
    {
        var rsp = await PostJsonAsync(typeof(LoginLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryLoginLogRsp> GetAsync(QueryLoginLogReq req)
    {
        var rsp = await PostJsonAsync(typeof(LoginLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<PagedQueryRsp<QueryLoginLogRsp>> PagedQueryAsync(PagedQueryReq<QueryLoginLogReq> req)
    {
        var rsp = await PostJsonAsync(typeof(LoginLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IEnumerable<QueryLoginLogRsp>> QueryAsync(QueryReq<QueryLoginLogReq> req)
    {
        var rsp = await PostJsonAsync(typeof(LoginLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }
}