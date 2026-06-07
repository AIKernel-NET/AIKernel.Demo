using AIKernel.Demo.Replay;
using AIKernel.Demo.Semantics;

namespace AIKernel.Demo.Tests;

public sealed class DemoReplayTests
{
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

    [Fact]
    public void CanonicalPayloadNormalizesWhitespace()
    {
        var first = DemoHashChain.CanonicalizePayload(" normalize ", "raw\r\n->trimmed ", " T+0000 ");
        var second = DemoHashChain.CanonicalizePayload("normalize", "raw\n->trimmed", "T+0000");

        Assert.Equal(first, second);
    }
}
