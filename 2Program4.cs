class Program
{

    public interface IIntegralSolver
    {
        double Solve(Func<double, double> f, double a, double b, double epsilon);

        string MethodName
        {
            get;
        }
    }

    public abstract class IntegralSolverBase :
        IIntegralSolver
    {

        protected IntegralSolverBase(
            string methodName)
        {
            MethodName = methodName;
        }

        public double Solve(Func<double, double>? f, double a, double b, double epsilon)
        {
            if (f == null)
            {
                throw new ArgumentNullException(nameof(f));
            }

            if (a >= b)
            {
                throw new ArgumentException("Invalid bounds passed!", nameof(a));
            }

            if (epsilon <= 0)
            {
                throw new ArgumentException("Invalid epsilon passed!", nameof(epsilon));
            }

            return SolveLogic(f, a, b, epsilon);
        }

        public string MethodName
        {
            get;
        }

        protected abstract double SolveLogic(Func<double, double> f, double a, double b, double epsilon);

    }

    public sealed class LeftRectanglesIntegralSolver :
        IntegralSolverBase
    {

        public LeftRectanglesIntegralSolver() :
            base("Left rectangles")
        {

        }

        protected override double SolveLogic(
            Func<double, double> f,
            double a,
            double b,
            double epsilon)
        {
            var previousApproximation = 0d;
            var currentApproximation = 0d;
            int n = 1;
            var h = b - a;

            do
            {
                previousApproximation = currentApproximation;
                n *= 2;
                h /= 2;

                currentApproximation = 0d;

                for (var i = 0; i < n; i++)
                {
                    currentApproximation += f(a + i * h);
                }

                currentApproximation *= h;
            } while (Math.Abs(currentApproximation - previousApproximation) >= epsilon);

            return currentApproximation;
        }

    }

    public sealed class RightRectanglesIntegralSolver :
        IntegralSolverBase
    {

        public RightRectanglesIntegralSolver() :
            base("Right rectangles")
        {

        }

        protected override double SolveLogic(
            Func<double, double> f,
            double a,
            double b,
            double epsilon)
        {
            var previousApproximation = 0d;
            var currentApproximation = 0d;
            int n = 1;
            var h = b - a;

            do
            {
                previousApproximation = currentApproximation;
                n *= 2;
                h /= 2;

                currentApproximation = 0d;

                for (var i = 1; i <= n; i++)
                {
                    currentApproximation += f(a + i * h);
                }

                currentApproximation *= h;
            } while (Math.Abs(currentApproximation - previousApproximation) >= epsilon);

            return currentApproximation;
        }

    }

    public sealed class CentralRectanglesIntegralSolver :
        IntegralSolverBase
    {

        public CentralRectanglesIntegralSolver() :
            base("Central rectangles")
        {

        }

        protected override double SolveLogic(
            Func<double, double> f,
            double a,
            double b,
            double epsilon)
        {
            var previousApproximation = 0d;
            var currentApproximation = 0d;
            int n = 1;
            var h = b - a;

            do
            {
                previousApproximation = currentApproximation;
                n *= 2;
                h /= 2;

                currentApproximation = 0d;

                for (var i = 0; i < n; i++)
                {
                    currentApproximation += f(a + i * h + h / 2);
                }

                currentApproximation *= h;
            } while (Math.Abs(currentApproximation - previousApproximation) >= epsilon);

            return currentApproximation;
        }

    }

    

    static void Main(string[] args)
    {
        var solvers = new IIntegralSolver[]
        {
            new LeftRectanglesIntegralSolver(),
            new RightRectanglesIntegralSolver(),
            new CentralRectanglesIntegralSolver()
        };

        // локальная функция
        double Foo(double x) => x / (x - 1);
        double leftBound = 4;
        double rightBound = 10;
        double epsilon = 1e-5;

        try
        {
            foreach (var solver in solvers)
            {
                var solution = solver.Solve(Foo, leftBound, rightBound, epsilon);
                Console.WriteLine($"Formula for {solver.MethodName}: {solution:F9}");
            }
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (ArgumentException ex)
        {
            Console.Write(ex.Message);
        }
    }
}