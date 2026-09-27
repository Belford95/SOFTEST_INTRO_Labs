using SOFTEST_INTRO_Calculator;
using NUnit.Framework;

namespace SOFTEST_INTRO_Calculator.UnitTests;

public class CalculatorTests
{
    private Calculator _calculator = null!;

    [SetUp]
    public void SetUp()
    {
        _calculator = new Calculator();
    }

    [Test]
    public void Add_TwoPositiveNumbers_ReturnsSum()
    {
        double result = _calculator.Add(10, 20);
        Assert.That(result, Is.EqualTo(30));
    }

    [TestCase(0, 0, 0)]
    [TestCase(0, 5, 5)]
    [TestCase(-3, 8, 5)]
    [TestCase(0.1, 0.2, 0.3)]
    public void Add_RepresentativeInputs_ReturnsSum(double a, double b, double expected)
    {
        double result = _calculator.Add(a, b);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(1, 2, 0.5)]
    [TestCase(0, 15, 0)]
    [TestCase(15, -3, -5)]
    public void Divide_ValidInputs_ReturnsQuotient(double a, double b, double expected)
    {
        double result = _calculator.Divide(a, b);
        Assert.That(result, Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(15, 0)]
    [TestCase(0, 0)]
    public void Divide_ZeroDivisor_ThrowsArgumentException(double a, double b)
    {
        Assert.That(() => _calculator.Divide(a, b), Throws.TypeOf<ArgumentException>());
    }

    [TestCase(-1)]
    [TestCase(21)]
    public void Factorial_InvalidInputs_ThrowsArgumentOutOfRangeException(int n)
    {
        Assert.That(() => _calculator.Factorial(n), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(0, 1L)]
    [TestCase(1, 1L)]
    [TestCase(5, 120L)]
    [TestCase(20, 2432902008176640000L)]
    public void Factorial_ValidInputs_ReturnsExpected(int n, long expected)
    {
        Assert.That(_calculator.Factorial(n), Is.EqualTo(expected));
    }

    [Test]
    public void TriangleArea_ValidInputs_ReturnsArea()
    {
        Assert.That(_calculator.TriangleArea(3, 4), Is.EqualTo(6).Within(1e-9));
        Assert.That(_calculator.TriangleArea(0, 5), Is.EqualTo(0).Within(1e-9));
    }

    [Test]
    public void TriangleArea_NegativeDimensions_ThrowsException()
    {
        Assert.That(() => _calculator.TriangleArea(-1, 5), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void CircleArea_ValidInputs_ReturnsArea()
    {
        Assert.That(_calculator.CircleArea(1), Is.EqualTo(Math.PI).Within(1e-9));
        Assert.That(_calculator.CircleArea(0), Is.EqualTo(0).Within(1e-9));
    }

    [Test]
    public void CircleArea_NegativeRadius_ThrowsException()
    {
        Assert.That(() => _calculator.CircleArea(-1), Throws.TypeOf<ArgumentOutOfRangeException>());
    }
    [TestCase(5, 5, 120)]
    [TestCase(5, 4, 120)]
    [TestCase(5, 3, 60)]
    [TestCase(5, 0, 1)]
    [TestCase(0, 0, 1)]
    public void UnknownFunctionA_ValidInputs_ReturnsExpected(int n, int r, long expected)
    {
        Assert.That(_calculator.UnknownFunctionA(n, r), Is.EqualTo(expected));
    }

    [TestCase(5, 5, 1)]
    [TestCase(5, 4, 5)]
    [TestCase(5, 3, 10)]
    [TestCase(5, 0, 1)]
    [TestCase(0, 0, 1)]
    public void UnknownFunctionB_ValidInputs_ReturnsExpected(int n, int r, long expected)
    {
        Assert.That(_calculator.UnknownFunctionB(n, r), Is.EqualTo(expected));
    }

    [TestCase(1, 11, 7)]
    [TestCase(10, 11, 11)]
    [TestCase(11, 11, 15)]
    [TestCase(1, 1, 5)]     // 01 then 01 -> 0101
    [TestCase(1, 0, 4)]     // 01 then 00 -> 0100
    [TestCase(0, 1, 1)]     // 00 then 01 -> 0001
    [TestCase(10, 10, 10)]  // 10 then 10 -> 1010
    [TestCase(11, 0, 12)]   // 11 then 00 -> 1100
    public void Add_BothOperandsAreTwoBitFields_PacksThemIntoFourBits(double a, double b, double expected)
    {
        Assert.That(_calculator.Add(a, b), Is.EqualTo(expected));
    }

    [TestCase(1, 2, 3)]         // 2 is not a valid field
    [TestCase(10, 20, 30)]
    [TestCase(1, 5, 6)]
    [TestCase(100, 1, 101)]     // 100 is too wide for a 2-bit field
    [TestCase(-1, 1, 0)]
    [TestCase(1.5, 1, 2.5)]
    public void Add_OtherInputs_StillAddsNormally(double a, double b, double expected)
    {
        Assert.That(_calculator.Add(a, b), Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(1000, 4, 250)]
    [TestCase(720, 1, 720)]     // one failure: MTBF equals operating time
    [TestCase(100, 8, 12.5)]
    [TestCase(0.5, 2, 0.25)]
    public void Mtbf_ValidInputs_ReturnsOperatingTimePerFailure(double operatingTime, int failures, double expected)
    {
        Assert.That(_calculator.Mtbf(operatingTime, failures), Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(0, 4)]
    [TestCase(-1, 4)]
    [TestCase(double.NaN, 4)]
    [TestCase(double.PositiveInfinity, 4)]
    [TestCase(1000, 0)]
    [TestCase(1000, -1)]
    public void Mtbf_InvalidInputs_ThrowsArgumentOutOfRangeException(double operatingTime, int failures)
    {
        Assert.That(() => _calculator.Mtbf(operatingTime, failures), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCase(90, 10, 0.9)]
    [TestCase(200, 50, 0.8)]
    [TestCase(50, 50, 0.5)]
    [TestCase(99, 1, 0.99)]
    [TestCase(100, 0, 1)]       // instant repair -> always available
    [TestCase(0, 100, 0)]       // never runs -> never available
    public void Availability_ValidInputs_ReturnsRatio(double mtbf, double mttr, double expected)
    {
        Assert.That(_calculator.Availability(mtbf, mttr), Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(-1, 10)]
    [TestCase(10, -1)]
    [TestCase(double.NaN, 10)]
    [TestCase(10, double.PositiveInfinity)]
    public void Availability_InvalidInputs_ThrowsArgumentOutOfRangeException(double mtbf, double mttr)
    {
        Assert.That(() => _calculator.Availability(mtbf, mttr), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Availability_BothZero_ThrowsArgumentException()
    {
        // Each value is individually allowed, but 0/(0+0) is undefined.
        Assert.That(() => _calculator.Availability(0, 0), Throws.TypeOf<ArgumentException>());
    }

    // ---- Basic Musa reliability-growth model ----

    public static IEnumerable<object[]> InvalidBasicMusaInputs => new[]
    {
        new object[] { 0.0, 100.0, 10.0 },                       // lambda0 must be > 0
        new object[] { -1.0, 100.0, 10.0 },
        new object[] { double.NaN, 100.0, 10.0 },
        new object[] { 10.0, 0.0, 10.0 },                        // nu0 must be > 0
        new object[] { 10.0, -5.0, 10.0 },
        new object[] { 10.0, double.NaN, 10.0 },
        new object[] { 10.0, 100.0, -1.0 },                      // tau must be >= 0
        new object[] { 10.0, 100.0, double.NaN },
        new object[] { 10.0, 100.0, double.PositiveInfinity },
    };

    [TestCase(10, 100, 0, 10)]                          // tau = 0: intensity equals lambda0
    [TestCase(0.5, 20, 0, 0.5)]
    [TestCase(10, 100, 5, 6.065306597126334)]
    [TestCase(10, 100, 10, 3.6787944117144233)]         // 10 * e^-1
    [TestCase(10, 100, 100, 0.00045399929762484856)]    // 10 * e^-10
    public void BasicMusaFailureIntensity_ValidInputs_ReturnsExpected(double l0, double v0, double tau, double expected)
    {
        Assert.That(_calculator.BasicMusaFailureIntensity(l0, v0, tau), Is.EqualTo(expected).Within(1e-9));
    }

    [TestCase(10, 100, 0, 0)]                           // tau = 0: no failures found yet
    [TestCase(0.5, 20, 0, 0)]
    [TestCase(10, 100, 5, 39.346934028736655)]
    [TestCase(10, 100, 10, 63.212055882855765)]         // 100 * (1 - e^-1)
    [TestCase(10, 100, 1000, 100)]                      // long run approaches nu0
    public void BasicMusaExpectedFailures_ValidInputs_ReturnsExpected(double l0, double v0, double tau, double expected)
    {
        Assert.That(_calculator.BasicMusaExpectedFailures(l0, v0, tau), Is.EqualTo(expected).Within(1e-9));
    }

    // The model's defining relationship: lambda(tau) = lambda0 * (1 - mu(tau) / nu0)
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(50)]
    public void BasicMusa_IntensityFallsLinearlyWithCumulativeFailures(double tau)
    {
        double intensity = _calculator.BasicMusaFailureIntensity(10, 100, tau);
        double failures = _calculator.BasicMusaExpectedFailures(10, 100, tau);

        Assert.That(intensity, Is.EqualTo(10 * (1 - failures / 100)).Within(1e-9));
    }

    [TestCaseSource(nameof(InvalidBasicMusaInputs))]
    public void BasicMusaFailureIntensity_InvalidInputs_ThrowsArgumentOutOfRangeException(double l0, double v0, double tau)
    {
        Assert.That(() => _calculator.BasicMusaFailureIntensity(l0, v0, tau), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    [TestCaseSource(nameof(InvalidBasicMusaInputs))]
    public void BasicMusaExpectedFailures_InvalidInputs_ThrowsArgumentOutOfRangeException(double l0, double v0, double tau)
    {
        Assert.That(() => _calculator.BasicMusaExpectedFailures(l0, v0, tau), Throws.TypeOf<ArgumentOutOfRangeException>());
    }

}