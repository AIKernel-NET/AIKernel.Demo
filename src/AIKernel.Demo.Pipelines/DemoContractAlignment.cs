namespace AIKernel.Demo.Pipelines;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
/// <param name="RoutingProviderId">EN:  JA: RoutingProviderId パラメーターです。
/// [EN] The demo value supplied for <c>RoutingProviderId</c>.
/// [JA] <c>RoutingProviderId</c> として渡される Demo 値です。
/// </param>
/// <param name="RoutingModelId">EN:  JA: RoutingModelId パラメーターです。
/// [EN] The demo value supplied for <c>RoutingModelId</c>.
/// [JA] <c>RoutingModelId</c> として渡される Demo 値です。
/// </param>
/// <param name="DslRootType">EN:  JA: DslRootType パラメーターです。
/// [EN] The demo value supplied for <c>DslRootType</c>.
/// [JA] <c>DslRootType</c> として渡される Demo 値です。
/// </param>
/// <param name="DslFirstStepName">EN:  JA: DslFirstStepName パラメーターです。
/// [EN] The demo value supplied for <c>DslFirstStepName</c>.
/// [JA] <c>DslFirstStepName</c> として渡される Demo 値です。
/// </param>
public sealed record DemoContractAlignment(
    string RoutingProviderId,
    string RoutingModelId,
    string DslRootType,
    string DslFirstStepName);
