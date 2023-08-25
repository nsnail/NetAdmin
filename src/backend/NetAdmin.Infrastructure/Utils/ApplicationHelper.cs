namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     应用程序帮助类
/// </summary>
public static class ApplicationHelper
{
    /// <summary>
    ///     获取系统环境
    /// </summary>
    public static Dictionary<string, object> GetEnvironmentInfo()
    {
        var ret = typeof(Environment).GetProperties(BindingFlags.Public | BindingFlags.Static)
                                     .Where(x => x.Name != nameof(Environment.StackTrace))
                                     .ToDictionary(x => x.Name, x => x.GetValue(null));
        _ = ret.TryAdd( //
            "Environment", Environment.GetEnvironmentVariables().ToJson());
        return ret;
    }
}