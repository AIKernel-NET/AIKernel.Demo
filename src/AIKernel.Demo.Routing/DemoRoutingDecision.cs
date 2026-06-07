namespace AIKernel.Demo.Routing;

public sealed record DemoRoutingDecision(
    string ProviderId,
    string ModelId,
    string RouteReason);
