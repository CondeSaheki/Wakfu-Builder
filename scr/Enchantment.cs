namespace WakfuBuider;

public class Enchantment
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Base { get; set; }
    public Func<Item, bool> Bonus { get; set; }
    public EnchatmentType Type { get; set; }

    private Enchantment(string name, string description, int bonusPerLevel, Func<Item, bool> bonusconditions)
    {
        Name = name;
        Description = description;
        Base = bonusPerLevel;
        Bonus = bonusconditions;
    }

    //red
    public static Enchantment MeleeMastery = new("Melee Mastery", "1 Melee Mastery per level", 1, (item) => item.Type == ItemType.Cloak || item.Type == ItemType.Helmet);
    public static Enchantment EarthResistance = new("Earth Resistance", "2 Earth Resistance per level", 2, (item) => item.Type == ItemType.Boots || item.Type == ItemType.Breastplate);
    public static Enchantment DistanceMastery = new("Distance Mastery", "1 Distance Mastery per level", 1, (item) => item.Type == ItemType.Belt || Item.IsMainHand(item));
    public static Enchantment BerserkMastery = new("Berserk Mastery", "1 Berserk Mastery per level", 1, (item) => item.Type == ItemType.Cloak || item.Type == ItemType.Amulet);
    // green
    public static Enchantment Dodge = new("Dodge", "3 Dodge per level", 3, (item) => item.Type == ItemType.Ring);
    public static Enchantment Initiative = new("Initiative", "2 Initiative per level", 2, (item) => item.Type == ItemType.Cloak || item.Type == ItemType.Amulet);
    public static Enchantment FireResistance = new("Fire Resistance", "2 Fire Resistance per level", 2, (item) => item.Type == ItemType.Belt || item.Type == ItemType.Breastplate);
    public static Enchantment CriticalMastery = new("Critical Mastery", "1 Critical Mastery per level", 1, (item) => item.Type == ItemType.Epaulettes || Item.IsMainHand(item));
    public static Enchantment RearMastery = new("Rear Mastery", "1 Rear Mastery per level", 1, (item) => item.Type == ItemType.Belt || item.Type == ItemType.Boots);

    //blue
    public static Enchantment Life = new("Life", "4 HP per level", 4, (item) => item.Type == ItemType.Helmet || Item.IsMainHand(item));
    public static Enchantment WaterResistance = new("Water Resistance", "2 Water Resistance per level", 2, (item) => item.Type == ItemType.Epaulettes || item.Type == ItemType.Breastplate);
    public static Enchantment AirResistance = new("Air Resistance", "2 Air Resistance per level", 2, (item) => item.Type == ItemType.Cloak || item.Type == ItemType.Breastplate);
    public static Enchantment HealingMastery = new("Healing Mastery", "1 Healing Mastery per level", 1, (item) => item.Type == ItemType.Epaulettes || item.Type == ItemType.Amulet);
    public static Enchantment ElementalMastery = new("Elemental Mastery", "1 Elemental Mastery per level", 1, (item) => item.Type == ItemType.Cloak || item.Type == ItemType.Breastplate);
    public static Enchantment Lock = new("Lock", "3 Lock per level", 3, (item) => item.Type == ItemType.Ring);

    public static Enchantment[] Red =
    [
        EarthResistance,
        MeleeMastery,
        DistanceMastery,
        BerserkMastery,
    ];

    public static Enchantment[] Green =
    [
        Dodge,
        Initiative,
        FireResistance,
        CriticalMastery,
        RearMastery,
    ];

    public static Enchantment[] Blue =
    [
        WaterResistance,
        AirResistance,
        ElementalMastery,
        Life,
        HealingMastery,
        Lock,
    ];

    public static Enchantment[] White =
    [
        BerserkMastery,
        DistanceMastery,
        EarthResistance,
        MeleeMastery,
        CriticalMastery,
        Dodge,
        FireResistance,
        Initiative,
        RearMastery,
        AirResistance,
        ElementalMastery,
        HealingMastery,
        Life,
        Lock,
        WaterResistance,
    ];

    public static uint Value(Item item, Enchantment enchantment, uint level)
    {
        var value = enchantment.Base * level;
        return (uint)(enchantment.Bonus.Invoke(item) ? value * 2 : value);
    }

    public static uint MaxValue(Item item, Enchantment enchantment) => Value(item, enchantment, MaxLevel(item));

    public static uint MaxLevel(Item item)
    {
        if (item.Level >= 1 && item.Level <= 35) return 1;
        if (item.Level >= 36 && item.Level <= 50) return 2;
        if (item.Level >= 51 && item.Level <= 65) return 3;
        if (item.Level >= 66 && item.Level <= 80) return 4;
        if (item.Level >= 81 && item.Level <= 95) return 5;
        if (item.Level >= 96 && item.Level <= 125) return 6;
        if (item.Level >= 126 && item.Level <= 140) return 7;
        if (item.Level >= 141 && item.Level <= 170) return 8;
        if (item.Level >= 171 && item.Level <= 185) return 9;
        if (item.Level >= 186 && item.Level <= 230) return 10;
        throw new Exception($"Enchantment MaxLevel, Item Level {item.Level}: Unhandled item level interval");
    }

    public static Enchantment[] GetEnchantments(EnchatmentType type)
    {
        return type switch
        {
            EnchatmentType.Red => Enchantment.Red,
            EnchatmentType.Green => Enchantment.Green,
            EnchatmentType.Blue => Enchantment.Blue,
            EnchatmentType.White => Enchantment.White,
            _ => throw new Exception($"Enchantment GetEnchantments, {type}: unhandled enchatment"),
        };
    }

}