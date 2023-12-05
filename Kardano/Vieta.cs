using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Kardano
{
    internal class Vi 
    {
        public static Complex[] Vieta(double a, double b, double c, double d, double epsilon) 
        {
            if (epsilon <= 0)
            {
                throw new ArgumentException("Epsilon can't be LT or EQ to 0.", nameof(epsilon));
            }

            if (Math.Abs(a) < epsilon)
            {
                throw new ArgumentException("Coefficient at x^3 can't be EQ to 0.", nameof(a));
            }

            b /= a;
            c /= a;
            d /= a;

            Complex[] roots = new Complex[3];

            double Q = (b * b - 3 * c) / 9;
            double R = (2 * Math.Pow(b, 3) - 9 * b * c + 27 * d) / 54;

            double S = Math.Pow(Q, 3) - Math.Pow(R, 2);

            double phi;

            if (Math.Abs(S) > epsilon) 
            {
                phi = Math.Acos(R / Math.Pow(Q, 3.0 / 2)) / 3;
                roots[0] = -2 * Math.Sqrt(Q) * Math.Cos(phi) - b / 3;
                roots[1] = -2 * Math.Sqrt(Q) * Math.Cos(phi + 2 * 3.14 / 3) - b / 3;
                roots[2] = -2 * Math.Sqrt(Q) * Math.Cos(phi - 2 * 3.14 / 3) - b / 3;
            }
            else if (Math.Abs(S) < epsilon)
            {
                double tmpx = Math.Abs(R) / Math.Pow(Math.Abs(Q), 3.0 / 2);
                double T;

                if (Q > 0)
                {
                    phi = Math.Log(tmpx + Math.Sqrt(tmpx * tmpx - 1)) / 3;
                    T = Math.Sign(R) * Math.Sqrt(Q) * Math.Cosh(phi);

                    roots[0] = -2 * T - b / 3;
                    roots[1] = new Complex(T - b / 3, Math.Sqrt(3) * Math.Sqrt(Q) * Math.Sinh(phi));
                    roots[2] = new Complex(T - b / 3, -Math.Sqrt(3) * Math.Sqrt(Q) * Math.Sinh(phi));
                }
                else if (Q < 0)
                {
                    phi = Math.Log(tmpx + Math.Sqrt(tmpx * tmpx + 1)) / 3;
                    T = Math.Sign(R) * Math.Sqrt(Q) * Math.Sinh(phi);

                    roots[0] = -2 * T - b / 3;
                    roots[1] = new Complex(T - b / 3, Math.Sqrt(3) * Math.Sqrt(Math.Abs(Q)) * Math.Cosh(phi));
                    roots[2] = new Complex(T - b / 3, -Math.Sqrt(3) * Math.Sqrt(Math.Abs(Q)) * Math.Cosh(phi));
                }
                else if (Q.CompareTo(0) == 0)
                {
                    T = -Math.Pow(d - b * b * b / 27, 1.0 / 3) - b / 3;
                    roots[0] = T;
                    roots[1] = new Complex(-(b + T) / 2, 1.0 / 2 * Math.Sqrt(Math.Abs((b - 3 * T) * (b + T) - 4 * c)));
                    roots[2] = new Complex(-(b + T) / 2, -1.0 / 2 * Math.Sqrt(Math.Abs((b - 3 * T) * (b + T) - 4 * c)));
                }
            }
            else if (S.CompareTo(0) == 0)
            {
                double T = Math.Sign(R) * Math.Sqrt(Q);

                roots[0] = -2 * T - b / 3;
                roots[1] = T - b / 3;
                roots[2] = T - b / 3;
            }

            return roots; 
        }

    }
}
