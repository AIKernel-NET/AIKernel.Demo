using AIKernel.Demo.Execution;
using AIKernel.Demo.Replay;
using AIKernel.Demo.Semantics;

namespace AIKernel.Demo.KernelExecution;

public sealed class DemoExecutionEngine
{
    public DemoKernelRun Execute(
        DemoExecutionGraph graph)
    {
        ArgumentNullException.ThrowIfNull(graph);

        var context = new DemoExecutionContext();
        var deltas = new List<DemoSemanticDelta>();

        foreach (var node in graph.Traverse())
        {
            var delta = BuildDelta(node);
            context.ApplyDelta(delta);
            deltas.Add(delta);
        }

        var replayLog = DemoReplayLog.FromDeltas(deltas);

        return new DemoKernelRun(
            ExecutionGraph: graph,
            SemanticDeltas: deltas,
            ReplayLog: replayLog,
            FinalHash: replayLog.FinalHash);
    }

    private static DemoSemanticDelta BuildDelta(
        DemoExecutionNode node)
        => new(
            StepName: node.StepName,
            Before: "pending",
            After: $"completed:{node.Id}");
}
