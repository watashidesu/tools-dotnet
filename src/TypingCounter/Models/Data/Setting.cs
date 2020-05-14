using MaterialDesignColors;
using Newtonsoft.Json;
using TypingCounter.Context.Converter;

namespace TypingCounter.Models.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Setting
    {
        [JsonProperty("theme.nightMode")]
        public bool IsNightMode { get; set; }

        [JsonProperty("theme.color.primary")]
        [JsonConverter(typeof(SwatchConverter))]
        public Swatch ColorPrimary { get; set; }

        [JsonProperty("theme.color.accent")]
        [JsonConverter(typeof(SwatchConverter))]
        public Swatch ColorAccent { get; set; }
    }
}