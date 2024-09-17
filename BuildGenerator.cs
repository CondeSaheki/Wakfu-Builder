namespace WakfuBuider;

public class BuildGenerator
{
    public SortedSet<Build> GenerateBuilds(
        List<Item> items,
        List<Sublimation> sublimations,
        List<SublimationState> sublimationStates,
        Func<Build, double> scoringFunc,
        int topN = 1000,
        Func<Item, bool>? earlyFilter = null,
        Func<Build, bool>? lateFilter = null,
        Build[]? requirements = null,
        bool includeEnchantments = false,
        bool includeSublimations = false)
    {
        var result = new SortedSet<Build>();

        var filteredItems = earlyFilter != null ? items.Where(earlyFilter).ToList() : items;

        var amulets = filteredItems.Where(item => item.Type == ItemType.Amulet).ToList();
        var rings = filteredItems.Where(item => item.Type == ItemType.Ring).ToList();
        var boots = filteredItems.Where(item => item.Type == ItemType.Boots).ToList();
        var belts = filteredItems.Where(item => item.Type == ItemType.Belt).ToList();
        var helmets = filteredItems.Where(item => item.Type == ItemType.Helmet).ToList();
        var epaulettes = filteredItems.Where(item => item.Type == ItemType.Epaulettes).ToList();
        var breastplates = filteredItems.Where(item => item.Type == ItemType.Breastplate).ToList();
        var cloaks = filteredItems.Where(item => item.Type == ItemType.Cloak).ToList();
        var emblems = filteredItems.Where(item => item.Type == ItemType.Emblem).ToList();
        var pets = filteredItems.Where(item => item.Type == ItemType.Pet).ToList();
        var oneHands = filteredItems.Where(Item.IsOneHandWeapon).ToList();
        var offHands = filteredItems.Where(Item.IsOffHandWeapon).ToList();
        var twoHands = filteredItems.Where(Item.IsTwoHandWeapon).ToList();
        
        foreach (var amulet in amulets)
        foreach (var ringOne in rings)
        foreach (var ringTwo in rings) 
        foreach (var boot in boots)
        foreach (var belt in belts)
        foreach (var helmet in helmets)
        foreach (var epaulette in epaulettes)
        foreach (var breastplate in breastplates)
        foreach (var cloak in cloaks)
        foreach (var emblem in emblems)
        foreach (var pet in pets)
        foreach (var mainHand in oneHands)
        foreach (var offHand in offHands)
        {
            var amuletElements = GenerateRandomElements(amulet);
            var ringOneElements = GenerateRandomElements(ringOne);
            var ringTwoElements = GenerateRandomElements(ringTwo);
            var bootElements = GenerateRandomElements(boot);
            var beltElements = GenerateRandomElements(belt);
            var helmetElements = GenerateRandomElements(helmet);
            var epauletteElements = GenerateRandomElements(epaulette);
            var breastplateElements = GenerateRandomElements(breastplate);
            var cloakElements = GenerateRandomElements(cloak);
            var emblemElements = GenerateRandomElements(emblem);
            var petElements = GenerateRandomElements(pet);
            var mainHandElements = GenerateRandomElements(mainHand);
            var offHandElements = GenerateRandomElements(offHand);


            if (includeEnchantments)
            {
                var enchantedAmulets = GenerateEnchantedItems(amulet);
                var enchantedRingOnes = GenerateEnchantedItems(ringOne);
                var enchantedRingTwos = GenerateEnchantedItems(ringTwo);
                var enchantedBootss = GenerateEnchantedItems(boot);
                var enchantedBelts = GenerateEnchantedItems(belt);
                var enchantedHelmets = GenerateEnchantedItems(helmet);
                var enchantedEpaulettess = GenerateEnchantedItems(epaulette);
                var enchantedBreastplates = GenerateEnchantedItems(breastplate);
                var enchantedCapes = GenerateEnchantedItems(cloak);
                var enchantedMainHands = GenerateEnchantedItems(mainHand);
                var enchantedOffHands = GenerateEnchantedItems(offHand);

                foreach (var enchantedAmulet in enchantedAmulets)
                foreach (var enchantedRingOne in enchantedRingOnes)
                foreach (var enchantedRingTwo in enchantedRingTwos) 
                foreach (var enchantedBoots in enchantedBootss)
                foreach (var enchantedBelt in enchantedBelts)
                foreach (var enchantedHelmet in enchantedHelmets)
                foreach (var enchantedEpaulettes in enchantedEpaulettess)
                foreach (var enchantedBreastplate in enchantedBreastplates)
                foreach (var enchantedCape in enchantedCapes)
                foreach (var enchantedMainHand in enchantedMainHands)
                foreach (var enchantedOffHand in enchantedOffHands)
                {
                    var build = new Build
                    {
                        Amulet = enchantedAmulet,
                        RingOne = enchantedRingOne,
                        RingTwo = enchantedRingTwo,
                        Boots = enchantedBoots,
                        Belt = enchantedBelt,
                        Helmet = enchantedHelmet,
                        Epaulettes = enchantedEpaulettes,
                        Breastplate = enchantedBreastplate,
                        Cape = enchantedCape,
                        Emblem = (ItemIndentified)emblem,
                        Pet = (ItemIndentified)pet,
                        MainHand = enchantedMainHand,
                        OffHand = enchantedOffHand,
                    };    

                    if (lateFilter?.Invoke(build) == false) continue;
                    
                    var Score = scoringFunc(build);
                }
            }
            else
            {
                var build = new Build
                {
                    Amulet = (ItemIndentified)amulet,
                    RingOne = (ItemIndentified)ringOne,
                    RingTwo = (ItemIndentified)ringTwo,
                    Boots = (ItemIndentified)boot,
                    Belt = (ItemIndentified)belt,
                    Helmet = (ItemIndentified)helmet,
                    Epaulettes = (ItemIndentified)epaulette,
                    Breastplate = (ItemIndentified)breastplate,
                    Cape = (ItemIndentified)cloak,
                    Emblem = (ItemIndentified)emblem,
                    Pet = (ItemIndentified)pet,
                    MainHand = (ItemIndentified)mainHand,
                    OffHand = (ItemIndentified)offHand,
                };  

                if (lateFilter?.Invoke(build) == false) continue;
             
                var Score = scoringFunc(build);
            }
        }

        return result;
    }

