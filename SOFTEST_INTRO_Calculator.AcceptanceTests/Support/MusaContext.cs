namespace SOFTEST_INTRO_Calculator.AcceptanceTests.Support;

public sealed class MusaContext
{
    public double InitialFailureIntensity { get; set; }   // lambda0, failures per hour
    public double TotalExpectedFailures { get; set; }     // nu0, failures
    public double ExecutionTime { get; set; }             // tau, hours
}