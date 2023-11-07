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
                                     .Where(x => x.Name is not (nameof(Environment.StackTrace)
                                                                or nameof(Environment.NewLine)))
                                     .ToDictionary(x => x.Name, x => x.GetValue(null));

        var vars = Environment.GetEnvironmentVariables();
        var keys = new ArrayList(vars.Keys);
        keys.Sort();
        var sb = new StringBuilder(vars.Count);
        foreach (var key in keys) {
            _ = sb.AppendLine(CultureInfo.InvariantCulture, $"{key}: {vars[key]}");
        }

        _ = ret.TryAdd("EnvironmentVars", sb.ToString().Trim());
        return ret;
    }
}