using AIKernel.Demo.Pipelines;

namespace AIKernel.Demo.Tests;

public sealed class DemoPipelineContractTests
{
    [Fact]
    public void DefaultPipelinePublishesRoutingAndDslContractAlignment()
    {
        var run = DemoPipelineCatalog.CreateDefaultRun();

        Assert.Equal("demo.pipeline.default", run.PipelineId);
        Assert.Equal("ALLOW", run.Decision);
        Assert.Equal("demo.mock", run.ContractAlignment.RoutingProviderId);
        Assert.Equal("mock-fixed", run.ContractAlignment.RoutingModelId);
        Assert.Equal("Pipeline", run.ContractAlignment.DslRootType);
        Assert.Equal("normalize", run.ContractAlignment.DslFirstStepName);
    }
}
