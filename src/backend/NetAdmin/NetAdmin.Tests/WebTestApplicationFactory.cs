using Microsoft.AspNetCore.Mvc.Testing;

namespace NetAdmin.Tests;

/// <summary>
///     Web 测试应用程序工厂
/// </summary>
/// <typeparam name="T"></typeparam>
public class WebTestApplicationFactory<T> : WebApplicationFactory<T>
    where T : class
{
    /// <summary>
    ///     测试用户
    /// </summary>
    public const string TEST_ACCOUNT = "root";

    /// <summary>
    ///     测试密码
    /// </summary>
    public const string TEST_PASSWORD = "1234qwer";
}