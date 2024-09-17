using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WakfuBuider;

public static class Parser
{
    public static List<Item> Items(string path)
    {
        string jsonData = File.ReadAllText(path);
        List<Item> items = [];
        try
        {
            var json = JsonConvert.DeserializeObject<List<Item>>(jsonData, new JsonSerializerSettings
            {
                Error = (sender, args) =>
                {
                    Console.WriteLine($"Error deserializing item: {args.ErrorContext.Error.Message}");
                    args.ErrorContext.Handled = true;
                }
            });
            if (json != null) items = json;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ParseItems: {ex}");
        }
        return items;
    }

    public static List<Sublimation> Sublimations(string filePath)
    {
        var jsonContent = File.ReadAllText(filePath);
        var jsonList = JsonConvert.DeserializeObject<List<dynamic>>(jsonContent);
        if (jsonList == null) return [];

        List<Sublimation> sublimations = [];
        try
        {
            foreach (var json in jsonList)
            {
                var name = new LocalizedString();
                foreach (var translation in json.translations)
                {
                    switch ((string)translation.locale)
                    {
                        case "fr":
                            name.French = (string)translation.value;
                            break;
                        case "en":
                            name.English = (string)translation.value;
                            break;
                        case "es":
                            name.Espanish = (string)translation.value;
                            break;
                        case "pt":
                            name.Portuguese = (string)translation.value;
                            break;
                    }
                }

                var description = new LocalizedString
                {
                    French = (string)json.parsedEffects.fr,
                    English = (string)json.parsedEffects.en,
                    Espanish = (string)json.parsedEffects.es,
                    Portuguese = (string)json.parsedEffects.pt
                };

                var colorsNeeded = (IEnumerable<dynamic>)json.colors_needed;
                EnchatmentType[] combination = colorsNeeded != null
                    ? colorsNeeded
                        .Select(color => (int)color.id_color switch
                        {
                            1 => EnchatmentType.Red,
                            2 => EnchatmentType.Green,
                            3 => EnchatmentType.Blue,
                            4 => EnchatmentType.White,
                            _ => throw new Exception("Invalid color ID")
                        })
                        .ToArray()
                    : []; // If colors_needed is null, return an empty array

                int coincidents = (int)json.max_usage;

                var type = SublimationType.Normal;
                if ((int)json.is_epic == 1)
                {
                    type = SublimationType.Epique;
                }
                else if ((int)json.is_relic == 1)
                {
                    type = SublimationType.Relic;
                }

                int id = (int)json.id_shard;
                int? level = json.level != null ? (int?)json.level : null;
                int? parent = json.parent_id != null ? (int?)json.parent_id : null;

                var state = new SublimationState();
                if (json.inner_states != null && ((IEnumerable<dynamic>)json.inner_states).Any())
                {
                    var innerState = json.inner_states[0]; // Assuming only one inner_state based on your description

                    state.Id = (int)innerState.id_state;

                    // Map name_state to LocalizedString
                    state.Name = new LocalizedString();
                    foreach (var translation in innerState.translations)
                    {
                        switch ((string)translation.locale)
                        {
                            case "fr":
                                state.Name.French = (string)translation.value;
                                break;
                            case "en":
                                state.Name.English = (string)translation.value;
                                break;
                            case "es":
                                state.Name.Espanish = (string)translation.value;
                                break;
                            case "pt":
                                state.Name.Portuguese = (string)translation.value;
                                break;
                        }
                    }
                }

                sublimations.Add(new Sublimation
                {
                    Id = id,
                    Level = level,
                    Parent = parent,
                    Name = name,
                    Description = description,
                    Combination = combination,
                    Coincidents = coincidents,
                    Type = type,
                    State = state
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ParseSublimations: {ex}");
        }
        return sublimations;
    }

    public static Dictionary<int, List<LocalizedString>> SublimationEffects(string path)
    {
        string jsonData = File.ReadAllText(path);
        var effectsDict = new Dictionary<int, List<LocalizedString>>();

        var jsonArray = JArray.Parse(jsonData);

        foreach (var item in jsonArray)
        {
            var effects = item["effects"];
            if (effects == null) continue;
            
            List<LocalizedString> effectsList = [];
            foreach (var effect in effects)
            {
                var translations = effect?["translations"];

                var localizedString = new LocalizedString
                {
                    French = translations?.FirstOrDefault(t => (string?)t["locale"] == "fr")?["value"]?.ToString() ?? effect?["name_effect"]?.ToString() ?? string.Empty,
                    English = translations?.FirstOrDefault(t => (string?)t["locale"] == "en")?["value"]?.ToString() ?? string.Empty,
                    Espanish = translations?.FirstOrDefault(t => (string?)t["locale"] == "es")?["value"]?.ToString() ?? string.Empty,
                    Portuguese = translations?.FirstOrDefault(t => (string?)t["locale"] == "pt")?["value"]?.ToString() ?? string.Empty
                };
                effectsList.Add(localizedString);
            }

            var effectId = (int?)item?["id_state"] ?? 0;
            effectsDict[effectId] = effectsList;
        }
        return effectsDict;
    }
}