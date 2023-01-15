namespace NetAdmin.Application.Modules.Sys;

/// <summary>
///     常量模块
/// </summary>
public interface IConstantModule
{
    /// <summary>
    ///     获得公共枚举值
    /// </summary>
    dynamic GetEnums();

    /// <summary>
    ///     获得本地化字符串
    /// </summary>
    object GetLocalizedStrings();

    /// <summary>
    ///     获得常量字符串
    /// </summary>
    dynamic GetStrings();
}