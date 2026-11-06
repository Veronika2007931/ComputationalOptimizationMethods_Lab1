using System;

namespace OptimizationMethods
{
    class Program
    {
        static double Func1D(double x) => (x + 7.0) * Math.Exp(x);

        public static double Dichotomy(Func<double, double> f, double a, double b, double eps, bool printStep = true)
        {
            double delta = eps / 5.0;
            int iteration = 0;

            if (printStep)
            {
                Console.WriteLine("Ітераційний процес методу дихтомії:");
                Console.WriteLine($"{"Крок",-6} | {"a",-10} | {"b",-10} | {"x1",-10} | {"x2",-10} | {"f(x1)",-12} | {"f(x2)",-12} | {"b-a",-10}");
                Console.WriteLine(new string('-', 88));
            }

            while (Math.Abs(b - a) > eps)
            {
                iteration++;
                double mid = (a + b) / 2.0;
                double x1 = mid - delta;
                double x2 = mid + delta;

                double f1 = f(x1);
                double f2 = f(x2);

                if (printStep)
                {
                    Console.WriteLine($"{iteration,-6} | {a,-10:F4} | {b,-10:F4} | {x1,-10:F4} | {x2,-10:F4} | {f1,-12:F6} | {f2,-12:F6} | {(b - a),-10:F4}");
                }

                if (f1 < f2)
                {
                    b = x2;
                }
                else
                {
                    a = x1;
                }


            }
            return (a + b) / 2.0;
        }
    }
}