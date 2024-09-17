using Newtonsoft.Json;

namespace WakfuBuider;

public class Effect
{
    [JsonProperty("definition")]
    public EffectDefinition Definition { get; set; } = new();
    [JsonProperty("description")]
    public LocalizedString Description { get; set; } = new();

    public static bool IsBroken(Effect effect) =>
        effect.Definition.Type == EffectType.ReflectsXofdamage ||
        effect.Definition.Type == EffectType.XLvltoXspells ||
        effect.Definition.Type == EffectType.XLvltoelementalspells ||
        effect.Definition.Type == EffectType.HarvestingQuantity ||
        effect.Definition.Type == EffectType.Unknown ||
        effect.Definition.Type == EffectType.MovementSpeed;

    public static bool IsPrimary(Effect effect) =>
        effect.Definition.Type == EffectType.HP ||
        effect.Definition.Type == EffectType.AP ||
        effect.Definition.Type == EffectType.MP ||
        effect.Definition.Type == EffectType.WP;

    public static bool IsElemental(Effect effect) =>
        effect.Definition.Type == EffectType.ElementalMastery ||
        effect.Definition.Type == EffectType.ElementalResistance ||
        effect.Definition.Type == EffectType.FireMastery ||
        effect.Definition.Type == EffectType.WaterMastery ||
        effect.Definition.Type == EffectType.EarthMastery ||
        effect.Definition.Type == EffectType.AirMastery ||
        effect.Definition.Type == EffectType.FireResistance ||
        effect.Definition.Type == EffectType.WaterResistance ||
        effect.Definition.Type == EffectType.EarthResistance ||
        effect.Definition.Type == EffectType.AirResistance ||
        effect.Definition.Type == EffectType.MasteryofXrandomelement ||
        effect.Definition.Type == EffectType.ResistancetoXRandomElements;

    public static bool IsSecundary(Effect effect) =>
        effect.Definition.Type == EffectType.CriticalHit ||
        effect.Definition.Type == EffectType.Initiative ||
        effect.Definition.Type == EffectType.Dodge ||
        effect.Definition.Type == EffectType.Wisdom ||
        effect.Definition.Type == EffectType.Control ||
        effect.Definition.Type == EffectType.Block ||
        effect.Definition.Type == EffectType.Range ||
        effect.Definition.Type == EffectType.Lock ||
        effect.Definition.Type == EffectType.Prospecting ||
        effect.Definition.Type == EffectType.ForceofWill;

    public static bool IsMastery(Effect effect) =>
        effect.Definition.Type == EffectType.CriticalMastery ||
        effect.Definition.Type == EffectType.RearMastery ||
        effect.Definition.Type == EffectType.MeleeMastery ||
        effect.Definition.Type == EffectType.DistanceMastery ||
        effect.Definition.Type == EffectType.HealingMastery ||
        effect.Definition.Type == EffectType.BerserkMastery ||
        effect.Definition.Type == EffectType.CriticalResistance ||
        effect.Definition.Type == EffectType.RearResistance ||
        effect.Definition.Type == EffectType.Armorgiven;

    public static bool IsRandom(Effect effect) => 
        effect.Definition.Type == EffectType.MasteryofXrandomelement ||
        effect.Definition.Type == EffectType.ResistancetoXRandomElements;

    public static bool IsWeird(Effect effect) =>
        !IsPrimary(effect) ||
        !IsElemental(effect) ||
        !IsSecundary(effect) ||
        !IsMastery(effect);
}
