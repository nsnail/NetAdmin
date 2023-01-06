using Microsoft.AspNetCore.Authorization;

namespace NetAdmin.Api.Sys.Implements;

/// <inheritdoc cref="IToolsApi" />
public class ToolsApi : ApiBase<IToolsApi>, IToolsApi
{
    /// <inheritdoc />
    [AllowAnonymous]
    public DateTime GetServerUtcTime()
    {
        return DateTime.UtcNow;
    }

    /// <inheritdoc />
    [AllowAnonymous]
    public string SystemInfo()
    {
        return string.Empty;
    }

    /// <inheritdoc />
    [AllowAnonymous]
    public string Version()
    {
        return typeof(ToolsApi).Assembly.GetName().Version!.ToString();
    }
}