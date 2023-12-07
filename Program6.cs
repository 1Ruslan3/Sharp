using System;

namespace CS6 
{
    internal static class Program
    {
        delegate double Equation(double one);

        static double FuncForEquation(double x)
        {
            return x * x + 5 * x; 
        }

        static double FindResult(double limLeft, double limRight, Equation equation, double eps)
        {
            var isSameSign = equation(limLeft) * equation(limRight) >= 0;

            if (isSameSign)
            {
                throw new ArgumentException("No Roots ((");
            }
            double limMiddle;
            bool absEquation;
            do
            {
                limMiddle = (limLeft + limRight) / 2;

                var isLeftPart = equation(limLeft) * equation(limMiddle) < 0;

                if (isLeftPart)
                    limRight = limMiddle;
                else
                    limLeft = limMiddle;

                absEquation = Math.Abs(equation(limMiddle)) > eps;

            } while (absEquation);
            return limMiddle;
        }

        static void Main()
        {
            try
            {
                double result = FindResult(-6, -4, FuncForEquation, 1e-7);
                Console.WriteLine(result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

}