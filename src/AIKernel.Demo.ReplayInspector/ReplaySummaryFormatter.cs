namespace AIKernel.Demo.ReplayInspector;

public static class ReplaySummaryFormatter
{
    public static string Format(
        string? replayHash,
        int stepCount)
    {
        if (string.IsNullOrEmpty(replayHash))
            return "steps=0; hash=invalid";

        var shortHash = replayHash.Length > 8
            ? replayHash[..8]
            : replayHash;

        return $"steps={stepCount}; hash={shortHash}";
    }
}
