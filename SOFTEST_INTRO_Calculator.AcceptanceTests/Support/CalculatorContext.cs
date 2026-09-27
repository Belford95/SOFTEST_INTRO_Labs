using SOFTEST_INTRO_Calculator;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

public sealed class CalculatorContext
{
    public Calculator Calculator { get; set; } = null!;
    public double? Result { get; set; }
    public long? IntegerResult { get; set; }
    public Exception? Error { get; set; }

    // Runs a calculation, recording either its result or an expected rejection.
    // Only ArgumentException (and subclasses) count as an expected rejection;
    // any other exception is a genuine bug and still fails the test.
    public void Calculate(Func<double> calculation)
    {
        Result = null;
        Error = null;

        try
        {
            Result = calculation();
        }
        catch (ArgumentException error)
        {
            Error = error;
        }
    }
}