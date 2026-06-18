using AIKernel.Demo.Execution;
using AIKernel.Demo.Replay;
using AIKernel.Demo.Semantics;
using System.Collections.Immutable;

namespace AIKernel.Demo.KernelExecution;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
/// <param name="ExecutionGraph">EN:  JA: ExecutionGraph パラメーターです。
/// [EN] The demo value supplied for <c>ExecutionGraph</c>.
/// [JA] <c>ExecutionGraph</c> として渡される Demo 値です。
/// </param>
/// <param name="SemanticDeltas">EN:  JA: SemanticDeltas パラメーターです。
/// [EN] The demo value supplied for <c>SemanticDeltas</c>.
/// [JA] <c>SemanticDeltas</c> として渡される Demo 値です。
/// </param>
/// <param name="ReplayLog">EN:  JA: ReplayLog パラメーターです。
/// [EN] The demo value supplied for <c>ReplayLog</c>.
/// [JA] <c>ReplayLog</c> として渡される Demo 値です。
/// </param>
/// <param name="FinalHash">EN:  JA: FinalHash パラメーターです。
/// [EN] The demo value supplied for <c>FinalHash</c>.
/// [JA] <c>FinalHash</c> として渡される Demo 値です。
/// </param>
public sealed record DemoKernelRun(
    DemoExecutionGraph ExecutionGraph,
    IReadOnlyList<DemoSemanticDelta> SemanticDeltas,
    DemoReplayLog ReplayLog,
    string FinalHash)
{
    /// <summary>
    /// [EN] Gets the deterministic demo value for <c>ProviderId</c>.
    /// [JA] <c>ProviderId</c> に対応する決定論的な Demo 値を取得します。
    /// </summary>
    public string? ProviderId { get; init; }
    /// <summary>
    /// [EN] Gets the deterministic demo value for <c>ModelId</c>.
    /// [JA] <c>ModelId</c> に対応する決定論的な Demo 値を取得します。
    /// </summary>
    public string? ModelId { get; init; }
    /// <summary>
    /// [EN] Gets the deterministic demo value for <c>RouteReason</c>.
    /// [JA] <c>RouteReason</c> に対応する決定論的な Demo 値を取得します。
    /// </summary>
    public string? RouteReason { get; init; }
    /// <summary>
    /// [EN] Gets the deterministic demo value for <c>ProviderOutputs</c>.
    /// [JA] <c>ProviderOutputs</c> に対応する決定論的な Demo 値を取得します。
    /// </summary>
    public ImmutableArray<string> ProviderOutputs { get; init; } = [];
}
