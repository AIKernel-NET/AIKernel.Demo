using AIKernel.Abstractions.Processes;
using AIKernel.Demo.Wasm;
using AIKernel.Dtos.Gpu;
using AIKernel.Enums;

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

    /// <summary>
    /// [EN] Confirms that the demo exposes the canonical rev3 GPU HUD/Aisthesis path as executable documentation.
    /// [JA] Demo が canonical rev3 GPU HUD/Aisthesis path を実行可能な文書として公開することを確認します。
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
    public async Task RunGpuRev3PipelineAsyncCoversCanonicalHudAisthesisAndSpatialPasses()
    {
        var result = await WasmDemoSurface.RunGpuRev3PipelineAsync();

        Assert.Equal("webgpu.compute", result.ProviderId);
        Assert.Equal(GpuBackend.CpuFallback.ToString(), result.Backend);
        Assert.True(result.UsingCpuFallback);
        Assert.Equal(
            [GpuOperationNames.GpuAisthesisRawFrame, GpuOperationNames.GpuSpatialReasoning, GpuOperationNames.GpuHudComposite],
            result.PassIds);
        Assert.True(result.RawAisthesisOnly);
        Assert.True(result.HudCompositeOffscreen);
        Assert.True(result.SensorReadbackRequired);
        Assert.True(result.HudReadbackRequired);
        Assert.Equal(GpuCanonicalLayouts.FeatureVector.Stride, result.FeatureVector.Count);
        Assert.Equal(320.0f, result.FeatureVector[0]);
        Assert.Equal(200.0f, result.FeatureVector[1]);
        Assert.Equal(GpuCanonicalLayouts.SpatialVector.Stride, result.SpatialVector.Count);
        Assert.Equal("true", result.Metadata[GpuProviderMetadataKeys.Rev3]);
        Assert.Equal("topos,route,threat,zoe", result.Metadata[GpuProviderMetadataKeys.AisMatrixOrder]);
        Assert.Equal("raw-framebuffer", result.Metadata[GpuProviderMetadataKeys.RawCaptureSource]);
        Assert.Equal("optional-native-or-js", result.Metadata[GpuProviderMetadataKeys.PassBridge]);
    }
}
