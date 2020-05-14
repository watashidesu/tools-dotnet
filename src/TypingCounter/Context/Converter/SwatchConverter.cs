using MaterialDesignColors;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace TypingCounter.Context.Converter
{
    public class SwatchConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Swatch));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new SwatchesProvider().Swatches.SingleOrDefault(s => s.Name == (string)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var name = (value as Swatch).Name;
            writer.WriteValue(name);
        }
    }
}