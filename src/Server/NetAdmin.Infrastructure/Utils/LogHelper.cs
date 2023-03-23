namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     日志帮助类
/// </summary>
public static class LogHelper
{
    /// <summary>
    ///     获取ILogger
    /// </summary>
    public static ILogger<T> Get<T>()
    {
        return App.GetRequiredService<ILogger<T>>();
    }
}