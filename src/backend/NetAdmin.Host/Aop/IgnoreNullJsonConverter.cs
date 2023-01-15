using System.Text.Json;
using System.Text.Json.Serialization;
using Furion.DependencyInjection;

namespace NetAdmin.Host.Aop;

/// <summary>
///     忽略空值JsonConvertor
/// </summary>
[SuppressSniffer]
public class IgnoreNullJsonConverter : JsonConverter<object>
{
    /// <inheritdoc />
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new object();
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options) { }
}