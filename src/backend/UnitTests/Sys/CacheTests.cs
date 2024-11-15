using NetAdmin.Domain.Dto.Sys.Cache;

namespace UnitTests.Sys;

/// <summary>
///     缓存测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class CacheTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), ICacheModule
{
    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> BulkDeleteEntryAsync(BulkReq<DelEntryReq> req)
    {
        var rsp = await PostJsonAsync(typeof(CacheController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<CacheStatisticsRsp> CacheStatisticsAsync()
    {
        var rsp = await PostJsonAsync(typeof(CacheController));
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> DeleteEntryAsync(DelEntryReq req)
    {
        var rsp = await PostJsonAsync(typeof(CacheController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IEnumerable<GetEntryRsp>> GetAllEntriesAsync(GetAllEntriesReq req)
    {
        var rsp = await PostJsonAsync(typeof(CacheController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<GetEntryRsp> GetEntryAsync(GetEntriesReq req)
    {
        var rsp = await PostJsonAsync(typeof(CacheController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }
}