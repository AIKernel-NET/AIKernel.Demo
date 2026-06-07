using AIKernel.Demo.PDP;
using AIKernel.Dtos.Dsl;
using AIKernel.Dtos.Routing;

namespace AIKernel.Demo.Pipelines;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public static class DemoPipelineCatalog
{
    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static DemoPipelineRun CreateDefaultRun()
    {
        var decision = DemoPolicyDecision.Allow("demo-readonly");
        var routing = new KernelProviderRoutingDecision(
            "demo.mock",
            "mock-fixed",
            ProviderTier: "mock",
            RouteReason: "demo-contract-alignment");
        var document = new DslDocument(new PipelineRootNode(
        [
            new StepNode("normalize"),
            new StepNode("structure"),
            new StepNode("provider"),
            new StepNode("polish")
        ]));
        var pipeline = (PipelineRootNode)document.Root;

        return new DemoPipelineRun(
            PipelineId: "demo.pipeline.default",
            Decision: decision.Code,
            ReplayHash: "demo-replay-hash",
            StepCount: pipeline.Steps.Count,
            ContractAlignment: new DemoContractAlignment(
                routing.ProviderId,
                routing.RequestedModelId,
                document.Root.Type,
                ((StepNode)pipeline.Steps[0]).Name));
    }
}
