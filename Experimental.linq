<Query Kind="Program">
  <Namespace>System.Text.Json</Namespace>
  <Namespace>System.Text.Json.Serialization.Metadata</Namespace>
  <Namespace>System.Text.Json.Serialization</Namespace>
</Query>

public record Person(int Age = 1, string Name = "Taoke")
{
    public List<int> Numbers { get; set; }
    public string[] Names { get; set; }
    public IReadOnlyCollection<string> ApiIds { get; init; }
}
delegate object ReadDelegate(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options);


public class xx : JsonConverter<object>
{

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var reader2 = reader;
        if (reader2.TokenType != JsonTokenType.StartArray) throw new Exception();
        reader2.Read();
        if (reader2.TokenType == JsonTokenType.EndArray)
        {
            reader.Read();
            return null;
        }

        var converter = options.GetConverter(typeToConvert);
        var instance = Expression.Constant(converter);
        var method = converter.GetType().GetMethod("Read", BindingFlags.Public | BindingFlags.Instance);
        var parameters = method.GetParameters().Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();
        var call = Expression.Call(instance, method, parameters);
        var cast = Expression.TypeAs(call, typeof(object));

        var @delegate = Expression.Lambda<ReadDelegate>(cast, parameters);

        var result = @delegate.Compile()(ref reader, typeToConvert, options);

        return result;


    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteEndArray();

    }
}

void Main()
{

    static void DefaultValueModifier(JsonTypeInfo typeInfo)
    {
        foreach (var property in typeInfo.Properties)
        {
            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                property.ShouldSerialize = (_, val) => val is ICollection { Count: > 0 };
                property.Set = (x, y) =>
                {
                    x.GetType().GetProperty(property.Name).SetValue(x,
                    y is ICollection s ? s.Count > 0 ? y : null : null
                    );

                };
            }
        }
    }


    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,

        TypeInfoResolver
                      = new DefaultJsonTypeInfoResolver { Modifiers = { DefaultValueModifier } },

        WriteIndented = true,

    };
    options.Converters.Add(new ToNullIfEmptyStringConverter());

    JsonSerializer.Deserialize<Person>("""{"age":0, "name":"","numbers":[1,2,3],"names":[""]}""", options).Dump();

    JsonSerializer.Serialize<Person>(new Person()
    {
        Name = "",
        Numbers = new List<int> { 1, 2 }

    ,
        ApiIds = new List<string> { }
    }, options).Dump();

}






/// <summary>
///     "" -> null 转换器
/// </summary>
public class ToNullIfEmptyStringConverter : JsonConverter<string>
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
        writer.WriteStringValue(value.Length == 0 ? null : value);
    }
}