using AIKernel.Demo.PDP;
using AIKernel.Dtos.Dsl;
using AIKernel.Dtos.Routing;

namespace AIKernel.Demo.Pipelines;

public static class DemoPipelineCatalog
{
    public static DemoPipelineRun CreateDefaultRun()
    {
        var decision = DemoPolicyDecision.Allow("demo-readonly");
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
        var pipeline = (PipelineRootNode)document.Root;

        return new DemoPipelineRun(
            PipelineId: "demo.pipeline.default",
            Decision: decision.Code,
            ReplayHash: "demo-replay-hash",
            StepCount: pipeline.Steps.Count,
            ContractAlignment: new DemoContractAlignment(
                routing.ProviderId,
                routing.RequestedModelId,
                document.Root.Type,
                ((StepNode)pipeline.Steps[0]).Name));
    }
}
