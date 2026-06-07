using AIKernel.Common.Results;
using AIKernel.Demo.Dsl;
using AIKernel.Demo.Execution;
using AIKernel.Demo.PDP;
using AIKernel.Demo.Replay;
using AIKernel.Demo.Routing;
using AIKernel.Demo.Semantics;

namespace AIKernel.Demo.Integration;

public static class DemoFullPipeline
{
    public static Result<DemoFullPipelineResult> Run(
        string dslText,
        string input)
        =>
            from document in DslParserDemo.Parse(dslText)
            from graph in DslToGraphDemo.Convert(document)
            let deltas = BuildDeltas(graph)
            let replay = DemoReplayLog.FromDeltas(deltas)
            let routing = DemoRoutingPolicy.Route(input.Length, ["local", "remote"])
            let policy = new DemoPolicyEngine([new DemoPolicyRule(128, "input-too-large")]).Evaluate(input)
            select new DemoFullPipelineResult(
                NodeCount: graph.Nodes.Count,
                SemanticDeltas: deltas,
                ReplayLog: replay,
                FinalReplayHash: replay.FinalHash,
                ProviderId: routing.ProviderId,
                PolicyAllowed: policy.Allowed);

    private static DemoSemanticDelta[] BuildDeltas(
        DemoExecutionGraph graph)
        => graph.Nodes
            .Select(node => new DemoSemanticDelta(node.StepName, "pending", "completed"))
            .ToArray();
}

public sealed record DemoFullPipelineResult(
    int NodeCount,
    IReadOnlyList<DemoSemanticDelta> SemanticDeltas,
    DemoReplayLog ReplayLog,
    string FinalReplayHash,
    string ProviderId,
    bool PolicyAllowed);
