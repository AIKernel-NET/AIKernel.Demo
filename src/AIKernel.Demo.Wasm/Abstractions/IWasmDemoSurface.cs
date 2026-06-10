namespace AIKernel.Demo.Wasm.Abstractions;

/// <summary>
/// [EN] Stable demo contract for WASM runtime and WebGPU examples.
/// [JA] WASM runtime と WebGPU example 向けの安定した Demo contract です。
/// </summary>
public interface IWasmDemoSurface
{
    /// <summary>
    /// [EN] Runs a WASM process lifecycle demo.
    /// [JA] WASM process lifecycle Demo を実行します。
    /// </summary>
    /// <returns>
    /// [EN] WASM process result.
    /// [JA] WASM process result です。
    /// </returns>
    Task<WasmProcessDemoResult> RunProcessLifecycleAsync();

    /// <summary>
    /// [EN] Runs a WebGPU fallback vector-add demo.
    /// [JA] WebGPU fallback vector-add Demo を実行します。
    /// </summary>
    /// <returns>
    /// [EN] WebGPU fallback result.
    /// [JA] WebGPU fallback result です。
    /// </returns>
    Task<WebGpuFallbackDemoResult> RunWebGpuFallbackVectorAddAsync();
}
