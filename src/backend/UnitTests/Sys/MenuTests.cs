using NetAdmin.Domain.Dto.Sys.Menu;

namespace UnitTests.Sys;

/// <summary>
///     菜单测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class MenuTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IMenuModule
{
    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var rsp = await PostJsonAsync(typeof(MenuTests), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<long> CountAsync(QueryReq<QueryMenuReq> req)
    {
        var rsp = await PostJsonAsync(typeof(MenuTests), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryMenuRsp> CreateAsync(CreateMenuReq req)
    {
        var rsp = await PostJsonAsync(typeof(MenuTests), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> DeleteAsync(DelReq req)
    {
        var rsp = await PostJsonAsync(typeof(MenuTests), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryMenuRsp> EditAsync(EditMenuReq req)
    {
        var rsp = await PostJsonAsync(typeof(MenuTests), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<bool> ExistAsync(QueryReq<QueryMenuReq> req)
    {
        var rsp = await PostJsonAsync(typeof(MenuTests), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IActionResult> ExportAsync(QueryReq<QueryMenuReq> req)
    {
        var rsp = await PostJsonAsync(typeof(MenuTests), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryMenuRsp> GetAsync(QueryMenuReq req)
    {
        var rsp = await PostJsonAsync(typeof(MenuTests), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<PagedQueryRsp<QueryMenuRsp>> PagedQueryAsync(PagedQueryReq<QueryMenuReq> req)
    {
        var rsp = await PostJsonAsync(typeof(MenuTests), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IEnumerable<QueryMenuRsp>> QueryAsync(QueryReq<QueryMenuReq> req)
    {
        var rsp = await PostJsonAsync(typeof(MenuTests), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<IEnumerable<QueryMenuRsp>> UserMenusAsync()
    {
        var rsp = await PostJsonAsync(typeof(MenuTests));
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }
}