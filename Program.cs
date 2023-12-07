using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2 
{
    class Program
    {
        public static double Factorial(int N)
        {
            double factorial_ = 1;
            if (N != 0)
            {
                for (int i = 2; i <= N; i++)
                {
                    factorial_ *= i;
                }
            }
            return factorial_;
        }
        static decimal SumE(int k) => (decimal)(1 / Factorial(k));
        static decimal FuncE1()
        {
            decimal prev, cur;
            decimal sum = 1;
            int k = 1;
            cur = SumE(k++);
            sum += cur;
            do
            {
                prev = cur;
                cur = SumE(k++);
                sum += cur;
            } while (Math.Abs(cur - prev) >= 0.000000000000001m);
            return sum;
        }
        static double FuncE2() => Math.Sinh(1) + Math.Cosh(1);
        static decimal FuncPi1()
        {
            decimal pi = 0;
            decimal regToken;
            var i = 0;
            do
            {
                var coef = 1 / (decimal)Math.Pow(16, i);
                var s1 = 4m / (8 * i + 1);
                var s2 = 2m / (8 * i + 4);
                var s3 = 1m / (8 * i + 5);
                var s4 = 1m / (8 * i + 6);
                regToken = coef * (s1 - s2 - s3 - s4);
                pi += regToken;

                ++i;
            } while (Math.Abs(regToken) >= 0.000000000000001m);
            return pi;
        }

        static double FuncPi2() => 2 * (Math.Asin(Math.Sqrt(1 - Math.Pow(1, 2))) + Math.Abs(Math.Asin(1)));
        static decimal FuncLn1()
        {
            decimal ln2 = 0;
            var counter = 1;
            decimal sum;
            do
            {
                sum = (decimal)(1 / (counter * Math.Pow(2, counter)));
                ln2 += sum;
                ++counter;
            } while (Math.Abs(sum) >= 0.00000000000000001m);
            return ln2 * 10;
        }
        static double FuncLn2() => Math.Log(10) * Math.Log10(2);
        static decimal SumSqrt(decimal k) => (decimal)((Convert.ToDecimal(1) / 2) * (k + 2 / k));
        static decimal FuncSqrt1()
        {
            decimal cur;
            decimal prev = 1;
            cur = SumSqrt(prev);
            do
            {
                prev = cur;
                cur = SumSqrt(prev);
            } while (Math.Abs(cur - prev) >= 0.000000000000001m);
            return cur;
        }
        static double FuncSqrt2() => 2 * Math.Cos(Math.PI / 4);

        static double FuncG1()
        {
            const double e_gamma = -0.870057726728315506734648;

            return -2 * (2 * e_gamma / (double)Math.Sqrt(Math.PI) + (double)Math.Log(2));
        }
        static decimal FuncG2() => (decimal)Math.Log(1.7810724179901979852);

        static void Main()
        {
            Console.WriteLine("e = {0}", FuncE1());
            Console.WriteLine("e = {0}", FuncE2());
            Console.WriteLine("Pi = {0}", FuncPi1());
            Console.WriteLine("Pi = {0}", FuncPi2());
            Console.WriteLine("ln2 = {0}", FuncLn1());
            Console.WriteLine("ln2 = {0}", FuncLn2());
            Console.WriteLine("sqrt(2) = {0}", FuncSqrt1());
            Console.WriteLine("sqrt(2) = {0}", FuncSqrt2());
            Console.WriteLine("g = {0}", FuncG1());
            Console.WriteLine("g = {0}", FuncG2());
        }
    }


}