using System;
using System.Linq;
using System.Text;

namespace CS5 
{
    public static class StringExtension
    {
        public static string ReplaceAll(this string str, string oldValue, string newValue)
        {
            while (str != null && str.Replace(oldValue, newValue) != str)
                str = str.Replace(oldValue, newValue);
            return str;
        }
    }

    internal static class Program
    {
        private static void Sort(string[] strCopy)
        {
            Console.WriteLine("\nWord created by last symbols");

            try
            {
                var result1 = strCopy.Aggregate("", (current, t) => current + t[t.Length - 1]);

                Console.WriteLine(result1);
                Console.WriteLine("sorting \n");

                Array.Sort(strCopy);

                foreach (string s in strCopy)
                    Console.Write(s + " ");

            }
            catch (Exception e)
            {
                Console.WriteLine("error! empty string");

            }

        }

        static void Main()
        {

            string userInput;

            do
            {
                Console.WriteLine("Input string");
                userInput = Console.ReadLine();
            } while (string.IsNullOrEmpty(userInput));

            userInput = userInput.ReplaceAll("  ", " ");
            userInput = userInput.Trim(' ');

            string[] masStr = userInput.Split(' ');

            var resultString = new StringBuilder(masStr.Length);

            Console.WriteLine("First sym UP n last DOWN");

            foreach (var str in masStr)
            {
                for (int j = 0; j < str.Length; j++)
                {
                    char st;
                    if (j == 0)
                        st = str.ToUpper()[j];
                    else if (j == str.Length - 1)
                        st = str.ToLower()[j];
                    else
                        st = str[j];
                    resultString.Append(st);
                }
                resultString.Append(' ');
            }
            Console.Write(resultString);
            Sort(masStr);

            Console.WriteLine("\nInput word");
            string word = Console.ReadLine();
            int n = 0;

            foreach (string element in masStr)
            {
                if (word == element)
                    n++;
            }

            Console.WriteLine("ur word was met {0} times in cur string", n);
            Console.WriteLine("\nInput word you wanna change with pre-last word");

            string word1 = Console.ReadLine();
            var mas = userInput.Split(' ');

            if (mas.Length - 2 >= 0)
                mas[mas.Length - 2] = word1;

            foreach (string str in mas)
            {
                Console.Write(str + ' ');
            }

            Console.WriteLine("\nInput k");
            try
            {
                var k = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                var mas2 = new string[mas.Length];
                int t = 0;
                foreach (string str in mas)
                {
                    if (str[0] == str.ToUpper()[0])
                    {
                        mas2[t++] = str;
                    }
                }
                Console.WriteLine("{0} word with first Capital Letter{1}", k, mas2[k - 1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
