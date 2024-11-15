using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.Domain.Dto.Sys.JobRecord;

namespace UnitTests.Sys;

/// <summary>
///     计划作业测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class JobTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IJobModule
{
    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<long> CountAsync(QueryReq<QueryJobReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<long> CountRecordAsync(QueryReq<QueryJobRecordReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryJobRsp> CreateAsync(CreateJobReq req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> DeleteAsync(DelReq req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryJobRsp> EditAsync(EditJobReq req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task ExecuteAsync(QueryJobReq req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<bool> ExistAsync(QueryReq<QueryJobReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IActionResult> ExportAsync(QueryReq<QueryJobReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IActionResult> ExportRecordAsync(QueryReq<QueryJobRecordReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryJobRsp> GetAsync(QueryJobReq req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryJobRecordRsp> GetRecordAsync(QueryJobRecordReq req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IEnumerable<GetBarChartRsp>> GetRecordBarChartAsync(QueryReq<QueryJobRecordReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByHttpStatusCodeAsync(QueryReq<QueryJobRecordReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByNameAsync(QueryReq<QueryJobRecordReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<PagedQueryRsp<QueryJobRsp>> PagedQueryAsync(PagedQueryReq<QueryJobReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<PagedQueryRsp<QueryJobRecordRsp>> PagedQueryRecordAsync(PagedQueryReq<QueryJobRecordReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IEnumerable<QueryJobRsp>> QueryAsync(QueryReq<QueryJobReq> req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> SetEnabledAsync(SetJobEnabledReq req)
    {
        var rsp = await PostJsonAsync(typeof(JobController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }
}