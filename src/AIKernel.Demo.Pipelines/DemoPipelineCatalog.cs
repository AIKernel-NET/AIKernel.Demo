using AIKernel.Demo.PDP;
using AIKernel.Demo.Providers.Mock;

namespace AIKernel.Demo.Pipelines;

public static class DemoPipelineCatalog
{
    public static DemoPipelineRun CreateDefaultRun()
    {
        var decision = DemoPolicyDecision.Allow("demo-readonly");
        var provider = new MockProvider();
        var response = provider.Generate("hello-rom");

        return new DemoPipelineRun(
            PipelineId: "demo.pipeline.default",
            Decision: decision.Code,
            ReplayHash: response.OutputHash,
            StepCount: 3);
    }
}
