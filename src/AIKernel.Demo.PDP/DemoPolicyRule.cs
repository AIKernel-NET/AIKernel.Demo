namespace AIKernel.Demo.PDP;

public sealed record DemoPolicyRule(
    int MaxInputLength,
    string DenyReason)
{
    public DemoPolicyDecision Evaluate(
        string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        return input.Length > MaxInputLength
            ? DemoPolicyDecision.Deny(DenyReason)
            : DemoPolicyDecision.Allow("within-limit");
    }
}
