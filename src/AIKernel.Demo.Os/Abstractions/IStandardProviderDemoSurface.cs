namespace AIKernel.Demo.Os.Abstractions;

/// <summary>
/// [EN] Stable demo contract for standard OS provider examples.
/// [JA] standard OS Provider example 向けの安定した Demo contract です。
/// </summary>
public interface IStandardProviderDemoSurface
{
    /// <summary>
    /// [EN] Runs the standard-provider demo.
    /// [JA] standard-provider Demo を実行します。
    /// </summary>
    /// <returns>
    /// [EN] Standard-provider demo result.
    /// [JA] standard-provider Demo result です。
    /// </returns>
    Task<StandardProviderDemoResult> RunAsync();
}
