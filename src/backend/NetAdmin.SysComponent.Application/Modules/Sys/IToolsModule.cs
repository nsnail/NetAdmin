namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     工具模块
/// </summary>
public interface IToolsModule
{
    /// <summary>
    ///     服务器时间
    /// </summary>
    Task<DateTime> GetServerUtcTimeAsync();

    /// <summary>
    ///     版本信息
    /// </summary>
    Task<string> VersionAsync();
}