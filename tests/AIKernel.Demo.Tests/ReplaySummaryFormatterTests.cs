using AIKernel.Demo.ReplayInspector;

namespace AIKernel.Demo.Tests;

public sealed class ReplaySummaryFormatterTests
{
    [Fact]
    public void FormatFailsClosedForNullReplayHash()
    {
        var summary = ReplaySummaryFormatter.Format(null, 4);

        Assert.Equal("steps=0; hash=invalid", summary);
    }

    [Fact]
    public void FormatShowsShortReplayHash()
    {
        var summary = ReplaySummaryFormatter.Format("1234567890abcdef", 4);

        Assert.Equal("steps=4; hash=12345678", summary);
    }

    [Fact]
    public void FormatFailsClosedForEmptyReplayHash()
    {
        var summary = ReplaySummaryFormatter.Format(string.Empty, 4);

        Assert.Equal("steps=0; hash=invalid", summary);
    }

    [Fact]
    public void FormatKeepsShortReplayHashAsIs()
    {
        var summary = ReplaySummaryFormatter.Format("abc", 2);

        Assert.Equal("steps=2; hash=abc", summary);
    }
}
