using NetAdmin.Domain.Dto.Sys.Role;
using NetAdmin.Domain.Dto.Sys.UserRole;

namespace UnitTests.Sys;

/// <summary>
///     角色测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
public class RoleTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IRoleModule
{
    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<long> CountAsync(QueryReq<QueryRoleReq> req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryRoleReq> req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryRoleRsp> CreateAsync(CreateRoleReq req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> DeleteAsync(DelReq req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryRoleRsp> EditAsync(EditRoleReq req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IActionResult> ExportAsync(QueryReq<QueryRoleReq> req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<QueryRoleRsp> GetAsync(QueryRoleReq req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<PagedQueryRsp<QueryRoleRsp>> PagedQueryAsync(PagedQueryReq<QueryRoleReq> req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IEnumerable<QueryRoleRsp>> QueryAsync(QueryReq<QueryRoleReq> req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> SetDisplayDashboardAsync(SetDisplayDashboardReq req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> SetEnabledAsync(SetRoleEnabledReq req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<int> SetIgnorePermissionControlAsync(SetIgnorePermissionControlReq req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<decimal> SumAsync(QueryReq<QueryRoleReq> req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return 0;
    }

    /// <inheritdoc />
    [InlineData(null)]
    [Theory]
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> UserCountByAsync(QueryReq<QueryUserRoleReq> req) {
        var rsp = await PostJsonAsync(typeof(RoleController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }
}