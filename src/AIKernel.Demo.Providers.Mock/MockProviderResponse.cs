namespace AIKernel.Demo.Providers.Mock;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
/// <param name="OutputHash">
/// [EN] The demo value supplied for <c>OutputHash</c>.
/// [JA] <c>OutputHash</c> として渡される Demo 値です。
/// </param>
public sealed record MockProviderResponse(
    string OutputHash);
