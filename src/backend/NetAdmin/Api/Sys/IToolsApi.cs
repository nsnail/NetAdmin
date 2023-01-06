namespace NetAdmin.Api.Sys;

/// <summary>
///     工具接口
/// </summary>
public interface IToolsApi : IRestfulApi
{
    /// <summary>
    ///     服务器时间
    /// </summary>
    DateTime GetServerUtcTime();

    /// <summary>
    ///     系统信息
    /// </summary>
    string SystemInfo();

    /// <summary>
    ///     版本信息
    /// </summary>
    string Version();
}