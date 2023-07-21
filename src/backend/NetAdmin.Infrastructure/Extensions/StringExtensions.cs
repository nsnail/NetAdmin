namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     String 扩展类
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     object -> json
    /// </summary>
    public static T ToObject<T>(this string me)
    {
        return me.Object<T>(Global.JsonSerializerOptions);
    }

    /// <summary>
    ///     object -> json
    /// </summary>
    public static object ToObject(this string me, Type toType)
    {
        return me.Object(toType, Global.JsonSerializerOptions);
    }

    /// <summary>
    ///     去掉尾部字符串“Options”
    /// </summary>
    public static string TrimEndOptions(this string me)
    {
        return Regex.Replace(me, "Options$", string.Empty);
    }
}