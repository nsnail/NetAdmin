namespace UnitTests.Sys;

/// <summary>
///     常量测试
/// </summary>
[SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters")]
[SuppressMessage("Usage", "xUnit1028:Test method must have valid return type")]
[SuppressMessage("Usage", "xUnit1031:Do not use blocking task operations in test method")]
public class ConstantTests(WebTestApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
    : WebApiTestBase<Startup>(factory, testOutputHelper), IConstantModule
{
    /// <inheritdoc />
    [Fact]
    public IDictionary<string, string> GetCharsDic()
    {
        var rsp = PostJsonAsync(typeof(ConstantController)).GetAwaiter().GetResult();
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [Fact]
    public IDictionary<string, Dictionary<string, string[]>> GetEnums()
    {
        var rsp = PostJsonAsync(typeof(ConstantController)).GetAwaiter().GetResult();
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [Fact]
    public IDictionary<string, string> GetLocalizedStrings()
    {
        var rsp = PostJsonAsync(typeof(ConstantController)).GetAwaiter().GetResult();
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }

    /// <inheritdoc />
    [Fact]
    public IDictionary<string, long> GetNumbersDic()
    {
        var rsp = PostJsonAsync(typeof(ConstantController)).GetAwaiter().GetResult();
        Assert.True(rsp.IsSuccessStatusCode);
        return null;
    }
}