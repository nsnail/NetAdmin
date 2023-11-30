namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     Object 扩展方法
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    ///     object -> json
    /// </summary>
    public static string ToJson(this object me)
    {
        return me.Json(GlobalStatic.JsonSerializerOptions);
    }
}