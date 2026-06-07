namespace AIKernel.Demo.PDP;

public enum DemoPolicyCode
{
    Allow,
    Deny
}

public sealed record DemoPolicyDecision(
    bool Allowed,
    DemoPolicyCode Code,
    string Reason)
{
    public static DemoPolicyDecision Allow(
        string? reason)
        => new(true, DemoPolicyCode.Allow, reason ?? string.Empty);

    public static DemoPolicyDecision Deny(
        string? reason)
        => new(false, DemoPolicyCode.Deny, reason ?? string.Empty);
}
