using System.Reflection;
using NetAdmin.Application.Services.Sys.Dependency;

namespace NetAdmin.Application.Services.Sys;

/// <inheritdoc cref="IToolsService" />
public class ToolsService : ServiceBase<IToolsService>, IToolsService
{
    /// <summary>
    ///     <see cref="EnvironmentInfo" />
    /// </summary>
    public static Dictionary<string, object> EnvironmentInfoStatic()
    {
        return typeof(Environment).GetProperties(BindingFlags.Public | BindingFlags.Static)
                                  .Where(x => x.Name != nameof(Environment.StackTrace))
                                  .ToDictionary(x => x.Name, x => x.GetValue(null));
    }

    /// <summary>
    ///     <see cref="Version" />
    /// </summary>
    public static string VersionStatic()
    {
        return typeof(ToolsService).Assembly.GetName().Version!.ToString();
    }

    /// <inheritdoc />
    public dynamic EnvironmentInfo()
    {
        return EnvironmentInfoStatic();
    }

    /// <inheritdoc />
    public DateTime GetServerUtcTime()
    {
        return DateTime.UtcNow;
    }

    /// <inheritdoc />
    public string Version()
    {
        return VersionStatic();
    }
}