using System.Reflection;

namespace WakfuBuider;

public static partial class Util
{
    public static bool ContainsSequence<T>(T[] array, T[] sequence)
    {
        if (sequence.Length == 0 || sequence.Length > array.Length) return false;

        for (int i = 0; i <= array.Length - sequence.Length; i++)
        {
            if (array.Skip(i).Take(sequence.Length).SequenceEqual(sequence)) return true;
        }
        return false;
    }

    public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k)
    {
        return k == 0 ? [[]] : elements.SelectMany((e, i) => elements.Skip(i + 1).DifferentCombinations(k - 1).Select(c => (new[] { e }).Concat(c)));
    }

    public static string ReadEmbeddedResource(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using Stream stream = assembly.GetManifestResourceStream(resourceName) ?? throw new Exception($"ReadEmbeddedResource, resourceName {resourceName}: stream is null");
        using StreamReader reader = new(stream);
        return reader.ReadToEnd();
    }

    public static int PosibilityCounter(List<Item> items)
    {
        var count = 0;
        foreach (var item in items)
        {
            if (Item.HasRandomEffect(item) == false) { ++count; continue; }
            var counterffect = 0;
            foreach (var effect in item.Effects)
            {
                if (!Effect.IsRandom(effect)) continue;
                var amount = Item.GetRandomAmount(effect);
                if (amount == 1) counterffect *= 4;
                if (amount == 2) counterffect *= 4 * 3;
                if (amount == 3) counterffect *= 4 * 3 * 2;
            }
            count += counterffect;
        }
        return count;
    }
    
    public static (int distinctCombinations, Dictionary<string, int> combinationCounts) CountEffectCombinations(List<Item> items)
    {
        // A dictionary to store the count of each distinct combination of effect types
        var effectCombinationsCount = new Dictionary<string, int>();

        foreach (var item in items)
        {
            // Get the effect types for the item and sort them to ignore order
            var effectTypes = item.Effects.Select(e => e.Definition.Type).OrderBy(type => type);

            // Join them into a single string to represent this combination (sorting ensures consistent comparison)
            string combinationKey = string.Join(",", effectTypes);

            // If this combination already exists, increment its count, otherwise add it to the dictionary
            if (effectCombinationsCount.ContainsKey(combinationKey))
            {
                effectCombinationsCount[combinationKey]++;
            }
            else
            {
                effectCombinationsCount[combinationKey] = 1;
            }
        }

        return (distinctCombinations: effectCombinationsCount.Count, combinationCounts: effectCombinationsCount);
    }
}