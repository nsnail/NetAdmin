using NetAdmin.Domain.Dto.Sys.Dept;

namespace UnitTests.Sys;

/// <summary>
///     部门测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class DeptTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IDeptModule
{
    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<long> CountAsync(QueryReq<QueryDeptReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryDeptReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryDeptRsp> CreateAsync(CreateDeptReq req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> DeleteAsync(DelReq req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryDeptRsp> EditAsync(EditDeptReq req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IActionResult> ExportAsync(QueryReq<QueryDeptReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryDeptRsp> GetAsync(QueryDeptReq req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<PagedQueryRsp<QueryDeptRsp>> PagedQueryAsync(PagedQueryReq<QueryDeptReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IEnumerable<QueryDeptRsp>> QueryAsync(QueryReq<QueryDeptReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> SetEnabledAsync(SetDeptEnabledReq req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<decimal> SumAsync(QueryReq<QueryDeptReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }
}