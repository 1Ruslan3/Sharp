using System.Numerics;
using System;

namespace Kardano
{
    internal static class Program
    {

        static Complex[] Root(
                Complex value,
                int degree)
        {
            var result = new Complex[degree];
            var magnitude = value.Magnitude;
            var magnitudeRoot = Math.Pow(magnitude, 1.0 / degree);
            var argument = value.Phase;

            for (var k = 0; k < degree; k++)
            {
                result[k] = new Complex(magnitudeRoot * Math.Cos((argument + 2 * Math.PI * k) / degree), magnitudeRoot * Math.Sin((argument + 2 * Math.PI * k) / degree));
            }

            return result;
        }

        static Complex[] Kardano(double x3Coeff, double x2Coeff, double x1Coeff, double x0Coeff, double epsilon)
        {
            if (epsilon <= 0)
            {
                throw new ArgumentException("Epsilon can't be LT or EQ to 0.", nameof(epsilon));
            }

            if (Math.Abs(x3Coeff) < epsilon)
            {
                throw new ArgumentException("Coefficient at x^3 can't be EQ to 0.", nameof(x3Coeff));
            }

            var p = (3 * x3Coeff * x1Coeff - x2Coeff * x2Coeff) / (3 * x3Coeff * x3Coeff);
            var q = (2 * Math.Pow(x2Coeff, 3) - 9 * x3Coeff * x2Coeff * x1Coeff + 27 * Math.Pow(x3Coeff, 2) * x0Coeff) / (27 * Math.Pow(x3Coeff, 3));

            var Q = Math.Pow(p / 3, 3) + Math.Pow(q / 2, 2);

            // Q == 0
            if (Math.Abs(Q) < epsilon)  
            {
                var alpha = Math.Pow(-q / 2, 1.0 / 3);
                var beta = alpha;
                var root1 = alpha + beta;
                var root2 = -root1 / 2;
                return new[] { new Complex(root1, 0), new Complex(root2, 0), new Complex(root2, 0) };
            }
            // Q > 0
            else if (Q > epsilon) 
            {
                var sqrtQ = Math.Sqrt(Q);
                var alpha = Math.Cbrt(-q / 2 + sqrtQ);
                var beta = Math.Cbrt(-q / 2 - sqrtQ);
                var root1 = alpha + beta;
                var root23RealPart = root1 / -2;
                var root23ImaginaryPart = (alpha - beta) * Math.Sqrt(3) / 2;

                return new[]
                {
                new Complex(root1, 0), new Complex(root23RealPart, root23ImaginaryPart), new Complex(
                    root23RealPart, -root23ImaginaryPart)
            };
            }
            else
            {
                var root1 = new Complex(-q / 2, Math.Sqrt(Math.Abs(Q)));
                var root2 = new Complex(-q / 2, -Math.Sqrt(Math.Abs(Q)));

                var roots1 = Root(root1, 3);
                var roots2 = Root(root2, 3);

                for (int i = 0; i < 3; i++)
                {
                    var alpha = roots1[i];

                    // Complex beta;
                    var beta = default(Complex);

                    for (int j = 0; j < 3; j++)
                    {
                        var multiplication = alpha * roots2[j];
                        if (Math.Abs(multiplication.Imaginary) < epsilon && Math.Abs(multiplication.Real + p / 3) < epsilon)
                        {
                            beta = roots2[j];
                            break;
                        }
                    }

                    if (beta == default(Complex))
                    {
                        continue;
                    }

                    var resultRoot1 = alpha + beta;

                    var resultLeftPart = (alpha + beta) / -2;
                    var resultRightPart = (alpha - beta) / 2 * Math.Sqrt(3) * new Complex(0, 1);

                    return new[] { resultRoot1, resultLeftPart + resultRightPart, resultLeftPart - resultRightPart };
                }
            }

            throw new ArithmeticException("Unreachable state reached.");
        }

        static void Main()
        {

            var results = Kardano(4, 11, -3, -2, 1e-7); 
            Console.WriteLine("Solutions: {0}, {1}, {2}", results[0], results[1], results[2]);

           Complex[] result1 = Vi.Vieta(4, 11, -3, -2, 1e-7);
            Console.Write("Solutions: ");
            Console.Write(result1[0]);
            Console.Write(result1[1]);
            Console.Write(result1[2]);
        }

    }
}