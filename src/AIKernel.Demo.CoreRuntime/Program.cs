using AIKernel.Demo.CoreRuntime;

/// <summary>
/// [EN] Console entry point for the Core runtime teaching demo.
/// [JA] Core runtime 教材デモの console entry point です。
/// </summary>
/// <remarks>
/// [EN] The entry point only prints the deterministic teaching log produced by <see cref="CoreRuntimeDemo"/> so readers can focus on the public Core surface.
/// [JA] この entry point は <see cref="CoreRuntimeDemo"/> が生成した決定論的教材ログを出力するだけにして、読者が public Core surface に集中できるようにします。
/// </remarks>
public static class Program
{
    /// <summary>
    /// [EN] Runs the demo and writes each explanatory log line to standard output.
    /// [JA] デモを実行し、説明用ログの各行を標準出力へ書き込みます。
    /// </summary>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] A task representing the asynchronous console run.
    /// [JA] 非同期 console 実行を表す task です。
    /// </returns>
    public static async Task Main()
    {
        foreach (var line in await CoreRuntimeDemo.RunAsync().ConfigureAwait(false))
        {
            Console.WriteLine(line);
        }
    }
}
