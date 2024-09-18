using WakfuBuider;

class Program
{
    static void Main(string[] args)
    {
        var sublimations = Parser.Sublimations(Util.ReadEmbeddedResource("WakfuBuilder.data.Sublimations.json"));
        var sublimationsStates = Parser.SublimationStates(Util.ReadEmbeddedResource("WakfuBuilder.data.SublimationsStates.json"));
        var items = Parser.Items(Util.ReadEmbeddedResource("WakfuBuilder.data.items.json"));

        TestItems(items);
    }

    public static void TestCalculator()
    {
        Entity actor = new();
        Entity target = new() { WaterResistance = 100 };
        EntityAction Spell = new() { Element = Element.Fire };

        int damage = Calculator.CalculateDamage(actor, target, Spell, new Positioning());
        Console.WriteLine($"damage {damage}");
    }

    public static void TestItems(List<Item> items)
    {
        // items = [.. items.Where(x => !Validation(x)).OrderBy(x => x.level)]; // these are the invalid itens
        items = [.. items.Where(item => Item.IsValid(item) && item.Effects.Count != 0).OrderBy(x => x.Level)];
        items = [.. items.Where(item => item.Level <= 35).OrderBy(x => x.Level)];
        items = [.. items.Where(item => item.Rarity == Rarity.Epique).OrderBy(x => x.Level)];

        var amulets = items.Where(item => item.Type == ItemType.Amulet).ToList();
        var rings = items.Where(item => item.Type == ItemType.Ring).ToList();
        var boots = items.Where(item => item.Type == ItemType.Boot).ToList();
        var belts = items.Where(item => item.Type == ItemType.Belt).ToList();
        var helmets = items.Where(item => item.Type == ItemType.Helmet).ToList();
        var epaulettes = items.Where(item => item.Type == ItemType.Epaulette).ToList();
        var breastplates = items.Where(item => item.Type == ItemType.Breastplate).ToList();
        var cloaks = items.Where(item => item.Type == ItemType.Cloak).ToList();
        var emblems = items.Where(item => item.Type == ItemType.Emblem).ToList();
        var pets = items.Where(item => item.Type == ItemType.Pet).ToList();
        var oneHands = items.Where(Item.IsOneHandWeapon).ToList();
        var offHands = items.Where(Item.IsOffHandWeapon).ToList();
        var twoHands = items.Where(Item.IsTwoHandWeapon).ToList();

        Item.PrintMany(cloaks, Localization.English);
    }
}
