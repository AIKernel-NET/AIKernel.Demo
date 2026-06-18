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
public sealed class DemoFullKernelStartupTests
{
    private const string DefaultDsl = """
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
    /// <summary>
    /// [EN] Verifies the documented Demo behavior for this deterministic test case.
    /// [JA] この決定論的なテストケースで文書化された Demo の振る舞いを検証します。
    /// </summary>
    /// <returns>
    /// [EN] A task that completes after the deterministic test assertion finishes.
    /// [JA] 決定論的なテスト検証が完了したときに完了するタスクです。
    /// </returns>
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

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    /// <summary>
    /// [EN] Verifies the documented Demo behavior for this deterministic test case.
    /// [JA] この決定論的なテストケースで文書化された Demo の振る舞いを検証します。
    /// </summary>
    /// <returns>
    /// [EN] A task that completes after the deterministic test assertion finishes.
    /// [JA] 決定論的なテスト検証が完了したときに完了するタスクです。
    /// </returns>
    public async Task FullKernelStartupFinalHashIsDeterministic()
    {
        var first = await DemoFullKernelStartup.RunAsync("demo.pipeline.kernel", DefaultDsl);
        var second = await DemoFullKernelStartup.RunAsync("demo.pipeline.kernel", DefaultDsl);

        Assert.True(first.IsSuccessState);
        Assert.True(second.IsSuccessState);
        Assert.Equal(first.Value.FinalHash, second.Value.FinalHash);
        Assert.Equal(first.Value.ReplayLog.Entries, second.Value.ReplayLog.Entries);
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
    /// <summary>
    /// [EN] Verifies the documented Demo behavior for this deterministic test case.
    /// [JA] この決定論的なテストケースで文書化された Demo の振る舞いを検証します。
    /// </summary>
    /// <returns>
    /// [EN] A task that completes after the deterministic test assertion finishes.
    /// [JA] 決定論的なテスト検証が完了したときに完了するタスクです。
    /// </returns>
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
