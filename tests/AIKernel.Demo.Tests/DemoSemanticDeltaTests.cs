using AIKernel.Demo.Semantics;

namespace AIKernel.Demo.Tests;

public sealed class DemoSemanticDeltaTests
{
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

    [Fact]
    public void ApplyReturnsNewState()
    {
        var first = new DemoSemanticState();
        var second = first.Apply(new DemoSemanticDelta("normalize", "raw", "trimmed"));

        Assert.Empty(first.Values);
        Assert.Equal("trimmed", second.Values["normalize"]);
        Assert.NotSame(first.Values, second.Values);
    }

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
