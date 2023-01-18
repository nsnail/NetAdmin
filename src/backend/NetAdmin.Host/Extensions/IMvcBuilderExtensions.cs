using System.Text.Json;
using System.Text.Json.Serialization;
using Furion.DependencyInjection;
using NetAdmin.Host.Filters;
using NetAdmin.Host.Utils;
using NSExt.Extensions;

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
    public static IMvcBuilder AddJsonSerializer(this IMvcBuilder me)
    {
        return me.AddJsonOptions(options => {
            // 加载基本的序列化/反序列化规则
            options.JsonSerializerOptions.CopyFrom(default(JsonSerializerOptions).NewJsonSerializerOptions());

            // 日期格式
            options.JsonSerializerOptions.Converters.AddDateTimeTypeConverters(Chars.TPL_DATE_YYYY_MM_DD_HH_MM_SS);

            // 枚举值显示Name（小驼峰）而非数字
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            // 读到String.Emtpy统一转为Null
            options.JsonSerializerOptions.Converters.Add(new ToNullIfReadEmptyStringConverter());

            // 写入时，忽略默认值
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;

            // 允许读取引号包围的数字
            options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        });
    }
}