using Newtonsoft.Json;

namespace WakfuBuider;

public class LocalizedString
{
    [JsonProperty("fr")]
    public string French { get; set; } = string.Empty;
    [JsonProperty("en")]
    public string English { get; set; } = string.Empty;
    [JsonProperty("es")]
    public string Espanish { get; set; } = string.Empty;
    [JsonProperty("pt")]
    public string Portuguese { get; set; } = string.Empty;

    public string Local(Localization localization)
    {
        return localization switch 
        {
            Localization.French => French,
            Localization.English => English,
            Localization.Espanish => Espanish,
            Localization.Portuguese => Portuguese,
            _ => throw new Exception($"LocalizedString Local, {localization}: Invalid Localization")
        };
    }
}