    private static List<ItemIndentified> GenerateRandomElements(Item item)
    {
        if (!Item.HasRandomEffect(item)) return [(ItemIndentified)item];
        List<ItemIndentified> result = [];
        var randomEffectCombinations = GetRandomEffectCombinations(item);

        foreach (var combination in GenerateEffectCombinations(randomEffectCombinations))
        {
            var identifiedItem = (ItemIndentified)item;
            identifiedItem.RandomElement = combination;
            result.Add(identifiedItem);
        }
        return result;
    }

    private static List<(int effectIndex, List<Element[]> elementCombinations)> GetRandomEffectCombinations(Item item)
    {
        var elementTypes = new[] { Element.Fire, Element.Water, Element.Air, Element.Earth };
        var randomEffectCombinations = new List<(int, List<Element[]>)>();

        for (int i = 0; i < item.Effects.Count; i++)
        {
            var effect = item.Effects[i];
            if (!Effect.IsRandom(effect)) continue;
            var elementCombinations = (List<Element[]>)elementTypes.DifferentCombinations(Item.GetRandomAmount(effect));
            randomEffectCombinations.Add((i, elementCombinations));
        }

        return randomEffectCombinations;
    }

    private static IEnumerable<Dictionary<int, Element[]>> GenerateEffectCombinations(
        List<(int effectIndex, List<Element[]> elementCombinations)> randomEffectCombinations)
    {
        var initialCombination = new Dictionary<int, Element[]>();
        IEnumerable<Dictionary<int, Element[]>> result = [initialCombination];
        foreach (var (effectIndex, elementCombinations) in randomEffectCombinations)
        {
            result = result.SelectMany(currentCombination => elementCombinations.Select(elementCombo =>
            {
                var newCombination = new Dictionary<int, Element[]>(currentCombination)
                {
                    [effectIndex] = elementCombo
                };
                return newCombination;
            }));
        }
        return result;
    }

    private static List<ItemIndentified> GenerateEnchantedItems(Item item)
    {            
        List<ItemIndentified> results = [];
        EnchatmentType[] enchatmentType = [EnchatmentType.Red, EnchatmentType.Green, EnchatmentType.Blue];
        foreach (var slot1type in enchatmentType)
        foreach (var slot2type in enchatmentType)
        foreach (var slot3type in enchatmentType)
        foreach (var slot4type in enchatmentType)
        {
            var slot1options = Enchantment.GetEnchantments(slot1type);
            var slot2options = Enchantment.GetEnchantments(slot2type);
            var slot3options = Enchantment.GetEnchantments(slot3type);
            var slot4options = Enchantment.GetEnchantments(slot4type);

            foreach (var slot1enchantment in slot1options)
            foreach (var slot2enchantment in slot2options)
            foreach (var slot3enchantment in slot3options)
            foreach (var slot4enchantment in slot4options)
            {
                var itemindentified = (ItemIndentified)item;
                var maxlevel = Enchantment.MaxLevel(itemindentified);
                itemindentified.Slot1.Type = slot1type;
                itemindentified.Slot1.Effect = slot1enchantment;
                itemindentified.Slot1.Level = maxlevel;
                itemindentified.Slot2.Type = slot2type;
                itemindentified.Slot2.Effect = slot2enchantment;
                itemindentified.Slot2.Level = maxlevel;
                itemindentified.Slot3.Type = slot3type;
                itemindentified.Slot3.Effect = slot3enchantment;
                itemindentified.Slot3.Level = maxlevel;
                itemindentified.Slot4.Type = slot4type;
                itemindentified.Slot4.Effect = slot4enchantment;
                itemindentified.Slot4.Level = maxlevel;
                results.Add(itemindentified);
            }
        }
        return results;
    }
}