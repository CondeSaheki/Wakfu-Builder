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
        var boots = filteredItems.Where(item => item.Type == ItemType.Boot).ToList();
        var belts = filteredItems.Where(item => item.Type == ItemType.Belt).ToList();
        var helmets = filteredItems.Where(item => item.Type == ItemType.Helmet).ToList();
        var epaulettes = filteredItems.Where(item => item.Type == ItemType.Epaulette).ToList();
        var breastplates = filteredItems.Where(item => item.Type == ItemType.Breastplate).ToList();
        var cloaks = filteredItems.Where(item => item.Type == ItemType.Cloak).ToList();
        var emblems = filteredItems.Where(item => item.Type == ItemType.Emblem).ToList();
        var pets = filteredItems.Where(item => item.Type == ItemType.Pet).ToList();
        var oneHands = filteredItems.Where(Item.IsOneHandWeapon).ToList();
        var offHands = filteredItems.Where(Item.IsOffHandWeapon).ToList();
        var twoHands = filteredItems.Where(Item.IsTwoHandWeapon).ToList();
        
        if(requirements != null && requirements.Length > 0)
        {
            foreach (var requirement in requirements)
            {
                var amuletGamer = requirement.Amulet != null ? [requirement.Amulet] : amulets;
                var ringOneGamer = requirement.RingOne != null ? [requirement.RingOne] : rings;
                var ringTwoGamer = requirement.RingTwo != null ? [requirement.RingOne] : rings;
                var bootGamer = requirement.Boot != null ? [requirement.Boot] : boots;
                var beltGamer = requirement.Belt != null ? [requirement.Belt] : belts;
                var helmetGamer = requirement.Helmet != null ? [requirement.Helmet] : helmets;
                var epauletteGamer = requirement.Epaulette != null ? [requirement.Epaulette] : epaulettes;
                var breastplateGamer = requirement.Breastplate != null ? [requirement.Breastplate] : breastplates;
                var cloakGamer = requirement.Cloak != null ? [requirement.Cloak] : cloaks;
                var emblemGamer = requirement.Emblem != null ? [requirement.Emblem] : emblems;
                var petGamer = requirement.Pet != null ? [requirement.Pet] : pets;

                //var twoHands2 = requirement.TwoHand ?? (List<ItemIndentified>)twoHands;


                var oneHands2 = requirement.MainHand != null ? [requirement.MainHand] : oneHands;
                var offHands2 = requirement.OffHand != null ? [requirement.OffHand] : offHands;
                
                if(requirement.MainHand != null && requirement.OffHand != null)
                {
                    if (Item.IsOneHandWeapon(requirement.MainHand))
                    {
                    //var offHands2 = requirement.OffHand != null ? [requirement.OffHand] : offHands;

                    }
                }
                

            }
        }


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
        {
            var amuletIndentifieds = GenerateRandomElements(amulet);
            var ringOneIndentifieds = GenerateRandomElements(ringOne);
            var ringTwoIndentifieds = GenerateRandomElements(ringTwo);
            var bootIndentifieds = GenerateRandomElements(boot);
            var beltIndentifieds = GenerateRandomElements(belt);
            var helmetIndentifieds = GenerateRandomElements(helmet);
            var epauletteIndentifieds = GenerateRandomElements(epaulette);
            var breastplateIndentifieds = GenerateRandomElements(breastplate);
            var cloakIndentifieds = GenerateRandomElements(cloak);

            if (includeEnchantments)
            {
                amuletIndentifieds = amuletIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
                ringOneIndentifieds = ringOneIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
                ringTwoIndentifieds = ringTwoIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
                bootIndentifieds = bootIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
                beltIndentifieds = beltIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
                helmetIndentifieds = helmetIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
                epauletteIndentifieds = epauletteIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
                breastplateIndentifieds = breastplateIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
                cloakIndentifieds = cloakIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
            }

            foreach (var twoHand in twoHands)
            {
                var twoHandIndentifieds = GenerateRandomElements(twoHand);
                
                if (includeEnchantments)
                {
                    twoHandIndentifieds = twoHandIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
                }

                foreach (var amuletIndentified in amuletIndentifieds)
                foreach (var ringOneIndentified in ringOneIndentifieds)
                foreach (var ringTwoIndentified in ringTwoIndentifieds)
                foreach (var bootIndentified in bootIndentifieds)
                foreach (var beltIndentified in beltIndentifieds)
                foreach (var helmetIndentified in helmetIndentifieds)
                foreach (var epauletteIndentified in epauletteIndentifieds)
                foreach (var breastplateIndentified in breastplateIndentifieds)
                foreach (var cloakIndentified in cloakIndentifieds)
                // weapom
                foreach (var twoHandIndentified in twoHandIndentifieds)
                {
                    var build = new Build
                    {
                        Amulet = amuletIndentified,
                        RingOne = ringOneIndentified,
                        RingTwo = ringTwoIndentified,
                        Boot = bootIndentified,
                        Belt = beltIndentified,
                        Helmet = helmetIndentified,
                        Epaulette = epauletteIndentified,
                        Breastplate = breastplateIndentified,
                        Cloak = cloakIndentified,
                        Emblem = (ItemIndentified)emblem,
                        Pet = (ItemIndentified)pet,
                        MainHand = twoHandIndentified,
                        OffHand = null,
                    };

                    if (lateFilter?.Invoke(build) == false) continue;

                    var Score = scoringFunc(build);
                }
            }
            
            foreach (var oneHand in oneHands)
            foreach (var offHand in offHands)
            {
                var oneHandIndentifieds = GenerateRandomElements(oneHand);
                var offHandIndentifieds = GenerateRandomElements(offHand);

                if (includeEnchantments)
                {
                    oneHandIndentifieds = oneHandIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
                    offHandIndentifieds = offHandIndentifieds.SelectMany(GenerateEnchantedItems).ToList();
                }

                foreach (var amuletIndentified in amuletIndentifieds)
                foreach (var ringOneIndentified in ringOneIndentifieds)
                foreach (var ringTwoIndentified in ringTwoIndentifieds)
                foreach (var bootIndentified in bootIndentifieds)
                foreach (var beltIndentified in beltIndentifieds)
                foreach (var helmetIndentified in helmetIndentifieds)
                foreach (var epauletteIndentified in epauletteIndentifieds)
                foreach (var breastplateIndentified in breastplateIndentifieds)
                foreach (var cloakIndentified in cloakIndentifieds)
                // weapom
                foreach (var oneHandIndentified in oneHandIndentifieds)
                foreach (var offHandIndentified in offHandIndentifieds)
                {
                    var build = new Build
                    {
                        Amulet = amuletIndentified,
                        RingOne = ringOneIndentified,
                        RingTwo = ringTwoIndentified,
                        Boot = bootIndentified,
                        Belt = beltIndentified,
                        Helmet = helmetIndentified,
                        Epaulette = epauletteIndentified,
                        Breastplate = breastplateIndentified,
                        Cloak = cloakIndentified,
                        Emblem = (ItemIndentified)emblem,
                        Pet = (ItemIndentified)pet,
                        MainHand = oneHandIndentified,
                        OffHand = offHandIndentified,
                    };

                    if (lateFilter?.Invoke(build) == false) continue;
                    
                    var Score = scoringFunc(build);
                }
            }
        }
        return result;
    }


    public void Asd<T>(IEnumerable<T> a, int top)
    {

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

    private static List<ItemIndentified> GenerateEnchantedItems(ItemIndentified item)
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