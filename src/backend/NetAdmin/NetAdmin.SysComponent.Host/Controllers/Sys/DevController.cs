using NetAdmin.Domain.Dto.Sys.Dev;

namespace NetAdmin.SysComponent.Host.Controllers.Sys;

/// <summary>
///     开发服务
/// </summary>
[ApiDescriptionSettings(nameof(Sys), Module = nameof(Sys))]
[Produces(Chars.FLG_HTTP_HEADER_VALUE_APPLICATION_JSON)]
public sealed class DevController(IDevCache cache) : ControllerBase<IDevCache, IDevService>(cache), IDevModule
{
    /// <summary>
    ///     生成后端代码
    /// </summary>
    public Task GenerateCsCodeAsync(GenerateCsCodeReq req) {
        return Cache.GenerateCsCodeAsync(req);
    }

    /// <summary>
    ///     生成图标代码
    /// </summary>
    public Task GenerateIconCodeAsync(GenerateIconCodeReq req) {
        return Cache.GenerateIconCodeAsync(req);
    }

    /// <summary>
    ///     生成接口代码
    /// </summary>
    public Task GenerateJsCodeAsync() {
        return Cache.GenerateJsCodeAsync();
    }

    /// <summary>
    ///     获取实体项目列表
    /// </summary>
    public Task<IEnumerable<Tuple<string, string>>> GetDomainProjectsAsync() {
        return Cache.GetDomainProjectsAsync();
    }

    /// <summary>
    ///     获取所有数据类型
    /// </summary>
    public IEnumerable<string> GetDotnetDataTypes(GetDotnetDataTypesReq req) {
        return Cache.GetDotnetDataTypes(req);
    }

    /// <summary>
    ///     获取实体基类列表
    /// </summary>
    public IEnumerable<Tuple<string, string>> GetEntityBaseClasses() {
        return Cache.GetEntityBaseClasses();
    }

    /// <summary>
    ///     获取字段接口列表
    /// </summary>
    public IEnumerable<Tuple<string, string>> GetFieldInterfaces() {
        return Cache.GetFieldInterfaces();
    }
}