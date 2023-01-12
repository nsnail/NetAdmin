using System.Text.Json;
using System.Text.Json.Serialization;
using Furion.DependencyInjection;
using NetAdmin.Infrastructure.Configuration;
using NSExt.Extensions;

namespace NetAdmin.Extensions;

/// <summary>
///     IMvcBuilder 扩展方法
/// </summary>
[SuppressSniffer]
public static class MvcBuilderExtensions
{
    /// <summary>
    ///     Json序列化配置
    /// </summary>
    public static IMvcBuilder AddJsonSerializer(this IMvcBuilder me)
    {
        return me.AddJsonOptions(options => {
            // 加载基本的序列化/反序列化规则
            options.JsonSerializerOptions.CopyFrom(default(JsonSerializerOptions).NewJsonSerializerOptions());

            // 日期格式
            options.JsonSerializerOptions.Converters.AddDateTimeTypeConverters("yyyy-MM-dd HH:mm:ss");

            // 枚举值显示Name（小驼峰）而非数字
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            // 读到String.Emtpy统一转为Null
            options.JsonSerializerOptions.Converters.Add(new ToNullIfReadEmptyStringConverter());

            // 允许读取引号包围的数字
            options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        });
    }
}