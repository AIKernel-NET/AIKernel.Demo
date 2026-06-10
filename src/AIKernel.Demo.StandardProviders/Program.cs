using AIKernel.Demo.StandardProviders;

/// <summary>
/// [EN] Console entry point for the standard Provider teaching demo.
/// [JA] standard Provider 教材デモの console entry point です。
/// </summary>
/// <remarks>
/// [EN] The entry point keeps provider orchestration inside <see cref="StandardProvidersDemo"/> and only writes the resulting teaching log.
/// [JA] この entry point は provider orchestration を <see cref="StandardProvidersDemo"/> 側に保ち、結果の教材ログだけを書き込みます。
/// </remarks>
public static class Program
{
    /// <summary>
    /// [EN] Runs the standard provider demo and writes each OS-driver log line.
    /// [JA] standard provider demo を実行し、OS driver log の各行を書き込みます。
    /// </summary>
    /// <returns>
    /// [EN] A task representing the asynchronous console run.
    /// [JA] 非同期 console 実行を表す task です。
    /// </returns>
    public static async Task Main()
    {
        foreach (var line in await StandardProvidersDemo.RunAsync().ConfigureAwait(false))
        {
            Console.WriteLine(line);
        }
    }
}
