using System.Collections;

namespace _2CS2;

public static class EnumerableExtensions
{
    // проверка на уникальность поданных элементов
    private static void ThrowIfNotDistinct<T>(
        this IEnumerable<T> values,
        IEqualityComparer<T> equalityComparer)
    {
        if (values.Distinct(equalityComparer).Count() != values.Count())
        {
            throw new ArgumentException("ThrowIfNotDistinct: Elements are repeated.", nameof(values));
        }
    }

    /* генерация всех возможных сочетаний из n (кол-во элементов
    перечисления) по k (с точностью до порядка, элементы могут
    повторяться) из элементов входного перечисления:
        Входное перечисление: [1, 2, 3]; k == 2
        Выходное перечисление: [ [1, 1], [1, 2], [1, 3], [2, 2], [2, 3], [3, 3] ] */

    public static IEnumerable<IEnumerable<T>> GetGenerationCombinationsWithElementRepetition<T>(
        this IEnumerable<T>? collection, int k, IEqualityComparer<T>? comparer)
    {
        Console.WriteLine("-> IEnumerable.GetGenerationCombinationsWithElementRepetition called");

        if (collection is null)
        {
            throw new ArgumentNullException(nameof(collection), "GetGenerationCombinationsWithElementRepetition: collection shouldn't be null.");
        }

        if (comparer is null)
        {
            throw new ArgumentNullException(nameof(collection), "GetGenerationCombinationsWithElementRepetition: comparer shouldn't be null.");

        }

        ThrowIfNotDistinct(collection, comparer);

        if (k > collection.Count())
        {
            throw new ArgumentException("GetGenerationCombinationsWithElementRepetition: Parameter k must not be less than the length of the collection.", nameof(collection));
        }

        return collection.GenerationCombinationsWithElementRepetition(k);
    }

    public static IEnumerable<IEnumerable<T>> GenerationCombinationsWithElementRepetition<T>(
        this IEnumerable<T> collection, int k)
    {
        if (k <= 0)
        {
            yield return new List<T>();
        }
        else
        {
            int curr = 0; // для исключения повторений самих сочетаний
            foreach (var i in collection)
            {
                foreach (var j in collection.Skip(curr++).GenerationCombinationsWithElementRepetition<T>(k - 1))
                {
                    List<T> tmpCollection = new List<T>();
                    tmpCollection.Add(i);
                    tmpCollection.AddRange(j);
                    yield return tmpCollection;
                }
            }
        }
    }

    /* генерация всех возможных подмножеств (без повторений) из
    элементов входного перечисления:
        Входное перечисление: [1, 2]
        Выходное перечисление: [ [], [1], [2], [1, 2] ] */

    public static IEnumerable<IEnumerable<T>> GetGenerationSubset<T>(
        this IEnumerable<T>? collection, IEqualityComparer<T>? comparer)
    {
        Console.WriteLine("-> IEnumerable.GenerationSubSet called");

        if (collection is null)
        {
            throw new ArgumentNullException(nameof(collection), "GetGenerationSubset: collection shouldn't be null.");
        }

        if (comparer is null)
        {
            throw new ArgumentNullException(nameof(comparer), "GetGenerationSubset: comparer shouldn't be null.");

        }

        ThrowIfNotDistinct(collection, comparer);

        // degree = 2^countCollection, количество подмножеств в множестве
        int degree = 1, countCollection = collection.Count();
        int tmp = countCollection;
        while (tmp > 0)
        {
            degree *= 2;
            tmp--;
        }

        var resultList = new List<IEnumerable<T>>();
        var elementList = new List<T>();
        for (int i = 0; i < degree; i++) // перебор битовых масок
        {
            for (int j = 0; j < countCollection; j++) // перебор битов в маске
            {
                if (Convert.ToBoolean(i & (1 << j)))
                {
                    elementList.Add(collection.ElementAt(j));
                }
            }

            resultList.Add(new List<T>(elementList));
            elementList.Clear();
        }

        return resultList;
    }

    /* генерация всех возможных перестановок (без повторений) из
    элементов входного перечисления:
        Входное перечисление: [1, 2, 3]
        Выходное перечисление: [ [1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1, 2], [3, 2, 1] ] */

    public static IEnumerable<IEnumerable<T>> GetGenerationPermutationWithoutElementRepetition<T>(
        this IEnumerable<T>? collection, IEqualityComparer<T>? comparer)
    {
        Console.WriteLine("-> IEnumerable.GetGenerationPermutationWithoutElementRepetition called");

        if (collection is null)
        {
            throw new ArgumentNullException(nameof(collection), "GenerationPermutation: collection shouldn't be null.");
        }

        if (comparer is null)
        {
            throw new ArgumentNullException(nameof(comparer), "GenerationPermutation: comparer shouldn't be null.");

        }

        ThrowIfNotDistinct(collection, comparer);

        return collection.GenerationPermutationWithoutElementRepetition();
    }

    public static IEnumerable<IEnumerable<T>> GenerationPermutationWithoutElementRepetition<T>(
        this IEnumerable<T> collection)
    {
        if (collection.Count() == 0) // если все элементы использованы, заканчиваем рекурсию
        {
            yield return new List<T>();
        }

        foreach (var i in collection)
        {
            var next = collection.Where(l => !(l.Equals(i))).ToList();
            foreach (var perm in GenerationPermutationWithoutElementRepetition(next))
            {
                yield return (new List<T> { i }).Concat(perm);
            }
        }
    }
}