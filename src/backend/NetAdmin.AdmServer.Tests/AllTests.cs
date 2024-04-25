using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NetAdmin.AdmServer.Host;
using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.Domain.Dto.Sys.Config;
using NetAdmin.Domain.Dto.Sys.Tool;
using NetAdmin.SysComponent.Application.Modules.Sys;
using NetAdmin.Tests;
using Xunit;
using Xunit.Abstractions;

namespace NetAdmin.AdmServer.Tests;

/// <summary>
///     所有测试
/// </summary>
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class AllTests(WebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IToolsModule, ICacheModule, IApiModule, IConfigModule

{
    /// <inheritdoc cref="ICrudModule{TCreateReq,TCreateRsp,TQueryReq,TQueryRsp,TUpdateReq,TUpdateRsp,TDelReq}.BulkDeleteAsync" />
    public Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [Fact]
    public async Task<CacheStatisticsRsp> CacheStatisticsAsync()
    {
        var rsp = await PostAsync("/api/sys/cache/cache.statistics", null).ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, rsp.StatusCode);
        return default;
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryConfigReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<long> CountAsync(QueryReq<QueryApiReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> CreateAsync(CreateConfigReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> CreateAsync(CreateApiReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc cref="ICrudModule{TCreateReq,TCreateRsp,TQueryReq,TQueryRsp,TUpdateReq,TUpdateRsp,TDelReq}.DeleteAsync" />
    public Task<int> DeleteAsync(DelReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryConfigReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<bool> ExistAsync(QueryReq<QueryApiReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public async Task<PagedQueryRsp<GetAllEntriesRsp>> GetAllEntriesAsync(PagedQueryReq<GetAllEntriesReq> req)
    {
        var rsp = await PostAsync("/api/sys/cache/get.all.entries"
                                , JsonContent.Create(new PagedQueryReq<GetAllEntriesReq>()))
            .ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, rsp.StatusCode);
        return default;
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> GetAsync(QueryConfigReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryApiRsp> GetAsync(QueryApiReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<string> GetChangeLogAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> GetLatestConfigAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<GetModulesRsp>> GetModulesAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [Fact]
    public async Task<DateTime> GetServerUtcTimeAsync()
    {
        var response = await PostAsync("/api/sys/tools/get.server.utc.time", null).ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public async Task<string> GetVersionAsync()
    {
        var response = await PostAsync("/api/sys/tools/version", null).ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        return default;
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryConfigRsp>> PagedQueryAsync(PagedQueryReq<QueryConfigReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<PagedQueryRsp<QueryApiRsp>> PagedQueryAsync(PagedQueryReq<QueryApiReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryConfigRsp>> QueryAsync(QueryReq<QueryConfigReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<IEnumerable<QueryApiRsp>> QueryAsync(QueryReq<QueryApiReq> req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    [Fact]
    public async Task SyncAsync()
    {
        var response = await PostAsync("/api/sys/api/sync", null).ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /// <inheritdoc />
    public Task<QueryConfigRsp> UpdateAsync(UpdateConfigReq req)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<NopReq> UpdateAsync(NopReq req)
    {
        throw new NotImplementedException();
    }
}