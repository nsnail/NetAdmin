namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     枚举扩展方法
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    ///     通过类泛型类型获取特性
    /// </summary>
    public static T Attr<T>(this Enum me)
        where T : Attribute
    {
        return me.GetType().GetMember(me.ToString())[0].GetCustomAttributes<T>(false).FirstOrDefault();
    }
}