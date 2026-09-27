namespace SOFTEST_INTRO_Calculator;

public class Calculator
{
    public double Add(double a, double b)
    {
        if (ToTwoBitValue(a) is int high && ToTwoBitValue(b) is int low)
        {
            return (high << 2) | low;
        }

        return a + b;
    }

    private static int? ToTwoBitValue(double value) => value switch
    {
        0 => 0b00,
        1 => 0b01,
        10 => 0b10,
        11 => 0b11,
        _ => null
    };

    public double Subtract(double a, double b) => a - b;
    public double Multiply(double a, double b) => a * b;

    public double Divide(double a, double b)
    {
        if (b == 0) throw new ArgumentException("Divisor cannot be zero.");
        return a / b;
    }

    public long Factorial(int n)
    {
        if (n < 0 || n > 20) throw new ArgumentOutOfRangeException(nameof(n));
        long result = 1;
        for (int i = 1; i <= n; i++) result *= i;
        return result;
    }

    public double TriangleArea(double height, double width)
    {
        if (height < 0 || width < 0 || !double.IsFinite(height) || !double.IsFinite(width))
            throw new ArgumentOutOfRangeException();
        return 0.5 * height * width;
    }

    public double CircleArea(double radius)
    {
        if (radius < 0 || !double.IsFinite(radius))
            throw new ArgumentOutOfRangeException();
        return Math.PI * radius * radius;
    }

    public double DoOperation(double a, double b, string op)
    {
        return op switch
        {
            "a" => Add(a, b),
            "s" => Subtract(a, b),
            "m" => Multiply(a, b),
            "d" => Divide(a, b),
            _ => throw new ArgumentException("Unknown operation.")
        };
    }
    public long UnknownFunctionA(int n, int r)
    {
        if (r < 0 || r > n || n > 20) throw new ArgumentOutOfRangeException();
        return Factorial(n) / Factorial(n - r);
    }

    public long UnknownFunctionB(int n, int r)
    {
        if (r < 0 || r > n || n > 20) throw new ArgumentOutOfRangeException();
        return Factorial(n) / (Factorial(r) * Factorial(n - r));
    }

    public double Mtbf(double operatingTime, int failureCount)
    {
        if (!double.IsFinite(operatingTime) || operatingTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(operatingTime), "Operating time must be a finite number greater than zero.");
        if (failureCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(failureCount), "Failure count must be greater than zero.");

        return operatingTime / failureCount;
    }

    public double Availability(double mtbf, double mttr)
    {
        if (!double.IsFinite(mtbf) || mtbf < 0)
            throw new ArgumentOutOfRangeException(nameof(mtbf), "MTBF must be a finite number that is not negative.");
        if (!double.IsFinite(mttr) || mttr < 0)
            throw new ArgumentOutOfRangeException(nameof(mttr), "MTTR must be a finite number that is not negative.");

        double total = mtbf + mttr;
        if (!double.IsFinite(total) || total <= 0)
            throw new ArgumentException("MTBF + MTTR must be greater than zero.");

        return mtbf / total;
    }

    // Basic Musa reliability-growth model. Units: initialIntensity in failures per unit
    // of execution time, executionTime in that same unit, totalFailures in failures.
    // Assumes a finite expected failure total and a constant decrease in failure
    // intensity per corrected failure. It is a model, not a guarantee for every system.

    // lambda(tau) = lambda0 * exp(-lambda0 * tau / nu0)
    public double BasicMusaFailureIntensity(double initialIntensity, double totalFailures, double executionTime)
    {
        ValidateBasicMusa(initialIntensity, totalFailures, executionTime);
        return initialIntensity * Math.Exp(-initialIntensity * executionTime / totalFailures);
    }

    // mu(tau) = nu0 * (1 - exp(-lambda0 * tau / nu0))
    public double BasicMusaExpectedFailures(double initialIntensity, double totalFailures, double executionTime)
    {
        ValidateBasicMusa(initialIntensity, totalFailures, executionTime);
        return totalFailures * (1 - Math.Exp(-initialIntensity * executionTime / totalFailures));
    }

    // Input domain: lambda0 > 0, nu0 > 0, tau >= 0, all finite.
    private static void ValidateBasicMusa(double initialIntensity, double totalFailures, double executionTime)
    {
        if (!double.IsFinite(initialIntensity) || initialIntensity <= 0)
            throw new ArgumentOutOfRangeException(nameof(initialIntensity), "Initial failure intensity must be a finite number greater than zero.");
        if (!double.IsFinite(totalFailures) || totalFailures <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalFailures), "Expected total failures must be a finite number greater than zero.");
        if (!double.IsFinite(executionTime) || executionTime < 0)
            throw new ArgumentOutOfRangeException(nameof(executionTime), "Execution time must be a finite number that is not negative.");
    }

    public double GenMagicNum(int choice, string path, IFileReader fileReader)
    {
        ArgumentNullException.ThrowIfNull(fileReader);

        if (choice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(choice));
        }

        string[] magicStrings = fileReader.Read(path);

        if (choice >= magicStrings.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(choice));
        }

        double magicNumber = double.Parse(magicStrings[choice]);
        return 2 * Math.Abs(magicNumber);
    }


}