namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     类型扩展方法
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    ///     递归获取类型的属性
    /// </summary>
    /// <param name="type">要查找的类型</param>
    /// <param name="propertyPath">属性路径，如"a.b.c"</param>
    /// <returns>找到的属性信息，如果路径中任何属性不存在则返回null</returns>
    public static PropertyInfo GetRecursiveProperty(this Type type, string propertyPath)
    {
        var          properties   = propertyPath.Split('.');
        PropertyInfo propertyInfo = null;
        var          currentType  = type;

        foreach (var propertyName in properties) {
            propertyInfo = currentType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (propertyInfo == null) {
                return null;
            }

            currentType = propertyInfo.PropertyType;
        }

        return propertyInfo;
    }
}