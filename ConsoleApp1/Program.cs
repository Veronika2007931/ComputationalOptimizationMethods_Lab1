using System;

namespace OptimizationMethods
{
    class Program
    {

        static double Func1D(double x) => (x + 7.0) * Math.Exp(x);

        public static double Dichotomy(Func<double, double> f, double a, double b, double eps, bool printSteps = true)
        {
            double delta = eps / 5.0;
            int iteration = 0;

            if (printSteps)
            {
                Console.WriteLine("Ітераційний процес методу дихотомії:");
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

                if (printSteps)
                {
                    Console.WriteLine($"{iteration,-6} | {a,-10:F4} | {b,-10:F4} | {x1,-10:F4} | {x2,-10:F4} | {f1,-12:F6} | {f2,-12:F6} | {(b - a),-10:F4}");
                }

                if (f1 < f2)
                    b = x2;
                else
                    a = x1;
            }

            return (a + b) / 2.0;
        }


        static double Func2D(double x1, double x2) => 7.0 * x1 * x1 + 5.0 * x2 * x2 - 11.0;

        static double[] Gradient(double x1, double x2) => new double[] { 14.0 * x1, 10.0 * x2 };

        static double Norm(double[] vec) => Math.Sqrt(vec[0] * vec[0] + vec[1] * vec[1]);

        static void NewtonRaphson(double[] x0, double eps)
        {
            double[] x = (double[])x0.Clone();
            int iter = 0;

            Console.WriteLine($"\nПочаткова точка: ({x[0]:F4}, {x[1]:F4})");
            Console.WriteLine($"{"Крок",-6} | {"x1",-10} | {"x2",-10} | {"f(X)",-12} | {"||grad||",-12} | {"alpha",-10}");
            Console.WriteLine(new string('-', 68));

            while (true)
            {
                iter++;
                double[] grad = Gradient(x[0], x[1]);
                double gradNorm = Norm(grad);
                double fVal = Func2D(x[0], x[1]);

                if (gradNorm <= eps)
                {
                    Console.WriteLine($"{iter,-6} | {x[0],-10:F6} | {x[1],-10:F6} | {fVal,-12:F6} | {gradNorm,-12:F6} | {"-",-10}");
                    break;
                }


                double[] s = new double[] { -grad[0] / 14.0, -grad[1] / 10.0 };


                double DirectionalFunction(double alpha) => Func2D(x[0] + alpha * s[0], x[1] + alpha * s[1]);
                double alphaOptimal = Dichotomy(DirectionalFunction, 0.0, 2.0, eps, printSteps: false);

                Console.WriteLine($"{iter,-6} | {x[0],-10:F6} | {x[1],-10:F6} | {fVal,-12:F6} | {gradNorm,-12:F6} | {alphaOptimal,-10:F4}");

                x[0] += alphaOptimal * s[0];
                x[1] += alphaOptimal * s[1];
            }

            Console.WriteLine($"\nЗнайдений мінімум: X* = ({x[0]:F6}, {x[1]:F6})");
            Console.WriteLine($"Значення функції в точці мінімуму: f(X*) = {Func2D(x[0], x[1]):F6}");
        }

        static void Main()
        {
            double eps = 0.01;

            Console.WriteLine("================ ЧАСТИНА 1: МЕТОД ДИХОТОМІЇ ================");
            double a = -10.0, b = -6.0;
            double xOpt = Dichotomy(Func1D, a, b, eps);
            Console.WriteLine($"\nРезультат: x* = {xOpt:F4}, f(x*) = {Func1D(xOpt):F6}");

            Console.WriteLine("\n================ ЧАСТИНА 2: МЕТОД НЬЮТОНА-РАФСОНА ================");
            double[] startPoint = new double[] { 4.0, 3.0 };
            NewtonRaphson(startPoint, eps);
        }
    }
}