using AIKernel.Demo.Pipelines;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoPipelineMonadTests
{
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
    public async Task RunAsyncComposesPipelineWithLinqQuerySyntax()
    {
        var result = await DemoPipelineMonad.RunAsync(" hello ");

        Assert.True(result.IsSuccessState);
        Assert.Equal("executed(compiled(hello))", result.Value);
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
    public async Task RunAsyncNormalizesInputBeforeCompileAndExecute()
    {
        var result = await DemoPipelineMonad.RunAsync("  hello  ");

        Assert.True(result.IsSuccessState);
        Assert.Equal("executed(compiled(hello))", result.Value);
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
    public async Task RunAsyncFailsClosedWhenInputIsNull()
    {
        var result = await DemoPipelineMonad.RunAsync(null!);

        Assert.False(result.IsSuccessState);
        Assert.Contains("Pipeline input is required.", result.Error.Message);
    }
}
