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

    /// <summary>
    /// [EN] Runs the canonical GPU rev3 teaching path for HUD, Aisthesis, and spatial reasoning.
    /// [JA] HUD、Aisthesis、spatial reasoning 用の canonical GPU rev3 teaching path を実行します。
    /// </summary>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] A deterministic snapshot of the shared GPU rev3 pass vocabulary.
    /// [JA] shared GPU rev3 pass vocabulary の決定論的 snapshot です。
    /// </returns>
    public static async Task<GpuRev3PipelineDemoResult> RunGpuRev3PipelineAsync()
        => await new WasmDemoService().RunGpuRev3PipelineAsync().ConfigureAwait(false);
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

/// <summary>
/// [EN] Result snapshot for the canonical GPU rev3 pipeline demo.
/// [JA] canonical GPU rev3 pipeline Demo の result snapshot です。
/// </summary>
/// <param name="ProviderId">EN:  JA: ProviderId パラメーターです。
/// [EN] Provider id used by the demo.
/// [JA] Demo が使用した Provider id です。
/// </param>
/// <param name="Backend">EN:  JA: Backend パラメーターです。
/// [EN] Active backend label.
/// [JA] active backend label です。
/// </param>
/// <param name="UsingCpuFallback">EN:  JA: UsingCpuFallback パラメーターです。
/// [EN] Whether deterministic CPU fallback was used.
/// [JA] deterministic CPU fallback が使用されたかどうかです。
/// </param>
/// <param name="PassIds">EN:  JA: PassIds パラメーターです。
/// [EN] Canonical rev3 pass ids exercised by the demo.
/// [JA] Demo が実行した canonical rev3 pass id です。
/// </param>
/// <param name="RawAisthesisOnly">EN:  JA: RawAisthesisOnly パラメーターです。
/// [EN] True when Aisthesis was bound to the raw framebuffer only.
/// [JA] Aisthesis が raw framebuffer のみに bind された場合 true です。
/// </param>
/// <param name="HudCompositeOffscreen">EN:  JA: HudCompositeOffscreen パラメーターです。
/// [EN] True when HUD composition returned an offscreen target.
/// [JA] HUD composition が offscreen target を返した場合 true です。
/// </param>
/// <param name="SensorReadbackRequired">EN:  JA: SensorReadbackRequired パラメーターです。
/// [EN] True when sensor diagnostics identify fallback readback.
/// [JA] sensor diagnostics が fallback readback を示す場合 true です。
/// </param>
/// <param name="HudReadbackRequired">EN:  JA: HudReadbackRequired パラメーターです。
/// [EN] True when HUD diagnostics identify fallback readback.
/// [JA] HUD diagnostics が fallback readback を示す場合 true です。
/// </param>
/// <param name="FeatureVector">EN:  JA: FeatureVector パラメーターです。
/// [EN] Canonical Aisthesis feature vector.
/// [JA] canonical Aisthesis feature vector です。
/// </param>
/// <param name="SpatialVector">EN:  JA: SpatialVector パラメーターです。
/// [EN] Canonical spatial reasoning vector.
/// [JA] canonical spatial reasoning vector です。
/// </param>
/// <param name="Metadata">EN:  JA: Metadata パラメーターです。
/// [EN] Provider rev3 metadata used by the demo.
/// [JA] Demo が使用した Provider rev3 metadata です。
/// </param>
public sealed record GpuRev3PipelineDemoResult(
    string ProviderId,
    string Backend,
    bool UsingCpuFallback,
    IReadOnlyList<string> PassIds,
    bool RawAisthesisOnly,
    bool HudCompositeOffscreen,
    bool SensorReadbackRequired,
    bool HudReadbackRequired,
    IReadOnlyList<float> FeatureVector,
    IReadOnlyList<float> SpatialVector,
    IReadOnlyDictionary<string, string> Metadata);
