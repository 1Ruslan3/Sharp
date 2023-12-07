namespace _3CS3;

public static class SortExtensions
{
    public enum SortingOrder // порядок сортировки
    {
        Ascending = 0, // по возрастанию
        Descending = 1 // по убыванию
    }

    public enum SortingAlgorithm  // алгоритмы сортировки
    {
        InsertionSort, // вставками
        SelectionSort, // выбором
        HeapSort,      // пирамидальная
        QuickSort,     // быстрая
        MergeSort      // слиянием
    }

    #region перегруженные методы расширения метода List.Sort()
    // четвёртого параметра нет
    public static T[] Sort<T>(
        this T[] collectionToSort, SortingOrder sortingOrder, SortingAlgorithm sortingAlgorithm)
    {
        var comparer = Comparer<T>.Default; // берёт comparer из System.Collections.Generic\
        return collectionToSort.Sort(sortingOrder, sortingAlgorithm, comparer);
    }

    // четвёртый параметр - значение типа Comparer<T>
    public static T[] Sort<T>(
        this T[] collectionToSort, SortingOrder sortingOrder, SortingAlgorithm sortingAlgorithm, Comparer<T> comparer)
    {
        return collectionToSort.Sort(sortingOrder, sortingAlgorithm, comparer as IComparer<T>);
    }

    // четвёртый параметр - значение типа IComparer<T>
    public static T[] Sort<T>(
        this T[] collectionToSort, SortingOrder sortingOrder, SortingAlgorithm sortingAlgorithm, IComparer<T> comparer)
    {
        switch (sortingAlgorithm)
        {
            case SortingAlgorithm.InsertionSort:
                return InsertionSort(collectionToSort, sortingOrder, comparer);
            case SortingAlgorithm.SelectionSort:
                return SelectionSort(collectionToSort, sortingOrder, comparer);
            case SortingAlgorithm.MergeSort:
                return MergeSort(collectionToSort, sortingOrder, comparer);
            case SortingAlgorithm.HeapSort:
                return HeapSort(collectionToSort, sortingOrder, comparer);
            case SortingAlgorithm.QuickSort:
                return QuickSort(collectionToSort, sortingOrder, comparer); // можно не писать данный case, однако пусть будет, т.к. понадобится при добавлении новых методов
            default:
                return QuickSort(collectionToSort, sortingOrder, comparer);
        }
    }

    // четвёртый параметр - делегат Comparison<T> на объект которого подписан метод, задающий внешнее отношение порядка на пространстве элементов типа T

    public static T[] Sort<T>(
        this T[] collectionToSort, SortingOrder sortingOrder, SortingAlgorithm sortingAlgorithm, Comparison<T> comparison)
    {
        var comparer = Comparer<T>.Create(comparison);
        return collectionToSort.Sort(sortingOrder, sortingAlgorithm, comparer);
    }
    #endregion

    #region вспомогательные методы для сортировок
    private static void Swap<T>(T[] a, int i, int j)
    {
        (a[i], a[j]) = (a[j], a[i]); // swap two variables without using a temporary variable
    }

    private static bool CompareInner<T>(T x, T y, SortingOrder sortingOrder, IComparer<T> comparer)
    {
        return ((sortingOrder == SortingOrder.Ascending) & (comparer.Compare(x, y) < 0));
    }
    #endregion


    #region алгоритмы сортировки

    #region InsertionSort
    private static T[] InsertionSort<T>(T[] keys, SortingOrder sortingOrder, IComparer<T> comparer)
    {
        T[] toReturn = new T[keys.Length];
        toReturn[0] = keys[0];

        for (int i = 0; i < keys.Length - 1; i++)
        {
            T t = keys[i + 1];

            int j = i;
            while (j >= 0 && CompareInner(t, toReturn[j], sortingOrder, comparer))
            {
                toReturn[j + 1] = toReturn[j];
                j--;
            }

            toReturn[j + 1] = t;
        }

        return toReturn;
    }
    #endregion

