using Reqnroll;
using SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

namespace SOFTEST_INTRO_Calculator.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class UsingCalculatorBasicReliabilitySteps
{
    private readonly CalculatorContext _context;
    private readonly MusaContext _musa;

    public UsingCalculatorBasicReliabilitySteps(CalculatorContext context, MusaContext musa)
    {
        _context = context;
        _musa = musa;
    }

    [Given("the initial failure intensity is {double} failures per hour")]
    public void GivenTheInitialFailureIntensityIs(double failuresPerHour)
        => _musa.InitialFailureIntensity = failuresPerHour;

    [Given("the expected total number of failures is {double}")]
    public void GivenTheExpectedTotalNumberOfFailuresIs(double failures)
        => _musa.TotalExpectedFailures = failures;

    [Given("the accumulated execution time is {double} hours")]
    public void GivenTheAccumulatedExecutionTimeIs(double hours)
        => _musa.ExecutionTime = hours;

    [When("I calculate the current failure intensity")]
    public void WhenICalculateTheCurrentFailureIntensity()
        => _context.Calculate(() => _context.Calculator.BasicMusaFailureIntensity(
            _musa.InitialFailureIntensity, _musa.TotalExpectedFailures, _musa.ExecutionTime));

    [When("I calculate the expected cumulative failures")]
    public void WhenICalculateTheExpectedCumulativeFailures()
        => _context.Calculate(() => _context.Calculator.BasicMusaExpectedFailures(
            _musa.InitialFailureIntensity, _musa.TotalExpectedFailures, _musa.ExecutionTime));

    private void Calculate() { }
}