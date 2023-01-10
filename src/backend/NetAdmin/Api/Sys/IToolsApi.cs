namespace NetAdmin.Api.Sys;

/// <summary>
///     工具接口
/// </summary>
public interface IToolsApi : IRestfulApi
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