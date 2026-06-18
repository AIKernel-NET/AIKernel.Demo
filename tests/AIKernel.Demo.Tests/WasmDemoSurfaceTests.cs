using AIKernel.Abstractions.Processes;
using AIKernel.Demo.Wasm;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Verifies WASM demo coverage as executable documentation.
/// [JA] WASM Demo coverage を実行可能な文書として検証します。
/// </summary>
public sealed class WasmDemoSurfaceTests
{
    /// <summary>
    /// [EN] Confirms that the demo uses the WASM process provider through the Core process contract.
    /// [JA] Demo が Core process contract を通じて WASM process Provider を使用することを確認します。
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
    public async Task RunProcessLifecycleAsyncCoversWasmProcessProvider()
    {
        var result = await WasmDemoSurface.RunProcessLifecycleAsync();

        Assert.Equal("demo.wasm.process", result.ProcessName);
        Assert.Equal(ProcessState.Running, result.RunningState);
        Assert.Equal(ProcessState.Stopped, result.FinalState);
        Assert.False(string.IsNullOrWhiteSpace(result.ProcessId));
    }

    /// <summary>
    /// [EN] Confirms that the demo exercises WebGPU compute through deterministic CPU fallback.
    /// [JA] Demo が deterministic CPU fallback を通じて WebGPU compute を実行することを確認します。
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
    public async Task RunWebGpuFallbackVectorAddAsyncCoversComputeFallback()
    {
        var result = await WasmDemoSurface.RunWebGpuFallbackVectorAddAsync();

        Assert.Equal("webgpu.compute", result.ProviderId);
        Assert.True(result.UsingCpuFallback);
        Assert.Equal([11.0f, 22.0f, 33.0f, 44.0f], result.VectorAdd);
    }
}
