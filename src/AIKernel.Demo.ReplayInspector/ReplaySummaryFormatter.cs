namespace AIKernel.Demo.ReplayInspector;

public static class ReplaySummaryFormatter
{
    public static string Format(
        string replayHash,
        int stepCount)
        => $"steps={stepCount}; hash={replayHash}";
}
