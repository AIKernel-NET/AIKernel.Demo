using AIKernel.Common.Results;
using AIKernel.Demo.Dsl;
using AIKernel.Demo.Execution;
using AIKernel.Demo.Hosting;
using AIKernel.Demo.KernelExecution;

namespace AIKernel.Demo.Integration;

public static class DemoKernelStartupPipeline
{
    public static Task<Result<DemoKernelStartupPipelineResult>> RunAsync(
        string pipelineId,
        string dslText)
        =>
            from parsed in DslParserDemo.Parse(dslText).AsTask()
            from graph in DslToGraphDemo.Convert(parsed).AsTask()
            from run in DemoHostingBootstrap.ExecuteSimplePipelineAsync(dslText)
            select BuildResult(pipelineId, graph, run);

    private static DemoKernelStartupPipelineResult BuildResult(
        string pipelineId,
        DemoExecutionGraph graph,
        DemoKernelRun run)
        => new(
            PipelineId: pipelineId,
            ExecutionGraph: graph,
            Steps: graph.Nodes.Select(node => node.StepName).ToArray(),
            SemanticDeltas: run.SemanticDeltas,
            ReplayLog: run.ReplayLog,
            FinalHash: run.FinalHash);

}

public sealed record DemoKernelStartupPipelineResult(
    string PipelineId,
    DemoExecutionGraph ExecutionGraph,
    IReadOnlyList<string> Steps,
    IReadOnlyList<AIKernel.Demo.Semantics.DemoSemanticDelta> SemanticDeltas,
    AIKernel.Demo.Replay.DemoReplayLog ReplayLog,
    string FinalHash);

public static class DemoFullKernelStartup
{
    public static async Task<Result<DemoFullKernelStartupResult>> RunAsync(
        string pipelineId,
        string dslText)
    {
        var result = await DemoKernelStartupPipeline.RunAsync(pipelineId, dslText).ConfigureAwait(false);
        return result.Map(value => new DemoFullKernelStartupResult(
            value.PipelineId,
            value.ExecutionGraph,
            value.Steps,
            value.SemanticDeltas,
            value.ReplayLog,
            value.FinalHash));
    }
}

public sealed record DemoFullKernelStartupResult(
    string PipelineId,
    DemoExecutionGraph ExecutionGraph,
    IReadOnlyList<AIKernel.Demo.Semantics.DemoSemanticDelta> SemanticDeltas,
    AIKernel.Demo.Replay.DemoReplayLog ReplayLog,
    string FinalHash)
{
    public IReadOnlyList<string> Steps { get; init; } = [];

    public DemoFullKernelStartupResult(
        string pipelineId,
        DemoExecutionGraph executionGraph,
        IReadOnlyList<string> steps,
        IReadOnlyList<AIKernel.Demo.Semantics.DemoSemanticDelta> semanticDeltas,
        AIKernel.Demo.Replay.DemoReplayLog replayLog,
        string finalHash)
        : this(pipelineId, executionGraph, semanticDeltas, replayLog, finalHash)
        => Steps = steps;
}
