using AIKernel.Common.Results;
using AIKernel.Demo.Dsl;
using AIKernel.Demo.Execution;
using AIKernel.Demo.Hosting;
using AIKernel.Demo.KernelExecution;

namespace AIKernel.Demo.Integration;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public static class DemoKernelStartupPipeline
{
    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="pipelineId">
    /// [EN] The demo value supplied for <c>pipelineId</c>.
    /// [JA] <c>pipelineId</c> として渡される Demo 値です。
    /// </param>
    /// <param name="dslText">
    /// [EN] The demo value supplied for <c>dslText</c>.
    /// [JA] <c>dslText</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
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

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
/// <param name="PipelineId">
/// [EN] The demo value supplied for <c>PipelineId</c>.
/// [JA] <c>PipelineId</c> として渡される Demo 値です。
/// </param>
/// <param name="ExecutionGraph">
/// [EN] The demo value supplied for <c>ExecutionGraph</c>.
/// [JA] <c>ExecutionGraph</c> として渡される Demo 値です。
/// </param>
/// <param name="Steps">
/// [EN] The demo value supplied for <c>Steps</c>.
/// [JA] <c>Steps</c> として渡される Demo 値です。
/// </param>
/// <param name="SemanticDeltas">
/// [EN] The demo value supplied for <c>SemanticDeltas</c>.
/// [JA] <c>SemanticDeltas</c> として渡される Demo 値です。
/// </param>
/// <param name="ReplayLog">
/// [EN] The demo value supplied for <c>ReplayLog</c>.
/// [JA] <c>ReplayLog</c> として渡される Demo 値です。
/// </param>
/// <param name="FinalHash">
/// [EN] The demo value supplied for <c>FinalHash</c>.
/// [JA] <c>FinalHash</c> として渡される Demo 値です。
/// </param>
public sealed record DemoKernelStartupPipelineResult(
    string PipelineId,
    DemoExecutionGraph ExecutionGraph,
    IReadOnlyList<string> Steps,
    IReadOnlyList<AIKernel.Demo.Semantics.DemoSemanticDelta> SemanticDeltas,
    AIKernel.Demo.Replay.DemoReplayLog ReplayLog,
    string FinalHash);

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public static class DemoFullKernelStartup
{
    /// <summary>
    /// [EN] Executes a deterministic operation on the demo contract surface.
    /// [JA] Demo 契約サーフェス上で決定論的な操作を実行します。
    /// </summary>
    /// <param name="pipelineId">
    /// [EN] The demo value supplied for <c>pipelineId</c>.
    /// [JA] <c>pipelineId</c> として渡される Demo 値です。
    /// </param>
    /// <param name="dslText">
    /// [EN] The demo value supplied for <c>dslText</c>.
    /// [JA] <c>dslText</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
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

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
/// <param name="PipelineId">
/// [EN] The demo value supplied for <c>PipelineId</c>.
/// [JA] <c>PipelineId</c> として渡される Demo 値です。
/// </param>
/// <param name="ExecutionGraph">
/// [EN] The demo value supplied for <c>ExecutionGraph</c>.
/// [JA] <c>ExecutionGraph</c> として渡される Demo 値です。
/// </param>
/// <param name="SemanticDeltas">
/// [EN] The demo value supplied for <c>SemanticDeltas</c>.
/// [JA] <c>SemanticDeltas</c> として渡される Demo 値です。
/// </param>
/// <param name="ReplayLog">
/// [EN] The demo value supplied for <c>ReplayLog</c>.
/// [JA] <c>ReplayLog</c> として渡される Demo 値です。
/// </param>
/// <param name="FinalHash">
/// [EN] The demo value supplied for <c>FinalHash</c>.
/// [JA] <c>FinalHash</c> として渡される Demo 値です。
/// </param>
public sealed record DemoFullKernelStartupResult(
    string PipelineId,
    DemoExecutionGraph ExecutionGraph,
    IReadOnlyList<AIKernel.Demo.Semantics.DemoSemanticDelta> SemanticDeltas,
    AIKernel.Demo.Replay.DemoReplayLog ReplayLog,
    string FinalHash)
{
    /// <summary>
    /// [EN] Gets the deterministic demo value for <c>Steps</c>.
    /// [JA] <c>Steps</c> に対応する決定論的な Demo 値を取得します。
    /// </summary>
    public IReadOnlyList<string> Steps { get; init; } = [];

    /// <summary>
    /// [EN] Executes a deterministic operation on the demo contract surface.
    /// [JA] Demo 契約サーフェス上で決定論的な操作を実行します。
    /// </summary>
    /// <param name="pipelineId">
    /// [EN] The demo value supplied for <c>pipelineId</c>.
    /// [JA] <c>pipelineId</c> として渡される Demo 値です。
    /// </param>
    /// <param name="executionGraph">
    /// [EN] The demo value supplied for <c>executionGraph</c>.
    /// [JA] <c>executionGraph</c> として渡される Demo 値です。
    /// </param>
    /// <param name="steps">
    /// [EN] The demo value supplied for <c>steps</c>.
    /// [JA] <c>steps</c> として渡される Demo 値です。
    /// </param>
    /// <param name="semanticDeltas">
    /// [EN] The demo value supplied for <c>semanticDeltas</c>.
    /// [JA] <c>semanticDeltas</c> として渡される Demo 値です。
    /// </param>
    /// <param name="replayLog">
    /// [EN] The demo value supplied for <c>replayLog</c>.
    /// [JA] <c>replayLog</c> として渡される Demo 値です。
    /// </param>
    /// <param name="finalHash">
    /// [EN] The demo value supplied for <c>finalHash</c>.
    /// [JA] <c>finalHash</c> として渡される Demo 値です。
    /// </param>
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
