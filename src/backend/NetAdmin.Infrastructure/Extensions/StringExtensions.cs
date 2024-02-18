namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     String 扩展方法
/// </summary>
public static class StringExtensions
{
    private static readonly Regex _regex  = new("Options$");
    private static readonly Regex _regex2 = new("Async$");

    /// <summary>
    ///     object -> json
    /// </summary>
    public static T ToObject<T>(this string me)
    {
        return me.Object<T>(GlobalStatic.JsonSerializerOptions);
    }

    /// <summary>
    ///     object -> json
    /// </summary>
    public static object ToObject(this string me, Type toType)
    {
        return me.Object(toType, GlobalStatic.JsonSerializerOptions);
    }

    /// <summary>
    ///     去掉尾部字符串“Async”
    /// </summary>
    #pragma warning disable RCS1047, ASA002, VSTHRD200
    public static string TrimEndAsync(this string me)
        #pragma warning restore VSTHRD200, ASA002, RCS1047
    {
        return _regex2.Replace(me, string.Empty);
    }

    /// <summary>
    ///     去掉尾部字符串“Options”
    /// </summary>
    public static string TrimEndOptions(this string me)
    {
        return _regex.Replace(me, string.Empty);
    }
}