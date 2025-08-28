using NetAdmin.Domain.Dto.Sys.VerifyCode;

namespace UnitTests.Sys;

/// <summary>
///     验证码测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class VerifyCodeTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IVerifyCodeModule
{
    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<long> CountAsync(QueryReq<QueryVerifyCodeReq> req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryVerifyCodeReq> req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryVerifyCodeRsp> CreateAsync(CreateVerifyCodeReq req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> DeleteAsync(DelReq req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryVerifyCodeRsp> EditAsync(EditVerifyCodeReq req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IActionResult> ExportAsync(QueryReq<QueryVerifyCodeReq> req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryVerifyCodeRsp> GetAsync(QueryVerifyCodeReq req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<PagedQueryRsp<QueryVerifyCodeRsp>> PagedQueryAsync(PagedQueryReq<QueryVerifyCodeReq> req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IEnumerable<QueryVerifyCodeRsp>> QueryAsync(QueryReq<QueryVerifyCodeReq> req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<SendVerifyCodeRsp> SendVerifyCodeAsync(SendVerifyCodeReq req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<decimal> SumAsync(QueryReq<QueryVerifyCodeReq> req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<bool> VerifyAsync(VerifyCodeReq req) {
        var rsp = await PostJsonAsync(typeof(VerifyCodeController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return false;
    }
}