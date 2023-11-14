using System.Text.Json;
using System.Text.Json.Serialization;

namespace Utils.Converters
{
    public class IgnoreEmptyStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            if (!string.IsNullOrEmpty(value))
            {
                writer.WriteStringValue(value);
            }
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(string);
        }
    }
}
