namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     常量模块
/// </summary>
public interface IConstantModule
{
    /// <summary>
    ///     获得常量字符串
    /// </summary>
    IDictionary<string, string> GetCharsDic();

    /// <summary>
    ///     获得公共枚举值
    /// </summary>
    IDictionary<string, Dictionary<string, string>> GetEnums();

    /// <summary>
    ///     获得本地化字符串
    /// </summary>
    IDictionary<string, string> GetLocalizedStrings();

    /// <summary>
    ///     获得数字常量表
    /// </summary>
    IDictionary<string, long> GetNumbersDic();
}