using System.Text;
using Newtonsoft.Json;

namespace WakfuBuider;

public class Item
{
    [JsonProperty("id")]
    public int ID { get; set; } = 0;
    [JsonProperty("level")]
    public int Level { get; set; } = 0;
    [JsonProperty("rarity")]
    public Rarity Rarity { get; set; } = 0;
    [JsonProperty("itemTypeId")]
    public ItemType Type { get; set; } = 0;
    [JsonProperty("title")]
    public LocalizedString Name { get; set; } = new();
    [JsonProperty("description")]
    public LocalizedString Description { get; set; } = new();
    [JsonProperty("equipEffects")]
    public List<Effect> Effects { get; set; } = [];
    // [JsonProperty("useEffects")]
    // public List<UseEffects> useEffects { get; set; } = [];
    // [JsonProperty("useParameters")]
    // public UseParameters useParameters { get; set; } = new();
    // [JsonProperty("imageId")]
    // public int imageId { get; set; } = 0;
    // [JsonProperty("itemSetId")]
    // public int itemSetId { get; set; } = 0;

    public float Value(Effect effect)
    {
        if (effect.Definition.RawValue.Count < 1) throw new Exception($"Item Value, {effect.Description}: Bad Raw Value size.");

        if (effect.Definition.RawValue[1] == 0)
        {
            return effect.Definition.RawValue[0];
        }
        return effect.Definition.RawValue[0] + (effect.Definition.RawValue[1] * Level);
    }
    public static int GetRandomAmount(Effect effect)
    {
        if (effect.Definition.RawValue.Count < 2) throw new Exception($"Item Value, {effect.Description}: Bad Raw Value size.");
        return (int)effect.Definition.RawValue[2];
    }

    public static bool IsWeapon(Item item) =>
        IsOffHandWeapon(item) ||
        IsTwoHandWeapon(item) ||
        IsOneHandWeapon(item);

    public static bool IsMainHand(Item item) =>
        IsTwoHandWeapon(item) ||
        IsOneHandWeapon(item);

    public static bool IsOffHandWeapon(Item item) =>
        item.Type == ItemType.Dagger ||
        item.Type == ItemType.Shield;

    public static bool IsTwoHandWeapon(Item item) =>
        item.Type == ItemType.Hammer ||
        item.Type == ItemType.StaffTwoHand ||
        item.Type == ItemType.SwordTwoHand ||
        item.Type == ItemType.Axe ||
        item.Type == ItemType.Bow ||
        item.Type == ItemType.Shovel;

    public static bool IsOneHandWeapon(Item item) =>
        item.Type == ItemType.Sword ||
        item.Type == ItemType.Wand ||
        item.Type == ItemType.Cards ||
        item.Type == ItemType.Staff ||
        item.Type == ItemType.Nail;

    public static bool IsBroken(Item item) =>
        item.Type == ItemType.Lantern ||
        item.Type == ItemType.Tool ||
        item.Type == ItemType.Sublimation ||
        item.Type == ItemType.Costume ||
        item.Type == ItemType.Moderator;

    public static bool HasRandomEffect(Item item)
    {
        foreach (var effect in item.Effects) if (Effect.IsRandom(effect)) return true;
        return false;
    }

    public string ToString(Localization localization = Localization.English)
    {
        StringBuilder sb = new();
        sb.AppendLine($"> {Level} {Rarity} {Type} {Name.Local(localization)}");
        foreach (var effect in Effects)
        {
            if (!Effect.IsRandom(effect)) sb.AppendLine($"{effect.Description.Local(localization)} | {Value(effect)} {effect.Definition.Type}");
            else sb.AppendLine($"{effect.Description.Local(localization)} | {Value(effect)} * {GetRandomAmount(effect)} {effect.Definition.Type}");
        }
        return sb.ToString();
    }

    public static void PrintMany(IEnumerable<Item> items, Localization localization = Localization.English)
    {
        int counter = 0;
        StringBuilder sb = new();
        foreach (var item in items)
        {
            sb.AppendLine(item.ToString(localization));
            ++counter;
        }
        sb.AppendLine();
        sb.AppendLine($"> {counter} item(s)");
        Console.WriteLine(sb.ToString());
    }

    public static bool IsValid(Item item)
    {
        if (item.Rarity == Rarity.Unusual) return false;
        if (IsBroken(item)) return false;
        foreach (var effect in item.Effects) if (Effect.IsBroken(effect)) return false;
        return true;
    }
}

/*

// ignored

public class UseParameters
{
    public int useCostAp { get; set; } = 0;
    public int useCostMp { get; set; } = 0;
    public int useCostWp { get; set; } = 0;
}

public class UseEffects
{
    public UseEffectsDefinition definition { get; set; } = new();
    public LocalizedString description { get; set; } = new();
}

public class UseEffectsDefinition
{
    public int id { get; set; } = 0;
    public int actionId { get; set; } = 0;
    public List<float> @params { get; set; } = [];
    public int areaShape { get; set; } = 1;
    public List<float> areaSize { get; set; } = [];
}

*/