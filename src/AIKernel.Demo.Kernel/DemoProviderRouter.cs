using AIKernel.Demo.Routing;

namespace AIKernel.Demo.Kernel;

public sealed class DemoProviderRouter
{
    public DemoRoutingDecision Route()
        => new(
            ProviderId: "mock",
            ModelId: "demo",
            RouteReason: "demo-routing");
}
