namespace WakfuBuider;

public static partial class Utils
{
    public static bool ContainsSequence<T>(T[] array, T[] sequence)
    {
        if (sequence.Length == 0 || sequence.Length > array.Length)
            return false;

        for (int i = 0; i <= array.Length - sequence.Length; i++)
        {
            if (array.Skip(i).Take(sequence.Length).SequenceEqual(sequence))
                return true;
        }

        return false;
    }

    public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k)
    {
        return k == 0 ? [[]] :
          elements.SelectMany((e, i) =>
            elements.Skip(i + 1).DifferentCombinations(k - 1).Select(c => (new[] {e}).Concat(c)));
    }
}