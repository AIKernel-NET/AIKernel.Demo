using AIKernel.Demo.Integration;

namespace AIKernel.Demo.Tests;

public sealed class DemoFullKernelStartupTests
{
    private const string DefaultDsl = """
        normalize
        structure
        provider
        polish
        """;

    [Fact]
    public async Task FullKernelStartupRunsEndToEnd()
    {
        var result = await DemoFullKernelStartup.RunAsync("demo.pipeline.kernel", DefaultDsl);

        Assert.True(result.IsSuccessState);
        Assert.Equal("demo.pipeline.kernel", result.Value.PipelineId);
        Assert.Equal(["normalize", "structure", "provider", "polish"], result.Value.Steps);
        Assert.Equal(4, result.Value.SemanticDeltas.Count);
        Assert.All(result.Value.SemanticDeltas, delta => Assert.Equal("pending", delta.Before));
        Assert.All(result.Value.SemanticDeltas, delta => Assert.StartsWith("completed:", delta.After));
        Assert.Equal(4, result.Value.ReplayLog.Entries.Count);
        Assert.Equal(result.Value.ReplayLog.FinalHash, result.Value.FinalHash);
    }

    [Fact]
    public async Task FullKernelStartupFinalHashIsDeterministic()
    {
        var first = await DemoFullKernelStartup.RunAsync("demo.pipeline.kernel", DefaultDsl);
        var second = await DemoFullKernelStartup.RunAsync("demo.pipeline.kernel", DefaultDsl);

        Assert.True(first.IsSuccessState);
        Assert.True(second.IsSuccessState);
        Assert.Equal(first.Value.FinalHash, second.Value.FinalHash);
        Assert.Equal(first.Value.ReplayLog.Entries, second.Value.ReplayLog.Entries);
    }

    [Fact]
    public async Task ChangingDslChangesGraphAndHash()
    {
        var first = await DemoFullKernelStartup.RunAsync("demo.pipeline.kernel", """
            normalize
            provider
            """);
        var second = await DemoFullKernelStartup.RunAsync("demo.pipeline.kernel", """
            normalize
            structure
            provider
            """);

        Assert.True(first.IsSuccessState);
        Assert.True(second.IsSuccessState);
        Assert.NotEqual(first.Value.Steps, second.Value.Steps);
        Assert.NotEqual(first.Value.ExecutionGraph.Nodes.Count, second.Value.ExecutionGraph.Nodes.Count);
        Assert.NotEqual(first.Value.FinalHash, second.Value.FinalHash);
    }
}
