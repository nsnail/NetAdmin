using System.Reflection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using NetAdmin.DataContract;

namespace NetAdmin.Application.Service.Sys.Implements;

/// <inheritdoc cref="IToolsService" />
public class ToolsService : ServiceBase<IToolsService>, IToolsService, IDynamicApiController
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ToolsService" /> class.
    /// </summary>
    public ToolsService(ContextUser user) //
        : base(user) { }

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
    [AllowAnonymous]
    public DateTime GetServerUtcTime()
    {
        return DateTime.UtcNow;
    }

    /// <inheritdoc />
    [AllowAnonymous]
    public string Version()
    {
        return VersionStatic();
    }
}