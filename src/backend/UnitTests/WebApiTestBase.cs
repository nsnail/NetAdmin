using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using NetAdmin.Domain.Dto;
using NetAdmin.Domain.Dto.Sys.User;
using NetAdmin.Host.Extensions;

namespace UnitTests;

/// <summary>
///     WebApi 测试用例基类
/// </summary>
public abstract class WebApiTestBase<T>(WebTestApplicationFactory<T> factory, ITestOutputHelper testOutputHelper)
    : IClassFixture<WebTestApplicationFactory<T>>
    where T : AppStartup
{
    // ReSharper disable once StaticMemberInGenericType
    private static string _accessToken;

    /// <summary>
    ///     Post请求
    /// </summary>
    protected async Task<HttpResponseMessage> PostAsync(Type type, HttpContent content, [CallerMemberName] string memberName = null)
    {
        var client = factory.CreateClient();
        await Authorization(client);

        var ret = await client.PostAsync(type.GetMethod(memberName!).GetRoutePath(factory.Services), content).ConfigureAwait(false);
        testOutputHelper.WriteLine(await ret.Content.ReadAsStringAsync().ConfigureAwait(false));
        return ret;
    }

    /// <summary>
    ///     Post请求
    /// </summary>
    protected async Task<HttpResponseMessage> PostJsonAsync<TRequest>(Type type, TRequest req = default, [CallerMemberName] string memberName = null)
    {
        return await PostAsync(type, req == null ? JsonContent.Create(new { }) : JsonContent.Create(req), memberName);
    }

    /// <summary>
    ///     Post请求
    /// </summary>
    protected async Task<HttpResponseMessage> PostJsonAsync(Type type, [CallerMemberName] string memberName = null)
    {
        return await PostJsonAsync<object>(type, null, memberName);
    }

    private static async Task Authorization(HttpClient client)
    {
        if (_accessToken == null) {
            var req = new LoginByPwdReq //
                      {
                          Password
                              = Environment.GetEnvironmentVariable(nameof(WebTestApplicationFactory<>.TEST_PASSWORD)) ??
                                WebTestApplicationFactory<T>.TEST_PASSWORD
                        , Account = Environment.GetEnvironmentVariable(nameof(WebTestApplicationFactory<>.TEST_ACCOUNT)) ??
                                    WebTestApplicationFactory<T>.TEST_ACCOUNT
                      };
            var loginAccount = JsonContent.Create(req);
            var rspMsg = await client.PostAsync( //
                                         Chars.FLG_PATH_API_SYS_USER_LOGIN_BY_PWD, loginAccount)
                                     .ConfigureAwait(false);
            var rspObj = (await rspMsg.Content.ReadAsStringAsync().ConfigureAwait(false)).ToObject<RestfulInfo<LoginRsp>>();
            _accessToken = rspObj.Data.AccessToken;
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Chars.FLG_HTTP_HEADER_VALUE_AUTH_SCHEMA, _accessToken);
    }
}