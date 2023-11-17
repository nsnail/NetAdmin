namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     String 扩展类
/// </summary>
public static class StringExtensions
{
    private static readonly Regex _regex = new("Options$");

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
    ///     去掉尾部字符串“Options”
    /// </summary>
    public static string TrimEndOptions(this string me)
    {
        return _regex.Replace(me, string.Empty);
    }
}