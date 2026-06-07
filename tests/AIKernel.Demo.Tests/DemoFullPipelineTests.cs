using AIKernel.Demo.Integration;

namespace AIKernel.Demo.Tests;

public sealed class DemoFullPipelineTests
{
    private const string Dsl = """
        normalize
        structure
        provider
        polish
        """;

    [Fact]
    public void FullPipelineRunsEndToEnd()
    {
        var result = DemoFullPipeline.Run(Dsl, "hello");

        Assert.True(result.IsSuccessState);
        Assert.Equal(4, result.Value.NodeCount);
        Assert.All(result.Value.SemanticDeltas, delta => Assert.Equal("pending", delta.Before));
        Assert.All(result.Value.SemanticDeltas, delta => Assert.Equal("completed", delta.After));
        Assert.Equal("demo.local", result.Value.ProviderId);
        Assert.True(result.Value.PolicyAllowed);
        Assert.False(string.IsNullOrWhiteSpace(result.Value.FinalReplayHash));
    }

    [Fact]
    public void FullPipelineReplayHashIsDeterministic()
    {
        var first = DemoFullPipeline.Run(Dsl, "hello");
        var second = DemoFullPipeline.Run(Dsl, "hello");

        Assert.True(first.IsSuccessState);
        Assert.True(second.IsSuccessState);
        Assert.Equal(first.Value.FinalReplayHash, second.Value.FinalReplayHash);
        Assert.Equal(first.Value.ReplayLog.Entries, second.Value.ReplayLog.Entries);
    }
}
