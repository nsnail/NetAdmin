using NetAdmin.Domain.Dto.Sys.Tool;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     工具模块
/// </summary>
public interface IToolsModule
{
    /// <summary>
    ///     Aes解密
    /// </summary>
    string AesDecode(AesDecodeReq req);

    /// <summary>
    ///     执行SQL语句
    /// </summary>
    Task<object[][]> ExecuteSqlAsync(ExecuteSqlReq req);

    /// <summary>
    ///     获取更新日志
    /// </summary>
    Task<string> GetChangeLogAsync();

    /// <summary>
    ///     获取模块信息
    /// </summary>
    Task<IEnumerable<GetModulesRsp>> GetModulesAsync();

    /// <summary>
    ///     获取服务器时间
    /// </summary>
    Task<DateTime> GetServerUtcTimeAsync();

    /// <summary>
    ///     获取版本信息
    /// </summary>
    Task<string> GetVersionAsync();
}