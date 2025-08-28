namespace NetAdmin.Host.Utils;

/// <summary>
///     "" -> null 转换器
/// </summary>
public sealed class ToNullIfEmptyStringConverter : JsonConverter<string>
{
    /// <inheritdoc />
    public override string Read(
        ref Utf8JsonReader reader
        , Type typeToConvert
        , JsonSerializerOptions options
    ) {
        if (reader.TokenType == JsonTokenType.Number) {
            var success = reader.TryGetDecimal(out var dec);
            return success ? dec.ToString(CultureInfo.InvariantCulture) : "0";
        }

        var ret = reader.GetString();
        return ret?.Length == 0 ? null : ret;
    }

    /// <inheritdoc />
    public override void Write(
        Utf8JsonWriter writer
        , string value
        , JsonSerializerOptions options
    ) {
        writer.WriteStringValue(value.Length == 0 ? null : value);
    }
}