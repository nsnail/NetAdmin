using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.RequestLog;

namespace UnitTests.Sys;

/// <summary>
///     请求日志测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class RequestLogTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IRequestLogModule
{
    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<long> CountAsync(QueryReq<QueryRequestLogReq> req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryRequestLogReq> req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryRequestLogRsp> CreateAsync(CreateRequestLogReq req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> DeleteAsync(DelReq req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryRequestLogRsp> EditAsync(EditRequestLogReq req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IActionResult> ExportAsync(QueryReq<QueryRequestLogReq> req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryRequestLogRsp> GetAsync(QueryRequestLogReq req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IEnumerable<GetBarChartRsp>> GetBarChartAsync(QueryReq<QueryRequestLogReq> req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IEnumerable<GetPieChartRsp>> GetPieChartByApiSummaryAsync(QueryReq<QueryRequestLogReq> req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IEnumerable<GetPieChartRsp>> GetPieChartByHttpStatusCodeAsync(QueryReq<QueryRequestLogReq> req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQueryAsync(PagedQueryReq<QueryRequestLogReq> req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IEnumerable<QueryRequestLogRsp>> QueryAsync(QueryReq<QueryRequestLogReq> req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<decimal> SumAsync(QueryReq<QueryRequestLogReq> req) {
        var rsp = await PostJsonAsync(typeof(RequestLogController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }
}