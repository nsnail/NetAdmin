#pragma warning disable xUnit1024,xUnit1026

using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NetAdmin.AdmServer.Host;
using NetAdmin.Application.Modules;
using NetAdmin.Domain.Dto.Dependency;
using NetAdmin.Domain.Dto.Sys;
using NetAdmin.Domain.Dto.Sys.Api;
using NetAdmin.Domain.Dto.Sys.Cache;
using NetAdmin.Domain.Dto.Sys.Captcha;
using NetAdmin.Domain.Dto.Sys.Config;
using NetAdmin.Domain.Dto.Sys.Dept;
using NetAdmin.Domain.Dto.Sys.Dev;
using NetAdmin.Domain.Dto.Sys.Dic.Catalog;
using NetAdmin.Domain.Dto.Sys.Dic.Content;
using NetAdmin.Domain.Dto.Sys.Job;
using NetAdmin.Domain.Dto.Sys.JobRecord;
using NetAdmin.Domain.Dto.Sys.Menu;
using NetAdmin.Domain.Dto.Sys.RequestLog;
using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.Domain.Dto.Sys.SiteMsg;
using NetAdmin.Domain.Dto.Sys.SiteMsgDept;
using NetAdmin.Domain.Dto.Sys.SiteMsgFlag;
using NetAdmin.Domain.Dto.Sys.SiteMsgRole;
using NetAdmin.Domain.Dto.Sys.SiteMsgUser;
using NetAdmin.Domain.Dto.Sys.Tool;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.Domain.Dto.Sys.VerifyCode;
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
    : WebApiTestBase<Startup>(factory, testOutputHelper), IApiModule, ICacheModule, ICaptchaModule, IConfigModule
    , IConstantModule, IDeptModule, IDevModule, IDicCatalogModule, IDicContentModule, IDicModule, IFileModule
    , IJobModule, IJobRecordModule, IMenuModule, IRequestLogModule, IRoleModule, ISiteMsgDeptModule, ISiteMsgFlagModule
    , ISiteMsgModule, ISiteMsgRoleModule, ISiteMsgUserModule, IToolsModule, IUserModule, IUserProfileModule
    , IVerifyCodeModule

