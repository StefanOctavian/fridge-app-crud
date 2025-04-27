using System.Text.Json;
using System.Text.Json.Serialization;

using Crud.Entities.Enums;

namespace Crud.Converters;

public class UnitJsonConverter : JsonConverter<Unit>
{
    public override Unit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (string.IsNullOrEmpty(value))
        {
            throw new JsonException("Unit value cannot be null or empty.");
        }
        return UnitExtensions.FromLabel(value)
            ?? throw new JsonException($"Invalid unit value: {value}");
    }

    public override void Write(Utf8JsonWriter writer, Unit value, JsonSerializerOptions options)
    {
        var label = value.ToLabel();
        writer.WriteStringValue(label);
    }
}