namespace NetAdmin.Infrastructure;

/// <summary>
///     全局静态类
/// </summary>
public static class Global
{
    /// <summary>
    ///     产品版本
    /// </summary>
    public static readonly string ProductVersion
        = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()!.Location).ProductVersion;

    /// <summary>
    ///     Json序列化选项
    /// </summary>
    public static JsonSerializerOptions JsonSerializerOptions { get; set; }
}