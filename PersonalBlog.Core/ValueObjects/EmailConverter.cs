using System.Text.Json;
using System.Text.Json.Serialization;

namespace PersonalBlog.Core.ValueObjects;

public class EmailConverter : JsonConverter<Email>
{
    public override Email? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        if (str == null) return null;
        return new Email(str);
    }

    public override void Write(Utf8JsonWriter writer, Email value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}