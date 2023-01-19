using System.Collections;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Furion.DependencyInjection;
using NetAdmin.Host.Filters;
using NetAdmin.Host.Utils;

namespace NetAdmin.Host.Extensions;

/// <summary>
///     IMvcBuilder 扩展方法
/// </summary>
[SuppressSniffer]

// ReSharper disable once InconsistentNaming
public static class IMvcBuilderExtensions
{
    /// <summary>
    ///     api结果处理器
    /// </summary>
    public static IMvcBuilder AddApiResultHander(this IMvcBuilder me)
    {
        return me.AddInjectWithUnifyResult<ApiResultHandler>(injectOptions => {
            // 替换自定义的EnumSchemaFilter，支持多语言Resx资源 （需将SpecificationDocumentSettings.EnableEnumSchemaFilter配置为false)
            injectOptions.ConfigureSwaggerGen(genOptions => { genOptions.SchemaFilter<SwaggerEnumSchemaFixer>(); });
        });
    }

    /// <summary>
    ///     Json序列化配置
    /// </summary>
    /// <remarks>
    ///     正反序列化规则：
    ///     object->json：
    ///     1、值为 null 或 default 的节点将被忽略
    ///     2、值为 "" 的节点将被忽略。
    ///     3、值为 [] 的节点将被忽略。
    ///     4、节点名：大驼峰转小驼峰
    ///     5、不转义除对json结构具有破坏性（如")以外的任何字符
    ///     6、大数字原样输出（不加引号），由前端处理js大数兼容问题
    ///     json->object:
    ///     1、允许带注释的json（自动忽略）
    ///     2、允许尾随逗号
    ///     3、节点名大小写不敏感
    ///     4、允许带双引号的数字
    ///     5、值为"" 转 null
    ///     6、值为[] 转 null
    /// </remarks>
    public static IMvcBuilder AddJsonSerializer(this IMvcBuilder me)
    {
        return me.AddJsonOptions(options => {
            ////////////////////////////// json -> object

            // 允许带注释
            options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;

            // 允许尾随逗号
            options.JsonSerializerOptions.AllowTrailingCommas = true;

            // 允许数字带双引号
            options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;

            // 大小写不敏感
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

            // 允许读取引号包围的数字
            options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;

            ///////////////////////////// object -> json

            #if DEBUG

            // 开启缩进
            options.JsonSerializerOptions.WriteIndented = true;
            #endif

            // 转小驼峰
            options.JsonSerializerOptions.DictionaryKeyPolicy  = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            // 不严格转义
            options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            // 写入时，忽略null、default
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;

            ////////////////////////////// object <-> json

            // "" 转 null 双向
            options.JsonSerializerOptions.Converters.Add(new ToNullIfEmptyStringConverter());

            // [] 转 null 双向
            options.JsonSerializerOptions.TypeInfoResolver
                = new DefaultJsonTypeInfoResolver { Modifiers = { CollectionValueModifier } };

            static void CollectionValueModifier(JsonTypeInfo typeInfo)
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

                    var memberName = property.GetType()
                                             .GetRuntimeProperties()
                                             .First(x => x.Name == "MemberName")
                                             .GetValue(property) as string;

                    // object->json 只在count>0时返回其值，否则返回null
                    property.Get = obj => {
                        object prop;
                        try {
                            prop = obj.GetType().GetProperty(memberName!)?.GetValue(obj);
                        }
                        catch (AmbiguousMatchException) {
                            // 这里处理子类new隐藏父类同名属性， 取得多个同名属性的问题
                            prop = obj.GetType()
                                      .GetProperties()
                                      .Where(x => x.Name          == memberName)
                                      .First(x => x.DeclaringType == x.ReflectedType)
                                      .GetValue(obj);
                        }

                        return prop switch { string => prop, ICollection { Count: > 0 } => prop, _ => null };
                    };

                    // json->object 时 为对象分配属性， 改为只在count>0分配 ，否则分配null，而不是分配[]
                    property.Set = (obj, val) => {
                        val = val switch { string => val, ICollection { Count: > 0 } => val, _ => null };
                        try {
                            obj.GetType().GetProperty(memberName!)?.SetValue(obj, val);
                        }
                        catch (AmbiguousMatchException) {
                            obj.GetType()
                               .GetProperties()
                               .Where(x => x.Name          == memberName)
                               .First(x => x.DeclaringType == x.ReflectedType)
                               .SetValue(obj, val);
                        }
                    };
                }
            }

            // 日期格式 2023-01-18 20:02:12
            options.JsonSerializerOptions.Converters.AddDateTimeTypeConverters(Chars.TPL_DATE_YYYY_MM_DD_HH_MM_SS);

            // object->json 枚举显名 而非数字 ，json->object 可以枚举名 也可以数值
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });
    }
}