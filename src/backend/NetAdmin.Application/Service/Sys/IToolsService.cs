namespace NetAdmin.Application.Service.Sys;

/// <summary>
///     工具服务
/// </summary>
public interface IToolsService
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