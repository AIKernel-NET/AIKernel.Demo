using AIKernel.Abstractions.Processes;
using AIKernel.Demo.Os;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Verifies standard-provider demo coverage as executable documentation.
/// [JA] standard-provider Demo coverage を実行可能な文書として検証します。
/// </summary>
public sealed class StandardProviderDemoSurfaceTests
{
    /// <summary>
    /// [EN] Confirms that the demo consumes standard providers through the package boundary.
    /// [JA] Demo が package 境界を通じて standard provider を消費することを確認します。
    /// </summary>
    [Fact]
    /// <summary>
    /// [EN] Verifies the documented Demo behavior for this deterministic test case.
    /// [JA] この決定論的なテストケースで文書化された Demo の振る舞いを検証します。
    /// </summary>
    /// <returns>
    /// [EN] A task that completes after the deterministic test assertion finishes.
    /// [JA] 決定論的なテスト検証が完了したときに完了するタスクです。
    /// </returns>
    public async Task RunAsyncCoversStandardProviderBoundaries()
    {
        var result = await StandardProviderDemoSurface.RunAsync();

        Assert.Equal("providers.compute.cpu", result.ComputeProviderId);
        Assert.Equal([5.0f, 7.0f, 9.0f], result.VectorAdd);
        Assert.Equal(ProcessState.Stopped, result.ProcessState);
        Assert.Equal("aik system info", result.ScheduledJobName);
        Assert.Equal(TimeSpan.FromMinutes(1), result.ScheduledInterval);
        Assert.False(string.IsNullOrWhiteSpace(result.ProcessId));
    }
}
