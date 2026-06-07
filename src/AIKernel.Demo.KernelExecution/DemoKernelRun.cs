using AIKernel.Demo.Execution;
using AIKernel.Demo.Replay;
using AIKernel.Demo.Semantics;
using System.Collections.Immutable;

namespace AIKernel.Demo.KernelExecution;

public sealed record DemoKernelRun(
    DemoExecutionGraph ExecutionGraph,
    IReadOnlyList<DemoSemanticDelta> SemanticDeltas,
    DemoReplayLog ReplayLog,
    string FinalHash)
{
    public string? ProviderId { get; init; }
    public string? ModelId { get; init; }
    public string? RouteReason { get; init; }
    public ImmutableArray<string> ProviderOutputs { get; init; } = [];
}
