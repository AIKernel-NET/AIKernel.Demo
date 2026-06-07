using AIKernel.Demo.Execution;
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
public sealed class DemoKernelTests
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

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
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

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public async Task KernelReplayHashIsDeterministic()
    {
        var kernel = CreateKernel();
        var graph = DemoExecutionGraph.FromSteps(["normalize", "provider"]);

        var first = await kernel.ExecuteAsync(graph);
        var second = await kernel.ExecuteAsync(graph);

        Assert.Equal(first.FinalHash, second.FinalHash);
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
