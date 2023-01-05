using System.Text.Json;
using System.Text.Json.Serialization;
using NSExt.Extensions;

namespace NetAdmin.Infrastructure.Extensions;

/// <summary>
///     IMvcBuilder 扩展方法
/// </summary>
public static class MvcBuilderExtensions
{
    /// <summary>
    ///     Json序列化配置
    /// </summary>
    public static IMvcBuilder AddJsonSerializer(this IMvcBuilder me)
    {
        return me.AddJsonOptions(options => {
            options.JsonSerializerOptions.CopyFrom(default(JsonSerializerOptions).NewJsonSerializerOptions());
            options.JsonSerializerOptions.Converters.AddDateTimeTypeConverters("yyyy-MM-dd HH:mm:ss");
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        });
    }
}