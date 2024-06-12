using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using NetAdmin.Domain.Dto;
using NetAdmin.Domain.Dto.Sys.User;
using Xunit;
using Xunit.Abstractions;

namespace NetAdmin.Tests;

/// <summary>
///     WebApi 测试用例基类
/// </summary>
public abstract class WebApiTestBase<T>(WebApplicationFactory<T> factory, ITestOutputHelper testOutputHelper)
    : IClassFixture<WebApplicationFactory<T>>
    where T : AppStartup
{
    private const string _ACCOUNT  = "root";
    private const string _PASSWORD = "1234qwer";
    private       string _accessToken;

    /// <summary>
    ///     Post请求
    /// </summary>
    protected async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
    {
        var client = factory.CreateClient();
        if (_accessToken == null) {
            var loginRsp = await client.PostAsync(Chars.FLG_PATH_API_SYS_USER_LOGIN_BY_PWD
                                                , JsonContent.Create(
                                                      new LoginByPwdReq { Password = _PASSWORD, Account = _ACCOUNT }))
                                       .ConfigureAwait(false);
            var loginRspObj = (await loginRsp.Content.ReadAsStringAsync().ConfigureAwait(false))
                .ToObject<RestfulInfo<LoginRsp>>();
            _accessToken = loginRspObj.Data.AccessToken;
        }

        client.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue(Chars.FLG_HTTP_HEADER_VALUE_AUTH_SCHEMA, _accessToken);
        var ret = await client.PostAsync(url, content).ConfigureAwait(false);
        testOutputHelper.WriteLine(await ret.Content.ReadAsStringAsync().ConfigureAwait(false));
        return ret;
    }
}