using NetAdmin.Domain.Dto.Sys.Config;

namespace UnitTests.Sys;

/// <summary>
///     配置测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class ConfigTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IConfigModule
{
    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var rsp = await PostJsonAsync(typeof(ConfigController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<long> CountAsync(QueryReq<QueryConfigReq> req)
    {
        var rsp = await PostJsonAsync(typeof(ConfigController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryConfigReq> req)
    {
        var rsp = await PostJsonAsync(typeof(ConfigController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryConfigRsp> CreateAsync(CreateConfigReq req)
    {
        var rsp = await PostJsonAsync(typeof(ConfigController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> DeleteAsync(DelReq req)
    {
        var rsp = await PostJsonAsync(typeof(ConfigController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryConfigRsp> EditAsync(EditConfigReq req)
    {
        var rsp = await PostJsonAsync(typeof(ConfigController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IActionResult> ExportAsync(QueryReq<QueryConfigReq> req)
    {
        var rsp = await PostJsonAsync(typeof(ConfigController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryConfigRsp> GetAsync(QueryConfigReq req)
    {
        var rsp = await PostJsonAsync(typeof(ConfigController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<QueryConfigRsp> GetLatestConfigAsync()
    {
        var rsp = await PostJsonAsync(typeof(ConfigController));
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<PagedQueryRsp<QueryConfigRsp>> PagedQueryAsync(PagedQueryReq<QueryConfigReq> req)
    {
        var rsp = await PostJsonAsync(typeof(ConfigController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IEnumerable<QueryConfigRsp>> QueryAsync(QueryReq<QueryConfigReq> req)
    {
        var rsp = await PostJsonAsync(typeof(ConfigController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> SetEnabledAsync(SetConfigEnabledReq req)
    {
        var rsp = await PostJsonAsync(typeof(ConfigController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }
}