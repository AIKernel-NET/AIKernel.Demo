using AIKernel.Demo.Providers;

/// <summary>
/// [EN] Console entry point for the official Provider teaching demo.
/// [JA] 公式 Provider 教材デモの console entry point です。
/// </summary>
/// <remarks>
/// [EN] The runner performs no network work; it prints the dry-run provider descriptor log produced by <see cref="ProvidersDemo"/>.
/// [JA] この runner は network 処理を行わず、<see cref="ProvidersDemo"/> が生成する dry-run provider descriptor log を出力します。
/// </remarks>
public static class Program
{
    /// <summary>
    /// [EN] Runs the provider demo and writes each descriptor-oriented log line.
    /// [JA] provider demo を実行し、descriptor を中心にしたログの各行を書き込みます。
    /// </summary>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] A task representing the asynchronous console run.
    /// [JA] 非同期 console 実行を表す task です。
    /// </returns>
    public static async Task Main()
    {
        foreach (var line in await ProvidersDemo.RunAsync().ConfigureAwait(false))
        {
            Console.WriteLine(line);
        }
    }
}
