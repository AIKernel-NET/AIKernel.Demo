using AIKernel.Demo.Execution;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoExecutionGraphTests
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
    public void FromStepsBuildsLinearGraph()
    {
        var graph = DemoExecutionGraph.FromSteps(["normalize", "provider"]);

        Assert.Equal(2, graph.Nodes.Count);
        Assert.Equal("01-normalize", graph.Nodes[0].Id);
        Assert.Equal("02-provider", graph.Nodes[1].Id);
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
    public void TraverseReturnsNodesInOrder()
    {
        var graph = DemoExecutionGraph.FromSteps(["normalize", "structure", "provider"]);

        Assert.Equal(
            ["normalize", "structure", "provider"],
            graph.Traverse().Select(node => node.StepName));
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
    public void TraverseReturnsUnderlyingNodes()
    {
        var graph = DemoExecutionGraph.FromSteps(["normalize", "provider"]);

        Assert.Same(graph.Nodes, graph.Traverse());
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
    public void NodeIdsFollowCanonicalFormat()
    {
        var graph = DemoExecutionGraph.FromSteps(["normalize", "provider"]);

        Assert.Matches(@"^\d{2}-[a-z]+$", graph.Nodes[0].Id);
        Assert.Matches(@"^\d{2}-[a-z]+$", graph.Nodes[1].Id);
        Assert.Equal("01-normalize", graph.Nodes[0].Id);
        Assert.Equal("02-provider", graph.Nodes[1].Id);
    }
}
