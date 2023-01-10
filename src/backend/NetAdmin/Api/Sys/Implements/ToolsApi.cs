using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="IToolsApi" />
public class ToolsApi : ApiBase<IToolsApi>, IToolsApi
{
    /// <inheritdoc />
    public dynamic EnvironmentInfo()
    {
        return typeof(Environment).GetProperties(BindingFlags.Public | BindingFlags.Static)
                                  .Where(x => x.Name != nameof(Environment.StackTrace))
                                  .ToDictionary(x => x.Name, x => x.GetValue(null));
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
        return typeof(ToolsApi).Assembly.GetName().Version!.ToString();
    }
}