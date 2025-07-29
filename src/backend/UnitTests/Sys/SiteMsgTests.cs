using NetAdmin.Domain.Dto.Sys.SiteMsg;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;

namespace UnitTests.Sys;

/// <summary>
///     站内信测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class SiteMsgTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), ISiteMsgModule
{
    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<long> CountAsync(QueryReq<QuerySiteMsgReq> req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QuerySiteMsgReq> req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QuerySiteMsgRsp> CreateAsync(CreateSiteMsgReq req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> DeleteAsync(DelReq req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QuerySiteMsgRsp> EditAsync(EditSiteMsgReq req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgReq> req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QuerySiteMsgRsp> GetAsync(QuerySiteMsgReq req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QuerySiteMsgRsp> GetMineAsync(QuerySiteMsgReq req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryMineAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IEnumerable<QuerySiteMsgRsp>> QueryAsync(QueryReq<QuerySiteMsgReq> req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task SetSiteMsgStatusAsync(SetUserSiteMsgStatusReq req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<decimal> SumAsync(QueryReq<QuerySiteMsgReq> req)
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<long> UnreadCountAsync()
    {
        var rsp = await PostJsonAsync(typeof(SiteMsgController));
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }
}