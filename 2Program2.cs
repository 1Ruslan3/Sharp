using System;

namespace _2CS2 
{
    class Program
    {
        class IntComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return x == y;
            }

            public int GetHashCode(int obj)
            {
                return obj.GetHashCode();
            }
        }

        public static string EnumerableEnumerableToString<T>(IEnumerable<IEnumerable<T>> collections)
        {
            string result = "";
            result += "[";

            foreach (var collection in collections)
            {
                string collectionString = "";
                collectionString += " [ ";
                collectionString += string.Join(", ", collection);
                collectionString += " ],";
                result += collectionString;
            }

            result = result[..^1];
            result += " ]";
            return result;
        }
        static void Main(string[] args)
        {
            try
            {
                int[] set1 = { 1, 2, 3 };
                int[] set2 = { 1, 2 };
                int[] set3 = { 1, 2, 3 };
                var comparer = new IntComparer();

                var list = new List<int>();

                // test combinations
                Console.WriteLine(EnumerableEnumerableToString(
                    set1.GetGenerationCombinationsWithElementRepetition(2, comparer)));

                // test subset
                Console.WriteLine(EnumerableEnumerableToString(
                    set2.GetGenerationSubset(comparer)));

                // test permutation
                Console.WriteLine(EnumerableEnumerableToString(
                    set3.GetGenerationPermutationWithoutElementRepetition(comparer)));
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