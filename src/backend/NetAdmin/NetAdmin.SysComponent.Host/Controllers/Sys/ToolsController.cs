using NetAdmin.SysComponent.Domain.Dto.Sys.Tool;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     工具服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class ToolsController(IToolsCache cache) : ControllerBase<IToolsCache, IToolsService>(cache), IToolsModule
{
    /// <summary>
    ///     Aes解密
    /// </summary>
    public string AesDecode(AesDecodeReq req)
    {
        return Cache.AesDecode(req);
    }

    /// <summary>
    ///     执行SQL语句
    /// </summary>
    public Task<object[][]> ExecuteSqlAsync(ExecuteSqlReq req)
    {
        return Cache.ExecuteSqlAsync(req);
    }

    /// <summary>
    ///     获取更新日志
    /// </summary>
    [AllowAnonymous]
    public Task<string> GetChangeLogAsync()
    {
        return Cache.GetChangeLogAsync();
    }

    /// <summary>
    ///     获取模块信息
    /// </summary>
    [AllowAnonymous]
    public Task<IEnumerable<GetModulesRsp>> GetModulesAsync()
    {
        return Cache.GetModulesAsync();
    }

    /// <summary>
    ///     获取服务器时间
    /// </summary>
    [AllowAnonymous]
    public Task<DateTime> GetServerUtcTimeAsync()
    {
        return Cache.GetServerUtcTimeAsync();
    }

    /// <summary>
    ///     获取版本信息
    /// </summary>
    [AllowAnonymous]
    public Task<string> GetVersionAsync()
    {
        return Cache.GetVersionAsync();
    }
}