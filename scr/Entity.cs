namespace WakfuBuider;

public class Entity
{
    public int HP { get; set; } = 0;
    public int AP { get; set; } = 0;
    public int MP { get; set; } = 0;
    public int WP { get; set; } = 0;

    public int WaterMastery { get; set; } = 0;
    public int WindMastery { get; set; } = 0;
    public int EarthMastery { get; set; } = 0;
    public int FireMastery { get; set; } = 0;
    public int WaterResistance { get; set; } = 0;
    public int WindResistance { get; set; } = 0;
    public int EarthResistance { get; set; } = 0;
    public int FireResistance { get; set; } = 0;

    public int DamageInflicted { get; set; } = 0;
    public int CriticalHits { get; set; } = 0;
    public int Initiative { get; set; } = 0;
    public int Dodge { get; set; } = 0;
    public int Wisdom { get; set; } = 0;
    public int Control { get; set; } = 0;
    public int HealsPerformed { get; set; } = 0;
    public int Block { get; set; } = 0;
    public int Range { get; set; } = 0;
    public int Lock { get; set; } = 0;
    public int Prospecting { get; set; } = 0;
    public int ForceOfWill { get; set; } = 0;

    public int CriticalMastery { get; set; } = 0;
    public int RearMastery { get; set; } = 0;
    public int MeleeMastery { get; set; } = 0;
    public int DistanceMastery { get; set; } = 0;
    public int HealingMastery { get; set; } = 0;
    public int BerserkMastery { get; set; } = 0;
    public int CriticalResistance { get; set; } = 0;
    public int RearResistance { get; set; } = 0;
    public int ArmorGiven { get; set; } = 0;
    public int ArmorReceived { get; set; } = 0;
    public int IndirectDamage { get; set; } = 0;

    public void ApplyItem(Item item)
    {
        if (Item.HasRandomEffect(item)) throw new Exception("Entity ApplyItem: item has random effects");
        foreach (var effect in item.Effects) ApplyItemEffect(item, effect);
    }

    public void ApplyBuild(Build build)
    {
        if (build.Amulet != null) ApplyItem(build.Amulet);
        if (build.RingOne != null) ApplyItem(build.RingOne);
        if (build.RingTwo != null) ApplyItem(build.RingTwo);
        if (build.Boot != null) ApplyItem(build.Boot);
        if (build.Cloak != null) ApplyItem(build.Cloak);
        if (build.Belt != null) ApplyItem(build.Belt);
        if (build.Helmet != null) ApplyItem(build.Helmet);
        if (build.Epaulette != null) ApplyItem(build.Epaulette);
        if (build.Breastplate != null) ApplyItem(build.Breastplate);
        if (build.Emblem != null) ApplyItem(build.Emblem);
        if (build.Pet != null) ApplyItem(build.Pet);
        if (build.MainHand != null) ApplyItem(build.MainHand);
        if (build.OffHand != null) ApplyItem(build.OffHand);
    }

