namespace AIKernel.Demo.Pipelines;

public sealed record DemoContractAlignment(
    string RoutingProviderId,
    string RoutingModelId,
    string DslRootType,
    string DslFirstStepName);
