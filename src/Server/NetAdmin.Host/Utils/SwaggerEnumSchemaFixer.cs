using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetAdmin.Host.Utils;

/// <summary>
///     修正 规范化文档 Enum 提示
/// </summary>
[SuppressSniffer]
public class SwaggerEnumSchemaFixer : ISchemaFilter
{
    /// <summary>
    ///     实现过滤器方法
    /// </summary>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // 非枚举退出
        if (!context.Type.IsEnum) {
            return;
        }

        const string wrap = "<br />";
        schema.Enum.Clear();
        schema.Type = context.Type.Name;
        var sb = new StringBuilder();
        _ = sb.Append(schema.Description);

        foreach (var e in Enum.GetValues(context.Type).Cast<Enum>()) {
            var value = Convert.ToInt64(e, CultureInfo.InvariantCulture);
            schema.Enum.Add(new OpenApiLong(value));
            _ = sb.Append(wrap);
            _ = sb.Append( //
                CultureInfo.InvariantCulture, $"{Enum.GetName(context.Type, e)}({e.Desc()}) = {value}");
        }

        schema.Description = sb.ToString();
    }
}