    private void ApplyItemEffect(Item item, Effect effect)
    {
        if (Item.HasRandomEffect(item)) throw new Exception("Entity ApplyItem: item has random effects");

        switch (effect.Definition.Type)
        {
            // HP, AP, MP, WP
            case EffectType.HP:
                HP += (int)item.Value(effect);
                break;
            case EffectType.NegativeHP:
                HP -= (int)item.Value(effect);
                break;
            case EffectType.AP:
                AP += (int)item.Value(effect);
                break;
            case EffectType.NegativeMaxAp:
                AP -= (int)item.Value(effect);
                break;
            case EffectType.MP:
                MP += (int)item.Value(effect);
                break;
            case EffectType.NegativeMaxMP:
            case EffectType.NegativeMP:
                MP -= (int)item.Value(effect);
                break;
            case EffectType.WP:
                WP += (int)item.Value(effect);
                break;
            case EffectType.NegativeMaxWP:
                WP -= (int)item.Value(effect);
                break;

            // Elemental Mastery
            case EffectType.ElementalMastery:
                ApplyElementalMastery((int)item.Value(effect));
                break;
            case EffectType.NegativeElementalMastery:
                ApplyElementalMastery(-(int)item.Value(effect));
                break;
            case EffectType.FireMastery:
                ApplyElementalMastery(Element.Fire, (int)item.Value(effect));
                break;
            case EffectType.NegativeFireMastery:
                ApplyElementalMastery(Element.Fire, -(int)item.Value(effect));
                break;
            case EffectType.WaterMastery:
                ApplyElementalMastery(Element.Water, (int)item.Value(effect));
                break;
            case EffectType.EarthMastery:
                ApplyElementalMastery(Element.Earth, (int)item.Value(effect));
                break;
            case EffectType.AirMastery:
                ApplyElementalMastery(Element.Air, (int)item.Value(effect));
                break;

            // Elemental Resistance
            case EffectType.ElementalResistance:
                ApplyElementalResistance((int)item.Value(effect));
                break;
            case EffectType.NegativeElementalResistance:
            case EffectType.NegativeElementalResistance2:
                ApplyElementalResistance(-(int)item.Value(effect));
                break;
            case EffectType.FireResistance:
                ApplyElementalResistance(Element.Fire, (int)item.Value(effect));
                break;
            case EffectType.NegativeFireResistance:
                ApplyElementalResistance(Element.Fire, -(int)item.Value(effect));
                break;
            case EffectType.WaterResistance:
                ApplyElementalResistance(Element.Water, (int)item.Value(effect));
                break;
            case EffectType.NegativeWaterResistance:
                ApplyElementalResistance(Element.Water, -(int)item.Value(effect));
                break;
            case EffectType.EarthResistance:
                ApplyElementalResistance(Element.Earth, (int)item.Value(effect));
                break;
            case EffectType.NegativeEarthResistance:
                ApplyElementalResistance(Element.Earth, -(int)item.Value(effect));
                break;
            case EffectType.AirResistance:
                ApplyElementalResistance(Element.Air, (int)item.Value(effect));
                break;

            // Critical Hits
            case EffectType.CriticalHit:
                CriticalHits += (int)item.Value(effect);
                break;
            case EffectType.NegativeCriticalHit:
                CriticalHits -= (int)item.Value(effect);
                break;
            // Critical Mastery
            case EffectType.CriticalMastery:
                CriticalMastery += (int)item.Value(effect);
                break;
            case EffectType.NegativeCriticalMastery:
                CriticalMastery -= (int)item.Value(effect);
                break;

            // Rear Mastery
            case EffectType.RearMastery:
                RearMastery += (int)item.Value(effect);
                break;
            case EffectType.NegativeRearMastery:
                RearMastery -= (int)item.Value(effect);
                break;

            // Melee Mastery
            case EffectType.MeleeMastery:
                MeleeMastery += (int)item.Value(effect);
                break;
            case EffectType.NegativeMeleeMastery:
                MeleeMastery -= (int)item.Value(effect);
                break;

            // Distance Mastery
            case EffectType.DistanceMastery:
                DistanceMastery += (int)item.Value(effect);
                break;
            case EffectType.NegativeDistanceMastery:
                DistanceMastery -= (int)item.Value(effect);
                break;

            // Healing Mastery
            case EffectType.HealingMastery:
                HealingMastery += (int)item.Value(effect);
                break;

            // Berserk Mastery
            case EffectType.BerserkMastery:
                BerserkMastery += (int)item.Value(effect);
                break;
            case EffectType.NegativeBerserkMastery:
                BerserkMastery -= (int)item.Value(effect);
                break;

            // Resistance-related
            case EffectType.CriticalResistance:
                CriticalResistance += (int)item.Value(effect);
                break;
            case EffectType.NegativeCriticalResistance:
                CriticalResistance -= (int)item.Value(effect);
                break;
            case EffectType.RearResistance:
                RearResistance += (int)item.Value(effect);
                break;
            case EffectType.NegativeRearResistance:
                RearResistance -= (int)item.Value(effect);
                break;

            // Armor
            case EffectType.Armorgiven:
                ArmorGiven += (int)item.Value(effect);
                break;
            case EffectType.NegativeArmorreceived:
                ArmorReceived -= (int)item.Value(effect);
                break;

            // Initiative, Dodge, Wisdom, etc.
            case EffectType.Initiative:
                Initiative += (int)item.Value(effect);
                break;
            case EffectType.NegativeInitiative:
                Initiative -= (int)item.Value(effect);
                break;
            case EffectType.Dodge:
                Dodge += (int)item.Value(effect);
                break;
            case EffectType.NegativeDodge:
                Dodge -= (int)item.Value(effect);
                break;
            case EffectType.Wisdom:
                Wisdom += (int)item.Value(effect);
                break;
            case EffectType.Control:
                Control += (int)item.Value(effect);
                break;
            case EffectType.Block:
                Block += (int)item.Value(effect);
                break;
            case EffectType.NegativeBlock:
                Block -= (int)item.Value(effect);
                break;
            case EffectType.Range:
                Range += (int)item.Value(effect);
                break;
            case EffectType.NegativeRange:
                Range -= (int)item.Value(effect);
                break;
            case EffectType.Lock:
                Lock += (int)item.Value(effect);
                break;
            case EffectType.NegativeLock:
                Lock -= (int)item.Value(effect);
                break;
            case EffectType.Prospecting:
                Prospecting += (int)item.Value(effect);
                break;
            case EffectType.ForceofWill:
                ForceOfWill += (int)item.Value(effect);
                break;
            default:
                throw new Exception("Unhandled EffectType: " + effect.Definition.Type);
        }
    }