    #region SelectionSort
    private static T[] SelectionSort<T>(T[] keys, SortingOrder sortingOrder, IComparer<T> comparer)
    {
        T[] toReturn = (T[])keys.Clone();

        for (int i = 0; i < toReturn.Length - 1; i++)
        {
            int min = i;
            for (int j = i + 1; j < toReturn.Length; j++)
            {
                if (CompareInner(toReturn[j], toReturn[min], sortingOrder, comparer))
                {
                    min = j;
                }
            }

            if (min != i)
            {
                Swap(toReturn, i, min);
            }
        }
        return toReturn;
    }
    #endregion

    #region HeapSort
    private static T[] HeapSort<T>(T[] keys, SortingOrder sortingOrder, IComparer<T> comparer)
    {
        T[] toReturn = (T[])keys.Clone();

        int n = toReturn.Length;
        for (int i = n >> 1; i >= 1; i--)
        {
            DownHeap(toReturn, i, n, sortingOrder, comparer);
        }

        for (int i = n; i > 1; i--)
        {
            Swap(toReturn, 0, i - 1);
            DownHeap(toReturn, 1, i - 1, sortingOrder, comparer);
        }

        return toReturn;
    }

    private static void DownHeap<T>(T[] keys, int i, int n, SortingOrder sortingOrder, IComparer<T> comparer)
    {
        T tmp = keys[i - 1];
        while (i <= n >> 1)
        {
            int child = 2 * i;
            if (child < n && CompareInner(keys[child - 1], keys[child], sortingOrder, comparer))
            {
                child++;
            }

            if (!CompareInner(tmp, keys[child - 1], sortingOrder, comparer))
                break;

            keys[i - 1] = keys[child - 1];
            i = child;
        }

        keys[i - 1] = tmp;
    }
    #endregion

    #region QuickSort
    private static T[] QuickSort<T>
        (T[] keys, SortingOrder sortingOrder, IComparer<T> comparer)
    {
        {
            T[] toReturn = (T[])keys.Clone();

            QuickSortingInner(toReturn, 0, toReturn.Length - 1, sortingOrder, comparer);

            return toReturn;
        }
    }

    private static void QuickSortingInner<T>
        (T[] keys, int leftBound, int rightBound, SortingOrder sortingOrder, IComparer<T> comparer)
    {
        if (leftBound < rightBound)
        {
            int pivot = Partition(keys, leftBound, rightBound, sortingOrder, comparer);
            QuickSortingInner(keys, leftBound, pivot - 1, sortingOrder, comparer);
            QuickSortingInner(keys, pivot + 1, rightBound, sortingOrder, comparer);
        }
    }

    private static int Partition<T> // для разделения обобщённого массива данных
        (T[] keys, int leftBound, int rightBound, SortingOrder sortingOrder, IComparer<T> comparer)
    {
        T pivot = keys[rightBound];
        int i = leftBound - 1;
        for (int j = leftBound; j < rightBound; j++)
        {
            if (CompareInner(keys[j], pivot, sortingOrder, comparer))
            {
                ++i;
                Swap(keys, i, j);
            }
        }

        Swap(keys, i + 1, rightBound);
        return (i + 1);
    }
    #endregion

    #region MergeSort

    private static T[] MergeSort<T>(T[] keys, SortingOrder sortingOrder, IComparer<T> comparer)
    {
        T[] toReturn = (T[])keys.Clone();
        if (keys.Length == 1) return toReturn;
        int middle = keys.Length / 2;

        return Merge(MergeSort(toReturn.Take(middle).ToArray(), sortingOrder, comparer),
            MergeSort(toReturn.Skip(middle).ToArray(), sortingOrder, comparer), sortingOrder, comparer);
    }

    private static T[] Merge<T>(T[] first, T[] second, SortingOrder sortingOrder, IComparer<T> comparer)
    {
        int ptr1 = 0, ptr2 = 0;
        T[] merged = new T[first.Length + second.Length];

        for (int i = 0; i < merged.Length; ++i)
        {
            if (ptr1 < first.Length && ptr2 < second.Length)
            {
                merged[i] = CompareInner(second[ptr2], first[ptr1], sortingOrder, comparer) ? second[ptr2++] : first[ptr1++];
            }
            else
            {
                merged[i] = ptr2 < second.Length ? second[ptr2++] : first[ptr1++];
            }
        }

        return merged;
    }

    #endregion

    #endregion
}