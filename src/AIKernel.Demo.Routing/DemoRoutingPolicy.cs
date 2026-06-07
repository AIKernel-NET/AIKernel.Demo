namespace AIKernel.Demo.Routing;

public static class DemoRoutingPolicy
{
    private const int LocalThreshold = 32;

    public static DemoRoutingDecision Route(
        int inputSize,
        IReadOnlyList<string> providerTiers)
    {
        ArgumentNullException.ThrowIfNull(providerTiers);

        var tiers = providerTiers.ToHashSet(StringComparer.Ordinal);
        var hasLocal = tiers.Contains("local");
        var hasRemote = tiers.Contains("remote");

        if (inputSize <= LocalThreshold && hasLocal)
        {
            return new DemoRoutingDecision(
                ProviderId: "demo.local",
                ModelId: "local-small",
                RouteReason: "small-input-local");
        }

        if (hasRemote)
        {
            return new DemoRoutingDecision(
                ProviderId: "demo.remote",
                ModelId: "remote-large",
                RouteReason: "large-input-remote");
        }

        return new DemoRoutingDecision(
            ProviderId: "demo.mock",
            ModelId: "mock-fixed",
            RouteReason: "no-matching-provider-tier");
    }
}
