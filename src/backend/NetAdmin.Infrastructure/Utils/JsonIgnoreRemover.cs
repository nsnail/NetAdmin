namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     忽略 JsonIgnore 特性
/// </summary>
public static class JsonIgnoreRemover
{
    private delegate TValue RefFunc<TObject, out TValue>(ref TObject arg);

    /// <summary>
    ///     忽略 JsonIgnore 特性
    /// </summary>
    public static Action<JsonTypeInfo> RemoveJsonIgnore(Type type)
    {
        return typeInfo => {
            if (!type.IsAssignableFrom(typeInfo.Type) || typeInfo.Kind != JsonTypeInfoKind.Object) {
                return;
            }

            foreach (var property in typeInfo.Properties.Where(property => property.ShouldSerialize != null &&
                                                                           property.AttributeProvider?.IsDefined(typeof(JsonIgnoreAttribute), true) ==
                                                                           true)) {
                property.Get ??= CreatePropertyGetter(property);
                property.Set ??= CreatePropertySetter(property);
                if (property.Get != null) {
                    property.ShouldSerialize = null;
                }
            }
        };
    }

    private static Func<object, object> CreateGetter(Type type, MethodInfo method)
    {
        if (method == null) {
            return null;
        }

        #pragma warning disable S3011
        var myMethod = typeof(JsonIgnoreRemover).GetMethod(nameof(CreateGetterGeneric), BindingFlags.NonPublic | BindingFlags.Static)!;
        #pragma warning restore S3011
        return (Func<object, object>)myMethod.MakeGenericMethod(type, method.ReturnType).Invoke(null, [method])!;
    }

    private static Func<object, object> CreateGetterGeneric<TObject, TValue>(MethodInfo method)
    {
        ArgumentNullException.ThrowIfNull(method);

        if (typeof(TObject).IsValueType) {
            var func = (RefFunc<TObject, TValue>)Delegate.CreateDelegate(typeof(RefFunc<TObject, TValue>), null, method);
            return o => {
                var tObj = (TObject)o;
                return func(ref tObj);
            };
        }
        else {
            var func = (Func<TObject, TValue>)Delegate.CreateDelegate(typeof(Func<TObject, TValue>), method);
            return o => func((TObject)o);
        }
    }

    private static Func<object, object> CreatePropertyGetter(JsonPropertyInfo property)
    {
        return property.AttributeProvider as PropertyInfo is { ReflectedType: not null } info && info.GetGetMethod() is { } getMethod
            ? CreateGetter(info.ReflectedType, getMethod)
            : null;
    }

    private static Action<object, object> CreatePropertySetter(JsonPropertyInfo property)
    {
        return property.AttributeProvider as PropertyInfo is { ReflectedType: not null } info && info.GetSetMethod() is { } setMethod
            ? CreateSetter(info.ReflectedType, setMethod)
            : null;
    }

    private static Action<object, object> CreateSetter(Type type, MethodInfo method)
    {
        if (method == null) {
            return null;
        }

        #pragma warning disable S3011
        var myMethod = typeof(JsonIgnoreRemover).GetMethod(nameof(CreateSetterGeneric), BindingFlags.NonPublic | BindingFlags.Static)!;
        #pragma warning restore S3011
        return (Action<object, object>)myMethod.MakeGenericMethod(type, method.GetParameters().Single().ParameterType).Invoke(null, [method])!;
    }

    private static Action<object, object> CreateSetterGeneric<TObject, TValue>(MethodInfo method)
    {
        ArgumentNullException.ThrowIfNull(method);

        if (typeof(TObject).IsValueType) {
            return (o, v) => method.Invoke(o, [v]);
        }

        var func = (Action<TObject, TValue>)Delegate.CreateDelegate(typeof(Action<TObject, TValue>), method);
        return (o, v) => func((TObject)o, (TValue)v);
    }
}