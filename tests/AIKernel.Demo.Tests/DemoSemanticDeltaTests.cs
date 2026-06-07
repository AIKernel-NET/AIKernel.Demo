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
public sealed class DemoSemanticDeltaTests
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
    public void ApplyingDeltasUpdatesState()
    {
        var state = DemoSemanticState.FromDeltas(
        [
            new DemoSemanticDelta("normalize", "raw", "trimmed"),
            new DemoSemanticDelta("provider", "pending", "completed")
        ]);

        Assert.Equal("trimmed", state.Values["normalize"]);
        Assert.Equal("completed", state.Values["provider"]);
        Assert.Equal(["normalize", "provider"], state.Values.Keys);
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
    public void ApplyReturnsNewState()
    {
        var first = new DemoSemanticState();
        var second = first.Apply(new DemoSemanticDelta("normalize", "raw", "trimmed"));

        Assert.Empty(first.Values);
        Assert.Equal("trimmed", second.Values["normalize"]);
        Assert.NotSame(first.Values, second.Values);
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
    public void ApplyingDeltasUpdatesSemanticSlots()
    {
        var state = DemoSemanticState.FromDeltas(
        [
            new DemoSemanticDelta("route", "pending", "selected", DemoSemanticSlot.Orchestration),
            new DemoSemanticDelta("prompt", "raw", "structured", DemoSemanticSlot.Expression),
            new DemoSemanticDelta("rom", "missing", "loaded", DemoSemanticSlot.Material)
        ]);

        Assert.Equal("selected", state.OrchestrationState["route"]);
        Assert.Equal("structured", state.ExpressionState["prompt"]);
        Assert.Equal("loaded", state.MaterialState["rom"]);
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
    public void SequenceOrderMatters()
    {
        var first = DemoSemanticState.FromDeltas(
        [
            new DemoSemanticDelta("provider", "pending", "first"),
            new DemoSemanticDelta("provider", "first", "second")
        ]);
        var second = DemoSemanticState.FromDeltas(
        [
            new DemoSemanticDelta("provider", "pending", "second"),
            new DemoSemanticDelta("provider", "second", "first")
        ]);

        Assert.Equal("second", first.Values["provider"]);
        Assert.Equal("first", second.Values["provider"]);
    }
}
