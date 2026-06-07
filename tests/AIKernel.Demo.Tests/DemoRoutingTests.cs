using AIKernel.Demo.Routing;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoRoutingTests
{
    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public void DifferentInputsProduceDifferentRoutingDecisions()
    {
        var small = DemoRoutingPolicy.Route(12, ["local", "remote"]);
        var large = DemoRoutingPolicy.Route(128, ["local", "remote"]);

        Assert.Equal("demo.local", small.ProviderId);
        Assert.Equal("demo.remote", large.ProviderId);
        Assert.NotEqual(small.RouteReason, large.RouteReason);
        Assert.Matches(@"^[a-z0-9-]+$", small.RouteReason);
        Assert.Matches(@"^[a-z0-9-]+$", large.RouteReason);
    }

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public void RouteFallsBackWithAuditableReasonWhenNoTierMatches()
    {
        var decision = DemoRoutingPolicy.Route(12, ["archive"]);

        Assert.Equal("demo.mock", decision.ProviderId);
        Assert.Equal("mock-fixed", decision.ModelId);
        Assert.Equal("no-matching-provider-tier", decision.RouteReason);
    }
}
