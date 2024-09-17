using System.Text;
using WakfuBuider;

class Program
{
    static void Main(string[] args)
    {
        string jsonFilePathSublimation = """C:\Users\xilinux is dead\Desktop\Programming\WakfuBuilder\data\zenithSublimations.json""";
        var sublimations = Parser.Sublimations(jsonFilePathSublimation);

        // StringBuilder sb = new();
        // foreach (var sublimation in sublimations)
        // {
        //     sb.AppendLine(sublimation.ToString(Localization.Portuguese));
        //     sb.AppendLine();
        // }
        // sb.AppendLine();
        // sb.AppendLine($"Count: {sublimations.Count}");
        // Console.WriteLine(sb.ToString());


        string jsonFilePathSublimationEffects = """C:\Users\xilinux is dead\Desktop\Programming\WakfuBuilder\data\zenithSublimationsStates.json""";
        var sublimationsEffects = Parser.SublimationEffects(jsonFilePathSublimationEffects);

        // StringBuilder sb = new();
        // foreach (var effects in sublimationsEffects)
        // {
        //     sb.AppendLine($"> {effects.Key}");
        //     foreach (var effect in effects.Value) sb.AppendLine($"{effect.Local(Localization.Portuguese)}");
        //     sb.AppendLine();
        // }
        // sb.AppendLine();
        // sb.AppendLine($"Count: {sublimationsEffects.Count}");
        // Console.WriteLine(sb.ToString());


        string jsonFilePath = """C:\Users\xilinux is dead\Desktop\Programming\WakfuBuilder\data\items.json""";
        var items = Parser.Items(jsonFilePath);

        // // items = [.. items.Where(x => !Validation(x)).OrderBy(x => x.level)]; // these are the invalid itens
        items = [.. items.Where(Item.IsValid).OrderBy(x => x.Level)];
        // items = [.. items.Where(item => item.Level <= 35).OrderBy(x => x.Level)];
        // items = [.. items.Where(item => item.Rarity == Rarity.Legendary).OrderBy(x => x.Level)];

        var amulets = items.Where(item => item.Type == ItemType.Amulet).ToList();
        var rings = items.Where(item => item.Type == ItemType.Ring).ToList();
        var boots = items.Where(item => item.Type == ItemType.Boots).ToList();
        var belts = items.Where(item => item.Type == ItemType.Belt).ToList();
        var helmets = items.Where(item => item.Type == ItemType.Helmet).ToList();
        var epaulettes = items.Where(item => item.Type == ItemType.Epaulettes).ToList();
        var breastplates = items.Where(item => item.Type == ItemType.Breastplate).ToList();
        var cloaks = items.Where(item => item.Type == ItemType.Cloak).ToList();
        var emblems = items.Where(item => item.Type == ItemType.Emblem).ToList();
        var pets = items.Where(item => item.Type == ItemType.Pet).ToList();
        var oneHands = items.Where(Item.IsOneHandWeapon).ToList();
        var offHands = items.Where(Item.IsOffHandWeapon).ToList();
        var twoHands = items.Where(Item.IsTwoHandWeapon).ToList();

        var amuletsCount = Counter(amulets);
        var ringsCount = Counter(rings);
        var bootsCount = Counter(boots);
        var beltsCount = Counter(belts);
        var helmetsCount = Counter(helmets);
        var epaulettesCount = Counter(epaulettes);
        var breastplatesCount = Counter(breastplates);
        var cloaksCount = Counter(cloaks);
        var emblemsCount = Counter(emblems);
        var petsCount = Counter(pets);
        var oneHandsCount = Counter(oneHands);
        var offHandsCount = Counter(offHands);
        var twoHandsCount = Counter(twoHands);

        static int Counter(List<Item> items)
        {
            var count = 0;
            foreach (var item in items)
            {
                if (Item.HasRandomEffect(item) == false) { ++count; continue; }
                foreach (var effect in item.Effects)
                {
                    if (!Effect.IsRandom(effect)) continue;
                    var amount = Item.GetRandomAmount(effect);
                    if (amount == 1) count += 4;
                    if (amount == 2) count += 4 * 3;
                    if (amount == 3) count += 4 * 3 * 2;
                }
            }
            return count;
        }


        Console.WriteLine($"amulets: {amuletsCount}");
        Console.WriteLine($"rings: {ringsCount}");
        Console.WriteLine($"boots: {bootsCount}");
        Console.WriteLine($"belts: {beltsCount}");
        Console.WriteLine($"helmets: {helmetsCount}");
        Console.WriteLine($"epaulettes: {epaulettesCount}");
        Console.WriteLine($"breastplates: {breastplatesCount}");
        Console.WriteLine($"cloaks: {cloaksCount}");
        Console.WriteLine($"emblems: {emblemsCount}");
        Console.WriteLine($"pets: {petsCount}");
        Console.WriteLine($"oneHands: {oneHandsCount}");
        Console.WriteLine($"offHands: {offHandsCount}");
        Console.WriteLine($"twoHands: {twoHandsCount}");

        double totalcombinations = 1;
        totalcombinations *= amuletsCount == 0 ? 1 : amuletsCount;
        totalcombinations *= ringsCount == 0 ? 1 : ringsCount;
        totalcombinations *= bootsCount == 0 ? 1 : bootsCount;
        totalcombinations *= beltsCount == 0 ? 1 : beltsCount;
        totalcombinations *= helmetsCount == 0 ? 1 : helmetsCount;
        totalcombinations *= epaulettesCount == 0 ? 1 : epaulettesCount;
        totalcombinations *= breastplatesCount == 0 ? 1 : breastplatesCount;
        totalcombinations *= cloaksCount == 0 ? 1 : cloaksCount;
        //totalcombinations *= emblemsCount == 0 ? 1 : emblemsCount;
        //totalcombinations *= petsCount == 0 ? 1 : petsCount;
        //totalcombinations *= oneHandsCount == 0 ? 1 : oneHandsCount;
        //totalcombinations *= offHandsCount == 0 ? 1 : offHandsCount;
        totalcombinations *= twoHandsCount == 0 ? 1 : twoHandsCount;
        Console.WriteLine($"combinations: {totalcombinations}");
        //var a = TimeSpan.(totalcombinations);
        Console.WriteLine($"{totalcombinations / 86400000000000000 / 8} days;");
        
        var groupByEffects = items.GroupBy(i => i.Effects.Count).Select(g => (g.Key, Count: g.Count()));
        foreach (var (key, count) in groupByEffects)
        {
            Console.WriteLine($"{key} effects: {count}");
        }
        

        // Item.PrintMany(items, Localization.Portuguese);
        // Console.WriteLine($"generating builds...");

        // // all possible builds
        // var builds = GenerateBuilds(items, null, []);

        // Console.WriteLine($"builds count {builds.Count}");

        // Entity actor = new();
        // Entity target = new() { WaterResistance = 100 };
        // EntityAction Spell = new() { Element = Element.Fire };

        // int damage = Calculator.CalculateDamage(actor, target, Spell, new Positioning());
        // Console.WriteLine($"damage {damage}");
    }

}
