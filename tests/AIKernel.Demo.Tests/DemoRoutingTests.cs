using AIKernel.Demo.Routing;

namespace AIKernel.Demo.Tests;

public sealed class DemoRoutingTests
{
    [Fact]
    public void DifferentInputsProduceDifferentRoutingDecisions()
    {
        var small = DemoRoutingPolicy.Route(12, ["local", "remote"]);
        var large = DemoRoutingPolicy.Route(128, ["local", "remote"]);

        Assert.Equal("demo.local", small.ProviderId);
        Assert.Equal("demo.remote", large.ProviderId);
        Assert.NotEqual(small.RouteReason, large.RouteReason);
        Assert.Matches(@"^[a-z0-9-]+$", small.RouteReason);
        Assert.Matches(@"^[a-z0-9-]+$", large.RouteReason);
    }

    [Fact]
    public void RouteFallsBackWithAuditableReasonWhenNoTierMatches()
    {
        var decision = DemoRoutingPolicy.Route(12, ["archive"]);

        Assert.Equal("demo.mock", decision.ProviderId);
        Assert.Equal("mock-fixed", decision.ModelId);
        Assert.Equal("no-matching-provider-tier", decision.RouteReason);
    }
}
