using Newtonsoft.Json;

namespace WakfuBuider;

public class EffectDefinition
{
    // [JsonProperty("id")]
    // public int ID { get; set; } = 0;
    [JsonProperty("actionId")]
    public EffectType Type { get; set; } = 0;
    [JsonProperty("params")]
    public List<float> RawValue { get; set; } = [];
}