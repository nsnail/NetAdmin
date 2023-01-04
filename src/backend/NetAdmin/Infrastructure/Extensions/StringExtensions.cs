using NetAdmin.Infrastructure.Constant;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     String  扩展方法
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     去掉尾部字符串“Api”
    /// </summary>
    public static string TrimEndApi(this string me)
    {
        return Regexes.RegexEndWithApi.Replace(me, string.Empty);
    }

    /// <summary>
    ///     去掉尾部字符串“Options”
    /// </summary>
    public static string TrimEndOptions(this string me)
    {
        return Regexes.RegexEndWithOptions.Replace(me, string.Empty);
    }
}