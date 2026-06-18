using AIKernel.Demo.PDP;

namespace AIKernel.Demo.Pipelines;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
/// <param name="PipelineId">EN:  JA: PipelineId パラメーターです。
/// [EN] The demo value supplied for <c>PipelineId</c>.
/// [JA] <c>PipelineId</c> として渡される Demo 値です。
/// </param>
/// <param name="Decision">EN:  JA: Decision パラメーターです。
/// [EN] The demo value supplied for <c>Decision</c>.
/// [JA] <c>Decision</c> として渡される Demo 値です。
/// </param>
/// <param name="ReplayHash">EN:  JA: ReplayHash パラメーターです。
/// [EN] The demo value supplied for <c>ReplayHash</c>.
/// [JA] <c>ReplayHash</c> として渡される Demo 値です。
/// </param>
/// <param name="StepCount">EN:  JA: StepCount パラメーターです。
/// [EN] The demo value supplied for <c>StepCount</c>.
/// [JA] <c>StepCount</c> として渡される Demo 値です。
/// </param>
/// <param name="ContractAlignment">EN:  JA: ContractAlignment パラメーターです。
/// [EN] The demo value supplied for <c>ContractAlignment</c>.
/// [JA] <c>ContractAlignment</c> として渡される Demo 値です。
/// </param>
public sealed record DemoPipelineRun(
    string PipelineId,
    DemoPolicyCode Decision,
    string ReplayHash,
    int StepCount,
    DemoContractAlignment ContractAlignment);
