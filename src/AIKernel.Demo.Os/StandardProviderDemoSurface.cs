using CoreProcessState = AIKernel.Abstractions.Processes.ProcessState;

namespace AIKernel.Demo.Os;

/// <summary>
/// [EN] Demonstrates how application code consumes AIKernel standard OS driver providers.
/// [JA] application code が AIKernel 標準 OS driver Provider を消費する方法を示します。
/// </summary>
/// <remarks>
/// [EN] The demo uses the provider package directly so readers can see the boundary between Demo code and reusable provider implementations.
/// [JA] Demo は Provider package を直接使用し、Demo code と再利用可能な Provider 実装の境界を読み手に見える形で示します。
/// </remarks>
public static class StandardProviderDemoSurface
{
    /// <summary>
    /// [EN] Runs a deterministic standard-provider demo covering compute, process supervision, and scheduling.
    /// [JA] compute、process supervision、scheduling を扱う決定論的な standard-provider Demo を実行します。
    /// </summary>
    /// <returns>
    /// [EN] A compact snapshot of observable provider behavior.
    /// [JA] 観測可能な Provider 動作をまとめた小さな snapshot です。
    /// </returns>
    public static async Task<StandardProviderDemoResult> RunAsync()
        => await new StandardProviderDemoService().RunAsync().ConfigureAwait(false);
}

/// <summary>
/// [EN] Result snapshot for the standard-provider demo.
/// [JA] standard-provider Demo の result snapshot です。
/// </summary>
/// <param name="ComputeProviderId">
/// [EN] The provider id used by the CPU compute demo.
/// [JA] CPU compute Demo で使用した Provider id です。
/// </param>
/// <param name="VectorAdd">
/// [EN] Deterministic vector-add output.
/// [JA] 決定論的な vector-add 出力です。
/// </param>
/// <param name="ProcessId">
/// [EN] Created logical process id.
/// [JA] 作成された logical process id です。
/// </param>
/// <param name="ProcessState">
/// [EN] Final logical process state.
/// [JA] 最終的な logical process state です。
/// </param>
/// <param name="ScheduledJobName">
/// [EN] Safe scheduled command example.
/// [JA] 安全な scheduled command example です。
/// </param>
/// <param name="ScheduledInterval">
/// [EN] Scheduled interval used by the demo.
/// [JA] Demo で使用した scheduled interval です。
/// </param>
public sealed record StandardProviderDemoResult(
    string ComputeProviderId,
    IReadOnlyList<float> VectorAdd,
    string ProcessId,
    CoreProcessState ProcessState,
    string ScheduledJobName,
    TimeSpan ScheduledInterval);
