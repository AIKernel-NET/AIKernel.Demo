using AIKernel.Demo.Semantics;

namespace AIKernel.Demo.Replay;

public sealed record DemoReplayLog(
    IReadOnlyList<DemoReplayEntry> Entries,
    string FinalHash)
{
    public static DemoReplayLog FromDeltas(
        IEnumerable<DemoSemanticDelta> deltas)
    {
        ArgumentNullException.ThrowIfNull(deltas);

        var entries = new List<DemoReplayEntry>();
        var previousHash = DemoHashChain.GenesisHash;
        var index = 0;

        foreach (var delta in deltas)
        {
            var summary = $"{delta.Before}->{delta.After}";
            var timestamp = $"T+{index:0000}";
            var payload = DemoHashChain.CanonicalizePayload(delta.StepName, summary, timestamp);
            var entryHash = DemoHashChain.ComputeEntryHash(payload);
            var chainHash = DemoHashChain.ComputeNext(previousHash, entryHash);
            entries.Add(new DemoReplayEntry(delta.StepName, summary, entryHash, chainHash, timestamp));
            previousHash = chainHash;
            index++;
        }

        return new DemoReplayLog(entries, previousHash);
    }
}
