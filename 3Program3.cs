using System;

namespace _3CS3 
{
    class Program
    {
        sealed class IntComparerWithComparer : Comparer<int>
        {
            public override int Compare(int x, int y)
            {
                return (x - y);
            }
        }
        public static void ToStringArray<T>(T[] arr)
        {
            Console.WriteLine(string.Join(", ", arr));
        }
        public static int Comparison(int x, int y)
        {
            return (x - y);
        }
        static void Main(string[] args)
        {
            try
            {
                int min = -1000;
                int max = 1000;
                Random randNumber = new Random();
                var comparer = new IntComparerWithComparer();
                int[] set = Enumerable
                    .Repeat(0, 10)
                    .Select(i => randNumber.Next(min, max))
                    .ToArray();

                var insertionSorted = set.Sort(SortExtensions.SortingOrder.Ascending, SortExtensions.SortingAlgorithm.InsertionSort, comparer);
                var selectionSorted = set.Sort(SortExtensions.SortingOrder.Ascending, SortExtensions.SortingAlgorithm.SelectionSort);
                var heapSorted = set.Sort(SortExtensions.SortingOrder.Ascending, SortExtensions.SortingAlgorithm.HeapSort, Comparison);
                var quickSorted = set.Sort(SortExtensions.SortingOrder.Ascending, SortExtensions.SortingAlgorithm.QuickSort);
                var mergeSorted = set.Sort(SortExtensions.SortingOrder.Ascending, SortExtensions.SortingAlgorithm.MergeSort, comparer);

                //Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("Original array:");
                ToStringArray(set);
                //Console.WriteLine("-----------------------------------------------------");

                Console.WriteLine("Selection sorting:");
                ToStringArray(selectionSorted);
                //Console.WriteLine("-----------------------------------------------------");

                Console.WriteLine("Insertion sorting:");
                ToStringArray(insertionSorted);
                //Console.WriteLine("-----------------------------------------------------");

                Console.WriteLine("Merge sorting");
                ToStringArray(mergeSorted);
                //Console.WriteLine("-----------------------------------------------------");

                Console.WriteLine("Heap sorting");
                ToStringArray(heapSorted);
                //Console.WriteLine("-----------------------------------------------------");

                Console.WriteLine("Quick sorting");
                ToStringArray(quickSorted);
                //Console.WriteLine("-----------------------------------------------------");

            }
            catch (ArgumentNullException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}