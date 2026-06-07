namespace AIKernel.Demo.PDP;

public sealed record DemoPolicyDecision(
    bool Allowed,
    string Code,
    string Reason)
{
    public static DemoPolicyDecision Allow(
        string reason)
        => new(true, "ALLOW", reason);

    public static DemoPolicyDecision Deny(
        string reason)
        => new(false, "DENY", reason);
}
