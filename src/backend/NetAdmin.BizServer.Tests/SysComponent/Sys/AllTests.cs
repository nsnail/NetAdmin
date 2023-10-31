using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NetAdmin.BizServer.Host;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.Tests;
using Xunit;
using Xunit.Abstractions;

namespace NetAdmin.BizServer.Tests.SysComponent.Sys;

/// <summary>
///     所有测试
/// </summary>
public class AllTests
    (WebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper) : WebApiTestBase<Startup>(
            factory, testOutputHelper)
      , IToolsModule, ICacheModule, IApiModule

{
    /// <inheritdoc />
    [Theory]
    [InlineData(default)]
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<CacheStatisticsRsp> CacheStatisticsAsync()
    {
        var rsp = await PostAsync("/api/sys/cache/cache.statistics", null);
        Assert.Equal(HttpStatusCode.OK, rsp.StatusCode);
        return default;
    }

    /// <inheritdoc />
    [Theory]
    [InlineData(default)]
    public Task<QueryApiRsp> CreateAsync(CreateApiReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [Theory]
    [InlineData(default)]
    public Task<int> DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [Theory]
    [InlineData(default)]
    public Task<bool> ExistAsync(QueryReq<QueryApiReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [Theory]
    [InlineData(default)]
    public async Task<PagedQueryRsp<GetAllEntriesRsp>> GetAllEntries(PagedQueryReq<GetAllEntriesReq> req)
    {
        var rsp = await PostAsync("/api/sys/cache/get.all.entries"
                                , JsonContent.Create(new PagedQueryReq<GetAllEntriesReq>()));
        Assert.Equal(HttpStatusCode.OK, rsp.StatusCode);
        return default;
    }

    /// <inheritdoc />
    [Theory]
    [InlineData(default)]
    public Task<QueryApiRsp> GetAsync(QueryApiReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<DateTime> GetServerUtcTimeAsync()
    {
        var response = await PostAsync("/api/sys/tools/get.server.utc.time", null);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return default;
    }

    /// <inheritdoc />
    [Theory]
    [InlineData(default)]
    public Task<PagedQueryRsp<QueryApiRsp>> PagedQueryAsync(PagedQueryReq<QueryApiReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [Theory]
    [InlineData(default)]
    public Task<IEnumerable<QueryApiRsp>> QueryAsync(QueryReq<QueryApiReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public async Task SyncAsync()
    {
        var response = await PostAsync("/api/sys/api/sync", null);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /// <inheritdoc />
    [Theory]
    [InlineData(default)]
    public Task<NopReq> UpdateAsync(NopReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<string> VersionAsync()
    {
        var response = await PostAsync("/api/sys/tools/version", null);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return default;
    }
}