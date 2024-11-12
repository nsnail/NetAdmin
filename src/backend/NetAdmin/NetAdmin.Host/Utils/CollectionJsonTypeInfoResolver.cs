namespace NetAdmin.Host.Utils;

/// <summary>
///     处理集合类型的Json解析器
/// </summary>
public sealed class CollectionJsonTypeInfoResolver : DefaultJsonTypeInfoResolver
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CollectionJsonTypeInfoResolver" /> class.
    /// </summary>
    public CollectionJsonTypeInfoResolver()
    {
        Modifiers.Add(CollectionValueModifier);
    }

    private static void CollectionValueModifier(JsonTypeInfo typeInfo)
    {
        foreach (var property in typeInfo.Properties) {
            // 跳过非集合属性
            if (!property.PropertyType.GetInterfaces().Contains(typeof(IEnumerable))) {
                continue;
            }

            // 跳过字符串
            if (property.PropertyType == typeof(string)) {
                continue;
            }

            var memberName = GetMemberName(property);

            // object->json 只在count>0时返回其值，否则返回null
            property.Get = PropertyGet(memberName);

            // json->object 时 为对象分配属性， 改为只在count>0分配 ，否则分配null，而不是分配[]
            property.Set = PropertySet(memberName);
        }
    }

    private static string GetMemberName(JsonPropertyInfo property)
    {
        return property.GetType().GetRuntimeProperties().First(x => x.Name == "MemberName").GetValue(property) as string;
    }

    /// <summary>
    ///     这里处理子类new隐藏父类同名属性， 取得多个同名属性的问题
    /// </summary>
    private static PropertyInfo GetNewProperty(string memberName, object obj)
    {
        return obj.GetType().GetProperties().Where(x => x.Name == memberName).First(x => x.DeclaringType == x.ReflectedType);
    }

    /// <summary>
    ///     object->json 只在count>0时返回其值，否则返回null
    /// </summary>
    private static Func<object, object> PropertyGet(string memberName)
    {
        return obj => {
            object prop;
            try {
                prop = obj.GetType().GetProperty(memberName!)?.GetValue(obj);
            }
            catch (AmbiguousMatchException) {
                // 这里处理子类new隐藏父类同名属性， 取得多个同名属性的问题
                prop = GetNewProperty(memberName, obj).GetValue(obj);
            }

            return prop switch { string => prop, ICollection { Count: > 0 } => prop, _ => null };
        };
    }

    /// <summary>
    ///     json->object 时 为对象分配属性， 改为只在count>0分配 ，否则分配null，而不是分配[]
    /// </summary>
    private static Action<object, object> PropertySet(string memberName)
    {
        return (obj, val) => {
            val = val switch { string => val, ICollection { Count: > 0 } => val, _ => null };
            try {
                obj.GetType().GetProperty(memberName!)?.SetValue(obj, val);
            }
            catch (AmbiguousMatchException) {
                GetNewProperty(memberName, obj).SetValue(obj, val);
            }
        };
    }
}