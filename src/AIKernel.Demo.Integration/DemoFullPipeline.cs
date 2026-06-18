using AIKernel.Common.Results;
using AIKernel.Demo.Dsl;
using AIKernel.Demo.Execution;
using AIKernel.Demo.PDP;
using AIKernel.Demo.Replay;
using AIKernel.Demo.Routing;
using AIKernel.Demo.Semantics;

namespace AIKernel.Demo.Integration;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public static class DemoFullPipeline
{
    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="dslText">EN:  JA: dslText パラメーターです。
    /// [EN] The demo value supplied for <c>dslText</c>.
    /// [JA] <c>dslText</c> として渡される Demo 値です。
    /// </param>
    /// <param name="input">EN:  JA: input パラメーターです。
    /// [EN] The demo value supplied for <c>input</c>.
    /// [JA] <c>input</c> として渡される Demo 値です。
    /// </param>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
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

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
/// <param name="NodeCount">EN:  JA: NodeCount パラメーターです。
/// [EN] The demo value supplied for <c>NodeCount</c>.
/// [JA] <c>NodeCount</c> として渡される Demo 値です。
/// </param>
/// <param name="SemanticDeltas">EN:  JA: SemanticDeltas パラメーターです。
/// [EN] The demo value supplied for <c>SemanticDeltas</c>.
/// [JA] <c>SemanticDeltas</c> として渡される Demo 値です。
/// </param>
/// <param name="ReplayLog">EN:  JA: ReplayLog パラメーターです。
/// [EN] The demo value supplied for <c>ReplayLog</c>.
/// [JA] <c>ReplayLog</c> として渡される Demo 値です。
/// </param>
/// <param name="FinalReplayHash">EN:  JA: FinalReplayHash パラメーターです。
/// [EN] The demo value supplied for <c>FinalReplayHash</c>.
/// [JA] <c>FinalReplayHash</c> として渡される Demo 値です。
/// </param>
/// <param name="ProviderId">EN:  JA: ProviderId パラメーターです。
/// [EN] The demo value supplied for <c>ProviderId</c>.
/// [JA] <c>ProviderId</c> として渡される Demo 値です。
/// </param>
/// <param name="PolicyAllowed">EN:  JA: PolicyAllowed パラメーターです。
/// [EN] The demo value supplied for <c>PolicyAllowed</c>.
/// [JA] <c>PolicyAllowed</c> として渡される Demo 値です。
/// </param>
public sealed record DemoFullPipelineResult(
    int NodeCount,
    IReadOnlyList<DemoSemanticDelta> SemanticDeltas,
    DemoReplayLog ReplayLog,
    string FinalReplayHash,
    string ProviderId,
    bool PolicyAllowed);
