using AIKernel.Demo.Replay;
using AIKernel.Demo.Semantics;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoReplayTests
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
    public void HashChainIsDeterministic()
    {
        var deltas = new[]
        {
            new DemoSemanticDelta("normalize", "raw", "trimmed"),
            new DemoSemanticDelta("provider", "pending", "completed")
        };

        var first = DemoReplayLog.FromDeltas(deltas);
        var second = DemoReplayLog.FromDeltas(deltas);

        Assert.Equal(first.FinalHash, second.FinalHash);
        Assert.Equal(first.Entries.Select(entry => entry.Hash), second.Entries.Select(entry => entry.Hash));
        Assert.Equal(first.Entries.Select(entry => entry.EntryHash), second.Entries.Select(entry => entry.EntryHash));
        Assert.Equal(first.Entries.Select(entry => entry.ChainHash), second.Entries.Select(entry => entry.ChainHash));
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
    public void ChangingOneDeltaChangesFinalHash()
    {
        var first = DemoReplayLog.FromDeltas(
        [
            new DemoSemanticDelta("normalize", "raw", "trimmed")
        ]);
        var second = DemoReplayLog.FromDeltas(
        [
            new DemoSemanticDelta("normalize", "raw", "changed")
        ]);

        Assert.NotEqual(first.FinalHash, second.FinalHash);
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
    public void ReplayEntriesSeparateEntryHashAndChainHash()
    {
        var log = DemoReplayLog.FromDeltas(
        [
            new DemoSemanticDelta("normalize", "raw", "trimmed")
        ]);

        var entry = Assert.Single(log.Entries);
        Assert.NotEqual(entry.EntryHash, entry.ChainHash);
        Assert.Equal(entry.ChainHash, entry.Hash);
        Assert.Equal("T+0000", entry.Timestamp);
        Assert.Matches(@"^T\+\d{4}$", entry.Timestamp);
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
    public void CanonicalPayloadNormalizesWhitespace()
    {
        var first = DemoHashChain.CanonicalizePayload(" normalize ", "raw\r\n->trimmed ", " T+0000 ");
        var second = DemoHashChain.CanonicalizePayload("normalize", "raw\n->trimmed", "T+0000");

        Assert.Equal(first, second);
    }
}