{
    /// <inheritdoc />
    public Task<int> BulkDeleteCatalogAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    public Task<int> BulkDeleteContentAsync(BulkReq<DelReq> req)
    {
        return default;
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
    [InlineData(default)]
    [Theory]
    public Task<bool> CheckMobileAvailableAsync(CheckMobileAvailableReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> CheckUserNameAvailableAsync(CheckUserNameAvailableReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryJobReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryUserProfileReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryUserReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QuerySiteMsgRoleReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryRoleReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryRequestLogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryMenuReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryJobRecordReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryDicContentReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryDeptReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryConfigReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountAsync(QueryReq<QueryApiReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<long> CountRecordAsync(QueryReq<QueryJobRecordReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryJobRsp> CreateAsync(CreateJobReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryVerifyCodeRsp> CreateAsync(CreateVerifyCodeReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryUserProfileRsp> CreateAsync(CreateUserProfileReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryUserRsp> CreateAsync(CreateUserReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgUserRsp> CreateAsync(CreateSiteMsgUserReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgRoleRsp> CreateAsync(CreateSiteMsgRoleReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgRsp> CreateAsync(CreateSiteMsgReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgFlagRsp> CreateAsync(CreateSiteMsgFlagReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgDeptRsp> CreateAsync(CreateSiteMsgDeptReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryRoleRsp> CreateAsync(CreateRoleReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryRequestLogRsp> CreateAsync(CreateRequestLogReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryMenuRsp> CreateAsync(CreateMenuReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryJobRecordRsp> CreateAsync(CreateJobRecordReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDicContentRsp> CreateAsync(CreateDicContentReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDicCatalogRsp> CreateAsync(CreateDicCatalogReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDeptRsp> CreateAsync(CreateDeptReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryConfigRsp> CreateAsync(CreateConfigReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryApiRsp> CreateAsync(CreateApiReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDicCatalogRsp> CreateCatalogAsync(CreateDicCatalogReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDicContentRsp> CreateContentAsync(CreateDicContentReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> DeleteCatalogAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> DeleteContentAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryRoleRsp> EditAsync(EditRoleReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDeptRsp> EditAsync(EditDeptReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryConfigRsp> EditAsync(EditConfigReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryJobRsp> EditAsync(EditJobReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryMenuRsp> EditAsync(EditMenuReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgRsp> EditAsync(EditSiteMsgReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryUserRsp> EditAsync(EditUserReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> EditCatalogAsync(EditDicCatalogReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDicContentRsp> EditContentAsync(EditDicContentReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task ExecuteAsync(QueryJobReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryJobReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryUserProfileReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryUserReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgRoleReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryRoleReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryRequestLogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryMenuReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryJobRecordReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryDicContentReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryDeptReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryConfigReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> ExistAsync(QueryReq<QueryApiReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryJobReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserProfileReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryUserReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgRoleReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryRoleReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryRequestLogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryMenuReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryJobRecordReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryDicContentReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryDeptReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryConfigReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportAsync(QueryReq<QueryApiReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportContentAsync(QueryReq<QueryDicContentReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IActionResult> ExportRecordAsync(QueryReq<QueryJobRecordReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task GenerateCsCodeAsync(GenerateCsCodeReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task GenerateIconCodeAsync(GenerateIconCodeReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public Task GenerateJsCodeAsync()
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<GetEntryRsp>> GetAllEntriesAsync(GetAllEntriesReq req)
    {
        var rsp = PostAsync("/api/sys/cache/get.all.entries", JsonContent.Create(new GetAllEntriesReq()))
                  .ConfigureAwait(true)
                  .GetAwaiter()
                  #pragma warning disable xUnit1031
                  .GetResult();
        #pragma warning restore xUnit1031
        Assert.Equal(HttpStatusCode.OK, rsp.StatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryJobRsp> GetAsync(QueryJobReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryVerifyCodeRsp> GetAsync(QueryVerifyCodeReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryUserProfileRsp> GetAsync(QueryUserProfileReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryUserRsp> GetAsync(QueryUserReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgUserRsp> GetAsync(QuerySiteMsgUserReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgRoleRsp> GetAsync(QuerySiteMsgRoleReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgRsp> GetAsync(QuerySiteMsgReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgFlagRsp> GetAsync(QuerySiteMsgFlagReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgDeptRsp> GetAsync(QuerySiteMsgDeptReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryRoleRsp> GetAsync(QueryRoleReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryRequestLogRsp> GetAsync(QueryRequestLogReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryMenuRsp> GetAsync(QueryMenuReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryJobRecordRsp> GetAsync(QueryJobRecordReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDicContentRsp> GetAsync(QueryDicContentReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDicCatalogRsp> GetAsync(QueryDicCatalogReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDeptRsp> GetAsync(QueryDeptReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryConfigRsp> GetAsync(QueryConfigReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryApiRsp> GetAsync(QueryApiReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<GetBarChartRsp>> GetBarChartAsync(QueryReq<QueryRequestLogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public Task<GetCaptchaRsp> GetCaptchaImageAsync()
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDicCatalogRsp> GetCatalogAsync(QueryDicCatalogReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public Task<string> GetChangeLogAsync()
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public IDictionary<string, string> GetCharsDic()
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryDicContentRsp> GetContentAsync(QueryDicContentReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<string> GetDicValueAsync(GetDicValueReq req)
    {
        return default;
    }

    /// <inheritdoc />
    public Task<GetEntryRsp> GetEntryAsync(GetEntriesReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public IDictionary<string, Dictionary<string, string[]>> GetEnums()
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public Task<QueryConfigRsp> GetLatestConfigAsync()
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public IDictionary<string, string> GetLocalizedStrings()
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QuerySiteMsgRsp> GetMineAsync(QuerySiteMsgReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public Task<IEnumerable<GetModulesRsp>> GetModulesAsync()
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public IDictionary<string, long> GetNumbersDic()
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<GetPieChartRsp>> GetPieChartByApiSummaryAsync(QueryReq<QueryRequestLogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<GetPieChartRsp>> GetPieChartByHttpStatusCodeAsync(QueryReq<QueryRequestLogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<QueryJobRecordRsp> GetRecordAsync(QueryJobRecordReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<GetBarChartRsp>> GetRecordBarChartAsync(QueryReq<QueryJobRecordReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByHttpStatusCodeAsync(QueryReq<QueryJobRecordReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<GetPieChartRsp>> GetRecordPieChartByNameAsync(QueryReq<QueryJobRecordReq> req)
    {
        return default;
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
    public Task<GetSessionUserAppConfigRsp> GetSessionUserAppConfigAsync()
    {
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
    [InlineData(default)]
    [Theory]
    public Task<LoginRsp> LoginByPwdAsync(LoginByPwdReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<LoginRsp> LoginBySmsAsync(LoginBySmsReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryJobRsp>> PagedQueryAsync(PagedQueryReq<QueryJobReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryVerifyCodeRsp>> PagedQueryAsync(PagedQueryReq<QueryVerifyCodeReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryUserProfileRsp>> PagedQueryAsync(PagedQueryReq<QueryUserProfileReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryUserRsp>> PagedQueryAsync(PagedQueryReq<QueryUserReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QuerySiteMsgUserRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgUserReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QuerySiteMsgRoleRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgRoleReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QuerySiteMsgFlagRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgFlagReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QuerySiteMsgDeptRsp>> PagedQueryAsync(PagedQueryReq<QuerySiteMsgDeptReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryRoleRsp>> PagedQueryAsync(PagedQueryReq<QueryRoleReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryRequestLogRsp>> PagedQueryAsync(PagedQueryReq<QueryRequestLogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryMenuRsp>> PagedQueryAsync(PagedQueryReq<QueryMenuReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryJobRecordRsp>> PagedQueryAsync(PagedQueryReq<QueryJobRecordReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryAsync(PagedQueryReq<QueryDicCatalogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryDeptRsp>> PagedQueryAsync(PagedQueryReq<QueryDeptReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryConfigRsp>> PagedQueryAsync(PagedQueryReq<QueryConfigReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryApiRsp>> PagedQueryAsync(PagedQueryReq<QueryApiReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryDicCatalogRsp>> PagedQueryCatalogAsync(PagedQueryReq<QueryDicCatalogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryDicContentRsp>> PagedQueryContentAsync(PagedQueryReq<QueryDicContentReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QuerySiteMsgRsp>> PagedQueryMineAsync(PagedQueryReq<QuerySiteMsgReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<PagedQueryRsp<QueryJobRecordRsp>> PagedQueryRecordAsync(PagedQueryReq<QueryJobRecordReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryJobRsp>> QueryAsync(QueryReq<QueryJobReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryVerifyCodeRsp>> QueryAsync(QueryReq<QueryVerifyCodeReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryUserProfileRsp>> QueryAsync(QueryReq<QueryUserProfileReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryUserRsp>> QueryAsync(QueryReq<QueryUserReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QuerySiteMsgUserRsp>> QueryAsync(QueryReq<QuerySiteMsgUserReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QuerySiteMsgRoleRsp>> QueryAsync(QueryReq<QuerySiteMsgRoleReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QuerySiteMsgRsp>> QueryAsync(QueryReq<QuerySiteMsgReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QuerySiteMsgFlagRsp>> QueryAsync(QueryReq<QuerySiteMsgFlagReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QuerySiteMsgDeptRsp>> QueryAsync(QueryReq<QuerySiteMsgDeptReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryRoleRsp>> QueryAsync(QueryReq<QueryRoleReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryRequestLogRsp>> QueryAsync(QueryReq<QueryRequestLogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryMenuRsp>> QueryAsync(QueryReq<QueryMenuReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryJobRecordRsp>> QueryAsync(QueryReq<QueryJobRecordReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryDicContentRsp>> QueryAsync(QueryReq<QueryDicContentReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryDicCatalogRsp>> QueryAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryDeptRsp>> QueryAsync(QueryReq<QueryDeptReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryConfigRsp>> QueryAsync(QueryReq<QueryConfigReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryApiRsp>> QueryAsync(QueryReq<QueryApiReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryDicCatalogRsp>> QueryCatalogAsync(QueryReq<QueryDicCatalogReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryDicContentRsp>> QueryContentAsync(QueryReq<QueryDicContentReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<UserInfoRsp> RegisterAsync(RegisterUserReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> ResetPasswordAsync(ResetPasswordReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<SendVerifyCodeRsp> SendVerifyCodeAsync(SendVerifyCodeReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<UserInfoRsp> SetAvatarAsync(SetAvatarReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> SetDisplayDashboardAsync(SetDisplayDashboardReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<UserInfoRsp> SetEmailAsync(SetEmailReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> SetEnabledAsync(SetDeptEnabledReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> SetEnabledAsync(SetConfigEnabledReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> SetEnabledAsync(SetRoleEnabledReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> SetEnabledAsync(SetJobEnabledReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> SetEnabledAsync(SetUserEnabledReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> SetIgnorePermissionControlAsync(SetIgnorePermissionControlReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<UserInfoRsp> SetMobileAsync(SetMobileReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> SetPasswordAsync(SetPasswordReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<int> SetSessionUserAppConfigAsync(SetSessionUserAppConfigReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task SetSiteMsgStatusAsync(SetUserSiteMsgStatusReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public async Task SyncAsync()
    {
        var response = await PostAsync("/api/sys/api/sync", null).ConfigureAwait(true);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /// <inheritdoc />
    [Fact]
    public Task<long> UnreadCountAsync()
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<string> UploadAsync(IFormFile file)
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public Task<UserInfoRsp> UserInfoAsync()
    {
        return default;
    }

    /// <inheritdoc />
    [Fact]
    public Task<IEnumerable<QueryMenuRsp>> UserMenusAsync()
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> VerifyAsync(VerifyCodeReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    public Task<bool> VerifyCaptchaAsync(VerifyCaptchaReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateJobReq, QueryJobRsp, QueryJobReq, QueryJobRsp, DelReq>.BulkDeleteAsync(
        BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateVerifyCodeReq, QueryVerifyCodeRsp, QueryVerifyCodeReq, QueryVerifyCodeRsp, DelReq>.
        BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateUserProfileReq, QueryUserProfileRsp, QueryUserProfileReq, QueryUserProfileRsp, DelReq>.
        BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateUserReq, QueryUserRsp, QueryUserReq, QueryUserRsp, DelReq>.BulkDeleteAsync(
        BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateSiteMsgUserReq, QuerySiteMsgUserRsp, QuerySiteMsgUserReq, QuerySiteMsgUserRsp, DelReq>.
        BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateSiteMsgRoleReq, QuerySiteMsgRoleRsp, QuerySiteMsgRoleReq, QuerySiteMsgRoleRsp, DelReq>.
        BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateSiteMsgReq, QuerySiteMsgRsp, QuerySiteMsgReq, QuerySiteMsgRsp, DelReq>.BulkDeleteAsync(
        BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateSiteMsgFlagReq, QuerySiteMsgFlagRsp, QuerySiteMsgFlagReq, QuerySiteMsgFlagRsp, DelReq>.
        BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateSiteMsgDeptReq, QuerySiteMsgDeptRsp, QuerySiteMsgDeptReq, QuerySiteMsgDeptRsp, DelReq>.
        BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateRoleReq, QueryRoleRsp, QueryRoleReq, QueryRoleRsp, DelReq>.BulkDeleteAsync(
        BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateRequestLogReq, QueryRequestLogRsp, QueryRequestLogReq, QueryRequestLogRsp, DelReq>.
        BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateMenuReq, QueryMenuRsp, QueryMenuReq, QueryMenuRsp, DelReq>.BulkDeleteAsync(
        BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateJobRecordReq, QueryJobRecordRsp, QueryJobRecordReq, QueryJobRecordRsp, DelReq>.
        BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateDicContentReq, QueryDicContentRsp, QueryDicContentReq, QueryDicContentRsp, DelReq>.
        BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateDicCatalogReq, QueryDicCatalogRsp, QueryDicCatalogReq, QueryDicCatalogRsp, DelReq>.
        BulkDeleteAsync(BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateDeptReq, QueryDeptRsp, QueryDeptReq, QueryDeptRsp, DelReq>.BulkDeleteAsync(
        BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateConfigReq, QueryConfigRsp, QueryConfigReq, QueryConfigRsp, DelReq>.BulkDeleteAsync(
        BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateApiReq, QueryApiRsp, QueryApiReq, QueryApiRsp, DelReq>.BulkDeleteAsync(
        BulkReq<DelReq> req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateJobReq, QueryJobRsp, QueryJobReq, QueryJobRsp, DelReq>.DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateVerifyCodeReq, QueryVerifyCodeRsp, QueryVerifyCodeReq, QueryVerifyCodeRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateUserProfileReq, QueryUserProfileRsp, QueryUserProfileReq, QueryUserProfileRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateUserReq, QueryUserRsp, QueryUserReq, QueryUserRsp, DelReq>.DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateSiteMsgUserReq, QuerySiteMsgUserRsp, QuerySiteMsgUserReq, QuerySiteMsgUserRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateSiteMsgRoleReq, QuerySiteMsgRoleRsp, QuerySiteMsgRoleReq, QuerySiteMsgRoleRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateSiteMsgReq, QuerySiteMsgRsp, QuerySiteMsgReq, QuerySiteMsgRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateSiteMsgFlagReq, QuerySiteMsgFlagRsp, QuerySiteMsgFlagReq, QuerySiteMsgFlagRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateSiteMsgDeptReq, QuerySiteMsgDeptRsp, QuerySiteMsgDeptReq, QuerySiteMsgDeptRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateRoleReq, QueryRoleRsp, QueryRoleReq, QueryRoleRsp, DelReq>.DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateRequestLogReq, QueryRequestLogRsp, QueryRequestLogReq, QueryRequestLogRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateMenuReq, QueryMenuRsp, QueryMenuReq, QueryMenuRsp, DelReq>.DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateJobRecordReq, QueryJobRecordRsp, QueryJobRecordReq, QueryJobRecordRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateDicContentReq, QueryDicContentRsp, QueryDicContentReq, QueryDicContentRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateDicCatalogReq, QueryDicCatalogRsp, QueryDicCatalogReq, QueryDicCatalogRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateDeptReq, QueryDeptRsp, QueryDeptReq, QueryDeptRsp, DelReq>.DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateConfigReq, QueryConfigRsp, QueryConfigReq, QueryConfigRsp, DelReq>.
        DeleteAsync(DelReq req)
    {
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    Task<int> ICrudModule<CreateApiReq, QueryApiRsp, QueryApiReq, QueryApiRsp, DelReq>.DeleteAsync(DelReq req)
    {
        return default;
    }
}