    public void ApplyElementalMastery(int value)
    {
        FireMastery += value;
        WaterMastery += value;
        WindMastery += value;
        EarthMastery += value;
    }

    public void ApplyElementalResistance(int value)
    {
        FireResistance += value;
        WaterResistance += value;
        WindResistance += value;
        EarthResistance += value;
    }

    public void ApplyElementalMastery(Element element, int value)
    {
        switch (element)
        {
            case Element.Fire:
                FireMastery += value;
                break;
            case Element.Water:
                WaterMastery += value;
                break;
            case Element.Air:
                WindMastery += value;
                break;
            case Element.Earth:
                EarthMastery += value;
                break;
            default:
                throw new Exception($"Entity ApplyElementalMastery, Element {element}: unhandled element");
        }
    }

    public void ApplyElementalResistance(Element element, int value)
    {
        switch (element)
        {
            case Element.Fire:
                FireResistance += value;
                break;
            case Element.Water:
                WaterResistance += value;
                break;
            case Element.Air:
                WindResistance += value;
                break;
            case Element.Earth:
                EarthResistance += value;
                break;
            default:
                throw new Exception($"Entity ApplyElementalResistance, Element {element}: unhandled element");
        }
    }

    public void ApplyElementalMastery(Element[] elements, int value)
    {
        foreach (var element in elements) ApplyElementalMastery(element, value);
    }

    public void ApplyElementalResistance(Element[] elements, int value)
    {
        foreach (var element in elements) ApplyElementalResistance(element, value);
    }

    public int GetElementalMastery(Element element)
    {
        return element switch
        {
            Element.Fire => FireMastery,
            Element.Water => WaterMastery,
            Element.Air => WindMastery,
            Element.Earth => EarthMastery,
            _ => throw new Exception($"Entity GetElementalMastery, Element {element}: unhandled element"),
        };
    }

    public void ApplyEchantment(Item item, Enchantment enchantment)
    {
        var value = (int)Enchantment.MaxValue(item, enchantment);

        if (Enchantment.MeleeMastery == enchantment) { MeleeMastery += value; return; }
        if (Enchantment.EarthResistance == enchantment) { EarthResistance += value; return; }
        if (Enchantment.DistanceMastery == enchantment) { DistanceMastery += value; return; }
        if (Enchantment.BerserkMastery == enchantment) { BerserkMastery += value; return; }
        if (Enchantment.Dodge == enchantment) { Dodge += value; return; }
        if (Enchantment.Initiative == enchantment) { Initiative += value; return; }
        if (Enchantment.FireResistance == enchantment) { FireResistance += value; return; }
        if (Enchantment.CriticalMastery == enchantment) { CriticalMastery += value; return; }
        if (Enchantment.RearMastery == enchantment) { RearMastery += value; return; }
        if (Enchantment.Life == enchantment) { HP += value; return; }
        if (Enchantment.WaterResistance == enchantment) { WaterResistance += value; return; }
        if (Enchantment.AirResistance == enchantment) { WindResistance += value; return; }
        if (Enchantment.HealingMastery == enchantment) { HealingMastery += value; return; }
        if (Enchantment.ElementalMastery == enchantment) { ApplyElementalMastery(value); return; }
        if (Enchantment.Lock == enchantment) { Lock += value; return; }
        throw new Exception($"Entity ApplyEchantment, Enchantment {enchantment}: unhandled enchantment");

    }

    public int GetElementalResistance(Element element)
    {
        return element switch
        {
            Element.Fire => FireResistance,
            Element.Water => WaterResistance,
            Element.Air => WindResistance,
            Element.Earth => EarthResistance,
            _ => throw new Exception($"Entity GetElementalResistance, Element {element}: unhandled element"),
        };
    }

    public float ResistPercent(Element element) => (float)Math.Floor((1 - Math.Pow(0.8f, (float)GetElementalResistance(element) / 100)) * 100) / 100;
}