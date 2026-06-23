using AIKernel.Demo.Wasm;
using AIKernel.Dtos.Gpu;

/// <summary>
/// [EN] Console entry point for the canonical GPU rev3 teaching demo.
/// [JA] canonical GPU rev3 教材デモの console entry point です。
/// </summary>
/// <remarks>
/// [EN] The runner calls the Wasm demo surface because browser WebGPU, Dawn, and CUDA providers share the same pass vocabulary. Automated runs use deterministic CPU fallback while keeping the canonical DTO path visible.
/// [JA] browser WebGPU、Dawn、CUDA Provider が同じ pass vocabulary を共有するため、この runner は Wasm demo surface を呼び出します。自動実行では deterministic CPU fallback を使いつつ、canonical DTO path を見える形に保ちます。
/// </remarks>
public static class Program
{
    /// <summary>
    /// [EN] Runs the GPU rev3 demo and writes a deterministic summary.
    /// [JA] GPU rev3 demo を実行し、決定論的 summary を出力します。
    /// </summary>
    public static async Task Main()
    {
        var result = await WasmDemoSurface.RunGpuRev3PipelineAsync().ConfigureAwait(false);

        Console.WriteLine("AIKernel.Demo.Gpu");
        Console.WriteLine($"provider={result.ProviderId}");
        Console.WriteLine($"backend={result.Backend}; cpuFallback={result.UsingCpuFallback}");
        Console.WriteLine($"passes={string.Join(',', result.PassIds)}");
        Console.WriteLine($"rawAisthesisOnly={result.RawAisthesisOnly}");
        Console.WriteLine($"hudCompositeOffscreen={result.HudCompositeOffscreen}");
        Console.WriteLine($"sensorReadback={result.SensorReadbackRequired}; hudReadback={result.HudReadbackRequired}");
        Console.WriteLine($"featureStride={result.FeatureVector.Count}; spatialStride={result.SpatialVector.Count}");
        Console.WriteLine($"matrixOrder={result.Metadata[GpuProviderMetadataKeys.AisMatrixOrder]}");
        Console.WriteLine($"rawCapture={result.Metadata[GpuProviderMetadataKeys.RawCaptureSource]}");
        Console.WriteLine($"passBridge={result.Metadata[GpuProviderMetadataKeys.PassBridge]}");
    }
}
