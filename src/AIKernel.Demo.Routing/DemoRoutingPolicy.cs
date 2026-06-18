namespace AIKernel.Demo.Routing;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public static class DemoRoutingPolicy
{
    private const int LocalThreshold = 32;

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="inputSize">EN:  JA: inputSize パラメーターです。
    /// [EN] The demo value supplied for <c>inputSize</c>.
    /// [JA] <c>inputSize</c> として渡される Demo 値です。
    /// </param>
    /// <param name="providerTiers">EN:  JA: providerTiers パラメーターです。
    /// [EN] The demo value supplied for <c>providerTiers</c>.
    /// [JA] <c>providerTiers</c> として渡される Demo 値です。
    /// </param>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static DemoRoutingDecision Route(
        int inputSize,
        IReadOnlyList<string> providerTiers)
    {
        ArgumentNullException.ThrowIfNull(providerTiers);

        var tiers = providerTiers.ToHashSet(StringComparer.Ordinal);
        var hasLocal = tiers.Contains("local");
        var hasRemote = tiers.Contains("remote");

        if (inputSize <= LocalThreshold && hasLocal)
        {
            return new DemoRoutingDecision(
                ProviderId: "demo.local",
                ModelId: "local-small",
                RouteReason: "small-input-local");
        }

        if (hasRemote)
        {
            return new DemoRoutingDecision(
                ProviderId: "demo.remote",
                ModelId: "remote-large",
                RouteReason: "large-input-remote");
        }

        return new DemoRoutingDecision(
            ProviderId: "demo.mock",
            ModelId: "mock-fixed",
            RouteReason: "no-matching-provider-tier");
    }
}
