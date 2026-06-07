namespace AIKernel.Demo.Replay;

public sealed record DemoReplayEntry(
    string StepName,
    string DeltaSummary,
    string EntryHash,
    string ChainHash,
    string Timestamp)
{
    public string Hash => ChainHash;
}
