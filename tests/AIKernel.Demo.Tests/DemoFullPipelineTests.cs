using AIKernel.Demo.Integration;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoFullPipelineTests
{
    private const string Dsl = """
        normalize
        structure
        provider
        polish
        """;

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
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

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
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
