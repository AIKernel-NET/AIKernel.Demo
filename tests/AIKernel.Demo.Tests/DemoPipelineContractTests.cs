using AIKernel.Demo.Pipelines;
using System.Text.RegularExpressions;

namespace AIKernel.Demo.Tests;

public sealed class DemoPipelineContractTests
{
    private static readonly Regex IdentifierPattern =
        new(@"^[a-z0-9]+(\.[a-z0-9]+)*$", RegexOptions.CultureInvariant);

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

    [Fact]
    public void ContractAlignmentUsesOnlyPublicDemoContractDto()
    {
        var run = DemoPipelineCatalog.CreateDefaultRun();

        Assert.IsAssignableFrom<DemoContractAlignment>(run.ContractAlignment);
        Assert.True(run.ContractAlignment.GetType().IsPublic);
        Assert.Equal(typeof(DemoContractAlignment), run.ContractAlignment.GetType());
    }

    [Fact]
    public void PipelineIdFollowsCanonicalFormat()
    {
        var run = DemoPipelineCatalog.CreateDefaultRun();

        Assert.Matches(IdentifierPattern, run.PipelineId);
    }

    [Fact]
    public void ContractAlignmentFieldsAreNonNull()
    {
        var run = DemoPipelineCatalog.CreateDefaultRun();

        Assert.NotNull(run.ContractAlignment.RoutingProviderId);
        Assert.NotNull(run.ContractAlignment.RoutingModelId);
        Assert.NotNull(run.ContractAlignment.DslRootType);
        Assert.NotNull(run.ContractAlignment.DslFirstStepName);
    }

    [Fact]
    public void DemoPipelineCatalogReturnsContractDto()
    {
        var run = DemoPipelineCatalog.CreateDefaultRun();

        Assert.IsAssignableFrom<DemoPipelineRun>(run);
        Assert.True(run.GetType().IsPublic);
        Assert.Equal(typeof(DemoPipelineRun), run.GetType());
    }
}
