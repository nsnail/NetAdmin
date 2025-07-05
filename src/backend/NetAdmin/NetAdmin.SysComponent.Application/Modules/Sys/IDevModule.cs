using NetAdmin.Domain.Dto.Sys.Dev;

namespace NetAdmin.SysComponent.Application.Modules.Sys;

/// <summary>
///     开发模块
/// </summary>
public interface IDevModule
{
    /// <summary>
    ///     生成后端代码
    /// </summary>
    Task GenerateCsCodeAsync(GenerateCsCodeReq req);

    /// <summary>
    ///     生成图标代码
    /// </summary>
    Task GenerateIconCodeAsync(GenerateIconCodeReq req);

    /// <summary>
    ///     生成接口代码
    /// </summary>
    Task GenerateJsCodeAsync();

    /// <summary>
    ///     获取实体项目列表
    /// </summary>
    Task<IEnumerable<Tuple<string, string>>> GetDomainProjectsAsync();

    /// <summary>
    ///     获取所有数据类型
    /// </summary>
    IEnumerable<string> GetDotnetDataTypes(GetDotnetDataTypesReq req);

    /// <summary>
    ///     获取实体基类列表
    /// </summary>
    IEnumerable<Tuple<string, string>> GetEntityBaseClasses();

    /// <summary>
    ///     获取字段接口列表
    /// </summary>
    IEnumerable<Tuple<string, string>> GetFieldInterfaces();
}