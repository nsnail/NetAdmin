namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     String 扩展方法
/// </summary>
public static class StringExtensions
{
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
    public static string TrimAsyncSuffix(this string me)
    {
        return me.TrimSuffix("Async");
    }

    /// <summary>
    ///     去掉尾部字符串“Options”
    /// </summary>
    public static string TrimOptionsSuffix(this string me)
    {
        return me.TrimSuffix("Options");
    }
}