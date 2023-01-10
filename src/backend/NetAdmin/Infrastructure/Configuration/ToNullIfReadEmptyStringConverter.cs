using System.Text.Json;
using System.Text.Json.Serialization;

namespace NetAdmin.Infrastructure.Configuration;

/// <summary>
///     String.Empty -> Null 转换器
/// </summary>
public class ToNullIfReadEmptyStringConverter : JsonConverter<string>
{
    /// <inheritdoc />
    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var ret = reader.GetString();
        return ret?.Length == 0 ? null : ret;
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}