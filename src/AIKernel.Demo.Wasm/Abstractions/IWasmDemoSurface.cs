namespace AIKernel.Demo.Wasm.Abstractions;

/// <summary>
/// [EN] Stable demo contract for WASM runtime and WebGPU examples.
/// [JA] WASM runtime と WebGPU example 向けの安定した Demo contract です。
/// </summary>
/// <remarks>
/// [EN] The interface lets tests, documentation, and sample code share the same teaching boundary: process lifecycle and WebGPU fallback are separate concepts even when one service implements both.
/// [JA] この interface により、test、documentation、sample code が同じ教材境界を共有できます。process lifecycle と WebGPU fallback は 1 つの service が実装していても別概念として扱います。
/// </remarks>
public interface IWasmDemoSurface
{
    /// <summary>
    /// [EN] Runs a WASM process lifecycle demo.
    /// [JA] WASM process lifecycle Demo を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] Use this member when teaching how a WASM module is represented as a Core process.
    /// [JA] WASM module が Core process として表現される方法を説明するときにこの member を使います。
    /// </remarks>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] WASM process result.
    /// [JA] WASM process result です。
    /// </returns>
    Task<WasmProcessDemoResult> RunProcessLifecycleAsync();

    /// <summary>
    /// [EN] Runs a WebGPU fallback vector-add demo.
    /// [JA] WebGPU fallback vector-add Demo を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] Use this member when teaching compute-provider fallback behavior without requiring a real browser GPU.
    /// [JA] 実 browser GPU を要求せず compute-provider fallback behavior を説明するときにこの member を使います。
    /// </remarks>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] WebGPU fallback result.
    /// [JA] WebGPU fallback result です。
    /// </returns>
    Task<WebGpuFallbackDemoResult> RunWebGpuFallbackVectorAddAsync();

    /// <summary>
    /// [EN] Runs the canonical v0.1.3 GPU rev3 HUD, Aisthesis, and spatial-reasoning DTO path.
    /// [JA] canonical v0.1.3 GPU rev3 HUD、Aisthesis、spatial-reasoning DTO path を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] Use this member when teaching that browser WebGPU, native Dawn, and CUDA providers share one DTO/pass vocabulary even when the demo uses deterministic CPU fallback.
    /// [JA] demo が deterministic CPU fallback を使う場合でも、browser WebGPU、native Dawn、CUDA Provider が 1 つの DTO/pass vocabulary を共有することを説明するときにこの member を使います。
    /// </remarks>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] Canonical GPU rev3 demo result.
    /// [JA] canonical GPU rev3 demo result です。
    /// </returns>
    Task<GpuRev3PipelineDemoResult> RunGpuRev3PipelineAsync();
}
