namespace AIKernel.Demo.Wasm;

using AIKernel.Abstractions.Processes;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public static class WasmDemoSurface
{
    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static string GetInitialRoute()
        => "/demo/pipeline";

    /// <summary>
    /// [EN] Runs the WASM process provider through the Core process abstraction.
    /// [JA] Core process abstraction を通じて WASM process Provider を実行します。
    /// </summary>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] A deterministic snapshot of the WASM process lifecycle.
    /// [JA] WASM process lifecycle の決定論的 snapshot です。
    /// </returns>
    public static async Task<WasmProcessDemoResult> RunProcessLifecycleAsync()
        => await new WasmDemoService().RunProcessLifecycleAsync().ConfigureAwait(false);

    /// <summary>
    /// [EN] Runs a WebGPU compute demo using the deterministic CPU fallback path.
    /// [JA] deterministic CPU fallback path を使って WebGPU compute Demo を実行します。
    /// </summary>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] A deterministic vector-add result produced through the WebGPU provider boundary.
    /// [JA] WebGPU Provider 境界を通じて生成された決定論的な vector-add result です。
    /// </returns>
    public static async Task<WebGpuFallbackDemoResult> RunWebGpuFallbackVectorAddAsync()
        => await new WasmDemoService().RunWebGpuFallbackVectorAddAsync().ConfigureAwait(false);
}

/// <summary>
/// [EN] Result snapshot for the WASM process lifecycle demo.
/// [JA] WASM process lifecycle Demo の result snapshot です。
/// </summary>
/// <param name="ProcessId">EN:  JA: ProcessId パラメーターです。
/// [EN] Created WASM process id.
/// [JA] 作成された WASM process id です。
/// </param>
/// <param name="ProcessName">EN:  JA: ProcessName パラメーターです。
/// [EN] Created WASM process name.
/// [JA] 作成された WASM process name です。
/// </param>
/// <param name="RunningState">EN:  JA: RunningState パラメーターです。
/// [EN] State observed after start.
/// [JA] start 後に観測された state です。
/// </param>
/// <param name="FinalState">EN:  JA: FinalState パラメーターです。
/// [EN] State observed after stop.
/// [JA] stop 後に観測された state です。
/// </param>
public sealed record WasmProcessDemoResult(
    string ProcessId,
    string ProcessName,
    ProcessState RunningState,
    ProcessState FinalState);

/// <summary>
/// [EN] Result snapshot for the WebGPU fallback vector-add demo.
/// [JA] WebGPU fallback vector-add Demo の result snapshot です。
/// </summary>
/// <param name="ProviderId">EN:  JA: ProviderId パラメーターです。
/// [EN] WebGPU provider id.
/// [JA] WebGPU Provider id です。
/// </param>
/// <param name="UsingCpuFallback">EN:  JA: UsingCpuFallback パラメーターです。
/// [EN] Whether the provider used CPU fallback.
/// [JA] Provider が CPU fallback を使用したかどうかです。
/// </param>
/// <param name="VectorAdd">EN:  JA: VectorAdd パラメーターです。
/// [EN] Deterministic vector-add output.
/// [JA] 決定論的な vector-add 出力です。
/// </param>
public sealed record WebGpuFallbackDemoResult(
    string ProviderId,
    bool UsingCpuFallback,
    IReadOnlyList<float> VectorAdd);
