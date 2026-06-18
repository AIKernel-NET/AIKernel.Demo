using AIKernel.Demo.Hosting;
using AIKernel.Demo.Kernel;
using AIKernel.Demo.KernelExecution;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoHostingTests
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
    /// <summary>
    /// [EN] Verifies the documented Demo behavior for this deterministic test case.
    /// [JA] この決定論的なテストケースで文書化された Demo の振る舞いを検証します。
    /// </summary>
    public void KernelHostBuildsSuccessfully()
    {
        using var host = DemoKernelHost.Build(new DemoKernelModule(), new DemoProviderModule());

        Assert.NotNull(host);
        Assert.NotNull(host.Get<DemoTaskManager>());
        Assert.NotNull(host.Get<DemoExecutionEngine>());
        Assert.NotNull(host.Get<DemoProviderRouter>());
        Assert.NotNull(host.Get<DemoLlmController>());
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
    public void KernelCanBeRetrieved()
    {
        using var host = DemoKernelHost.Build(new DemoKernelModule(), new DemoProviderModule());

        var kernel = host.GetKernel();

        Assert.NotNull(kernel);
        Assert.IsType<DemoKernel>(kernel);
        Assert.Same(kernel, host.Get<DemoKernel>());
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
    public async Task PipelineExecutesDeterministically()
    {
        var first = await DemoHostingBootstrap.ExecuteSimplePipelineAsync(Dsl);
        var second = await DemoHostingBootstrap.ExecuteSimplePipelineAsync(Dsl);

        Assert.True(first.IsSuccessState);
        Assert.True(second.IsSuccessState);
        Assert.Equal(first.Value.FinalHash, second.Value.FinalHash);
        Assert.Equal(4, first.Value.SemanticDeltas.Count);
    }
}
