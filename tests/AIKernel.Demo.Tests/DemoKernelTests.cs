using AIKernel.Demo.Execution;
using AIKernel.Demo.Kernel;
using AIKernel.Demo.KernelExecution;

namespace AIKernel.Demo.Tests;

public sealed class DemoKernelTests
{
    [Fact]
    public async Task KernelExecutesAllNodesInOrder()
    {
        var kernel = CreateKernel();
        var graph = DemoExecutionGraph.FromSteps(["normalize", "structure", "provider"]);

        var run = await kernel.ExecuteAsync(graph);

        Assert.Equal(
            ["normalize", "structure", "provider"],
            run.SemanticDeltas.Select(delta => delta.StepName));
        Assert.Equal(
            ["llm-output(normalize)", "llm-output(structure)", "llm-output(provider)"],
            run.ProviderOutputs);
    }

    [Fact]
    public async Task KernelProducesExpectedSemanticDeltas()
    {
        var kernel = CreateKernel();
        var graph = DemoExecutionGraph.FromSteps(["normalize"]);

        var run = await kernel.ExecuteAsync(graph);

        var delta = Assert.Single(run.SemanticDeltas);
        Assert.Equal("normalize", delta.StepName);
        Assert.Equal("pending", delta.Before);
        Assert.Equal("completed:01-normalize", delta.After);
        Assert.Matches(@"^completed:\d{2}-normalize$", delta.After);
    }

    [Fact]
    public async Task KernelReplayHashIsDeterministic()
    {
        var kernel = CreateKernel();
        var graph = DemoExecutionGraph.FromSteps(["normalize", "provider"]);

        var first = await kernel.ExecuteAsync(graph);
        var second = await kernel.ExecuteAsync(graph);

        Assert.Equal(first.FinalHash, second.FinalHash);
    }

    [Fact]
    public async Task KernelKeepsRoutingAndProviderOutputs()
    {
        var kernel = CreateKernel();
        var graph = DemoExecutionGraph.FromSteps(["normalize", "provider"]);

        var run = await kernel.ExecuteAsync(graph);

        Assert.Equal("mock", run.ProviderId);
        Assert.Equal("demo", run.ModelId);
        Assert.Equal("demo-routing", run.RouteReason);
        Assert.Equal(
            ["llm-output(normalize)", "llm-output(provider)"],
            run.ProviderOutputs);
    }

    private static DemoKernel CreateKernel()
        => new(
            new DemoTaskManager(),
            new DemoProviderRouter(),
            new DemoLlmController(),
            new DemoExecutionEngine());
}
