using AIKernel.Demo.Execution;
using AIKernel.Demo.KernelExecution;
using AIKernel.Demo.Semantics;

namespace AIKernel.Demo.Tests;

public sealed class DemoKernelExecutionTests
{
    [Fact]
    public void ContextUpdatesCorrectly()
    {
        var context = new DemoExecutionContext();

        context = context.ApplyDelta(new DemoSemanticDelta("normalize", "pending", "completed"));

        Assert.Equal("completed", context.OrchestrationState["normalize"]);
        Assert.Equal("normalize", context.ExpressionState["last_step"]);
        Assert.Equal("pending->completed", context.MaterialState["normalize.transition"]);
    }

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

    [Fact]
    public void FinalHashChangesWhenAnyDeltaChanges()
    {
        var engine = new DemoExecutionEngine();
        var first = engine.Execute(DemoExecutionGraph.FromSteps(["normalize", "provider"]));
        var second = engine.Execute(DemoExecutionGraph.FromSteps(["normalize", "polish"]));

        Assert.NotEqual(first.FinalHash, second.FinalHash);
    }
}
