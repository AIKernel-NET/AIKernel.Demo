using AIKernel.Demo.Providers.Mock;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class MockProviderTests
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
    public void GenerateReturnsDeterministicSha256Hash()
    {
        var provider = new MockProvider();

        var first = provider.Generate("hello-rom");
        var second = provider.Generate("hello-rom");

        Assert.Equal(first, second);
        Assert.Equal(
            "F12749ED10DFAF5A3CFACC44A2F444EF9E1C1312631108447C94C82B16CCF9AD",
            first.OutputHash);
        Assert.Matches("^[A-F0-9]{64}$", first.OutputHash);
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
    public void GenerateRejectsNullInput()
    {
        var provider = new MockProvider();

        Assert.Throws<ArgumentNullException>(() => provider.Generate(null!));
    }
}
