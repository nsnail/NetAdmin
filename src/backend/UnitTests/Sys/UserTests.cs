using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Domain.Dto.Sys.UserProfile;
using NetAdmin.Tests.Attributes;

namespace UnitTests.Sys;

/// <summary>
///     用户测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
[TestCaseOrderer("NetAdmin.Tests.PriorityOrderer", "NetAdmin.Tests")]
public class UserTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IUserModule
{
    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(100100)]
    public async Task<int> BulkDeleteAsync(BulkReq<DelReq> req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.Equal(HttpStatusCode.NotFound, rsp.StatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(100200)]
    public async Task<bool> CheckMobileAvailableAsync(CheckMobileAvailableReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), new CheckMobileAvailableReq { Mobile = "13838381438" });
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(100300)]
    public async Task<bool> CheckUserNameAvailableAsync(CheckUserNameAvailableReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), new CheckUserNameAvailableReq { UserName = "test" });
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(100400)]
    public async Task<long> CountAsync(QueryReq<QueryUserReq> req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(100400)]
    public async Task<IOrderedEnumerable<KeyValuePair<IImmutableDictionary<string, string>, int>>> CountByAsync(QueryReq<QueryUserReq> req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(100000)]
    public async Task<QueryUserRsp> CreateAsync(CreateUserReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController)
                                    , new CreateUserReq {
                                                            UserName     = "test"
                                                          , PasswordText = "1234qwer"
                                                          , RoleIds      = [371729946431493]
                                                          , DeptId       = 372119301627909
                                                        });
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(100600)]
    public async Task<int> DeleteAsync(DelReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(100700)]
    public async Task<QueryUserRsp> EditAsync(EditUserReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController)
                                    , new EditUserReq { UserName = "test", RoleIds = [371729946431493], DeptId = 372119301627909 });
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(100900)]
    public async Task<IActionResult> ExportAsync(QueryReq<QueryUserReq> req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(101000)]
    public async Task<QueryUserRsp> GetAsync(QueryUserReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [Fact]
    [TestPriority(101100)]
    public async Task<GetSessionUserAppConfigRsp> GetSessionUserAppConfigAsync()
    {
        var rsp = await PostJsonAsync(typeof(UserController));
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(101200)]
    public async Task<LoginRsp> LoginByPwdAsync(LoginByPwdReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), new LoginByPwdReq { Account = "root", Password = "1234qwer" });
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(101300)]
    public async Task<LoginRsp> LoginBySmsAsync(LoginBySmsReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), new LoginBySmsReq { Code = "1234", DestDevice = "13838381438" });
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(101400)]
    public async Task<PagedQueryRsp<QueryUserRsp>> PagedQueryAsync(PagedQueryReq<QueryUserReq> req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(101500)]
    public async Task<IEnumerable<QueryUserRsp>> QueryAsync(QueryReq<QueryUserReq> req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(101600)]
    public async Task<IEnumerable<QueryUserProfileRsp>> QueryProfileAsync(QueryReq<QueryUserProfileReq> req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(101700)]
    public async Task<UserInfoRsp> RegisterAsync(RegisterUserReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(101800)]
    public async Task<int> ResetPasswordAsync(ResetPasswordReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(101900)]
    public async Task<UserInfoRsp> SetAvatarAsync(SetAvatarReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(102000)]
    public async Task<UserInfoRsp> SetEmailAsync(SetEmailReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(102100)]
    public async Task<int> SetEnabledAsync(SetUserEnabledReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(102200)]
    public async Task<UserInfoRsp> SetMobileAsync(SetMobileReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(102300)]
    public async Task<int> SetPasswordAsync(SetPasswordReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [InlineData(default)]
    [Theory]
    [TestPriority(102400)]
    public async Task<int> SetSessionUserAppConfigAsync(SetSessionUserAppConfigReq req)
    {
        var rsp = await PostJsonAsync(typeof(UserController), req);
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }

    /// <inheritdoc />
    [Fact]
    [TestPriority(102500)]
    public async Task<UserInfoRsp> UserInfoAsync()
    {
        var rsp = await PostJsonAsync(typeof(UserController));
        Assert.True(rsp.IsSuccessStatusCode);
        return default;
    }
}