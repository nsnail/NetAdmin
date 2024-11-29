using NetAdmin.Domain.Dto.Sys.Doc.Catalog;
using NetAdmin.Domain.Dto.Sys.Doc.Content;

namespace UnitTests.Sys;

/// <summary>
///     文档测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class DocTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IDocModule
{
    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> BulkDeleteCatalogAsync(BulkReq<DelReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> BulkDeleteContentAsync(BulkReq<DelReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryDocCatalogRsp> CreateCatalogAsync(CreateDocCatalogReq req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryDocContentRsp> CreateContentAsync(CreateDocContentReq req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> DeleteCatalogAsync(DelReq req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> DeleteContentAsync(DelReq req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> EditCatalogAsync(EditDocCatalogReq req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryDocContentRsp> EditContentAsync(EditDocContentReq req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IActionResult> ExportContentAsync(QueryReq<QueryDocContentReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryDocCatalogRsp> GetCatalogAsync(QueryDocCatalogReq req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryDocContentRsp> GetContentAsync(QueryDocContentReq req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<PagedQueryRsp<QueryDocCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDocCatalogReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<PagedQueryRsp<QueryDocContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDocContentReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IEnumerable<QueryDocCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDocCatalogReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IEnumerable<QueryDocContentRsp>> QueryContentAsync(QueryReq<QueryDocContentReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> SetEnabledAsync(SetDocContentEnabledReq req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryDocContentRsp> ViewContentAsync(QueryDocContentReq req)
    {
        var rsp = await PostJsonAsync(typeof(DocController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }
}