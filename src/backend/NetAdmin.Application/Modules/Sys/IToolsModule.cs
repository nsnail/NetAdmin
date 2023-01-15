namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     工具模块
/// </summary>
public interface IToolsModule
{
    /// <summary>
    ///     运行环境
    /// </summary>
    dynamic EnvironmentInfo();

    /// <summary>
    ///     服务器时间
    /// </summary>
    DateTime GetServerUtcTime();

    /// <summary>
    ///     版本信息
    /// </summary>
    string Version();
}