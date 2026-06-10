using AIKernel.Demo.Execution;
using AIKernel.Demo.KernelExecution;
using AIKernel.Demo.Semantics;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoKernelExecutionTests
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
    public void ContextUpdatesCorrectly()
    {
        var context = new DemoExecutionContext();

        context = context.ApplyDelta(new DemoSemanticDelta("normalize", "pending", "completed"));

        Assert.Equal("completed", context.OrchestrationState["normalize"]);
        Assert.Equal("normalize", context.ExpressionState["last_step"]);
        Assert.Equal("pending->completed", context.MaterialState["normalize.transition"]);
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
    public void ContextTracksSequentialTransitions()
    {
        var context = new DemoExecutionContext()
            .ApplyDelta(new DemoSemanticDelta("normalize", "pending", "completed"))
            .ApplyDelta(new DemoSemanticDelta("provider", "pending", "completed"));

        Assert.Equal("completed", context.OrchestrationState["normalize"]);
        Assert.Equal("completed", context.OrchestrationState["provider"]);
        Assert.Equal("provider", context.ExpressionState["last_step"]);
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
    public void ReplayLogEntriesMatchDeltas()
    {
        var engine = new DemoExecutionEngine();
        var graph = DemoExecutionGraph.FromSteps(["normalize", "provider"]);

        var run = engine.Execute(graph);

        Assert.Equal(run.SemanticDeltas.Count, run.ReplayLog.Entries.Count);
        Assert.Equal(
            run.SemanticDeltas.Select(delta => delta.StepName),
            run.ReplayLog.Entries.Select(entry => entry.StepName));
        Assert.NotEqual(run.ReplayLog.Entries[0].Hash, run.ReplayLog.Entries[1].Hash);
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
    public void FinalHashChangesWhenAnyDeltaChanges()
    {
        var engine = new DemoExecutionEngine();
        var first = engine.Execute(DemoExecutionGraph.FromSteps(["normalize", "provider"]));
        var second = engine.Execute(DemoExecutionGraph.FromSteps(["normalize", "polish"]));

        Assert.NotEqual(first.FinalHash, second.FinalHash);
    }
}
