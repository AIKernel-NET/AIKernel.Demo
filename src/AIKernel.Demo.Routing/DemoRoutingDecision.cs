namespace AIKernel.Demo.Routing;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
/// <param name="ProviderId">EN:  JA: ProviderId パラメーターです。
/// [EN] The demo value supplied for <c>ProviderId</c>.
/// [JA] <c>ProviderId</c> として渡される Demo 値です。
/// </param>
/// <param name="ModelId">EN:  JA: ModelId パラメーターです。
/// [EN] The demo value supplied for <c>ModelId</c>.
/// [JA] <c>ModelId</c> として渡される Demo 値です。
/// </param>
/// <param name="RouteReason">EN:  JA: RouteReason パラメーターです。
/// [EN] The demo value supplied for <c>RouteReason</c>.
/// [JA] <c>RouteReason</c> として渡される Demo 値です。
/// </param>
public sealed record DemoRoutingDecision(
    string ProviderId,
    string ModelId,
    string RouteReason);
