using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="IToolsApi" />
public class ToolsApi : ApiBase<IToolsApi>, IToolsApi
{
    /// <inheritdoc cref="IToolsApi" />
    public ToolsApi() { }

    /// <inheritdoc />
    public dynamic EnvironmentInfo()
    {
        return EnvironmentInfoInternal();
    }

    /// <inheritdoc />
    [AllowAnonymous]
    public DateTime GetServerUtcTime()
    {
        return DateTime.UtcNow;
    }

    /// <inheritdoc />
    [AllowAnonymous]
    public string Version()
    {
        return VersionInternal();
    }

    internal static Dictionary<string, object> EnvironmentInfoInternal()
    {
        return typeof(Environment).GetProperties(BindingFlags.Public | BindingFlags.Static)
                                  .Where(x => x.Name != nameof(Environment.StackTrace))
                                  .ToDictionary(x => x.Name, x => x.GetValue(null));
    }

    internal static string VersionInternal()
    {
        return typeof(ToolsApi).Assembly.GetName().Version!.ToString();
    }
}