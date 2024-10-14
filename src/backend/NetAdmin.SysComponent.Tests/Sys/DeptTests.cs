using System.Diagnostics.CodeAnalysis;
using NetAdmin.AdmServer.Host;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.SysComponent.Host.Controllers.Sys;
using NetAdmin.Tests;
using Xunit;
using Xunit.Abstractions;

namespace NetAdmin.SysComponent.Tests.Sys;

/// <summary>
///     部门测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class DeptTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IDeptModule
{
    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<long> CountAsync(QueryReq<QueryDeptReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryDeptRsp> CreateAsync(CreateDeptReq req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> DeleteAsync(DelReq req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryDeptRsp> EditAsync(EditDeptReq req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<bool> ExistAsync(QueryReq<QueryDeptReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IActionResult> ExportAsync(QueryReq<QueryDeptReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<QueryDeptRsp> GetAsync(QueryDeptReq req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<PagedQueryRsp<QueryDeptRsp>> PagedQueryAsync(PagedQueryReq<QueryDeptReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<IEnumerable<QueryDeptRsp>> QueryAsync(QueryReq<QueryDeptReq> req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<int> SetEnabledAsync(SetDeptEnabledReq req)
    {
        var rsp = await PostJsonAsync(typeof(DeptController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }
}