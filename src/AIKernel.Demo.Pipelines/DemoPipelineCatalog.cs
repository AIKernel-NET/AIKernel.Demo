using AIKernel.Demo.PDP;
using AIKernel.Demo.Providers.Mock;
using AIKernel.Dtos.Dsl;
using AIKernel.Dtos.Routing;

namespace AIKernel.Demo.Pipelines;

public static class DemoPipelineCatalog
{
    public static DemoPipelineRun CreateDefaultRun()
    {
        var decision = DemoPolicyDecision.Allow("demo-readonly");
        var provider = new MockProvider();
        var response = provider.Generate("hello-rom");
        var routing = new KernelProviderRoutingDecision(
            "demo.mock",
            "mock-fixed",
            ProviderTier: "mock",
            RouteReason: "demo-contract-alignment");
        var document = new DslDocument(new PipelineRootNode(
        [
            new StepNode("normalize"),
            new StepNode("structure"),
            new StepNode("provider"),
            new StepNode("polish")
        ]));

        return new DemoPipelineRun(
            PipelineId: "demo.pipeline.default",
            Decision: decision.Code,
            ReplayHash: response.OutputHash,
            StepCount: 3,
            ContractAlignment: new DemoContractAlignment(
                routing.ProviderId,
                routing.RequestedModelId,
                document.Root.Type,
                ((PipelineRootNode)document.Root).Steps[0] is StepNode first
                    ? first.Name
                    : "unknown"));
    }
}
