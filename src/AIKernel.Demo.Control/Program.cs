using AIKernel.Demo.Control;

/// <summary>
/// [EN] Console entry point for the Control governance teaching demo.
/// [JA] Control governance 教材デモの console entry point です。
/// </summary>
/// <remarks>
/// [EN] Keeping this runner thin makes the emulator flow in <see cref="ControlDemo"/> the primary code readers should study.
/// [JA] この runner を薄く保つことで、読者が主に読むべき emulator flow を <see cref="ControlDemo"/> に集約します。
/// </remarks>
public static class Program
{
    /// <summary>
    /// [EN] Runs the Control demo and writes each governance log line.
    /// [JA] Control demo を実行し、governance log の各行を書き込みます。
    /// </summary>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] A task representing the asynchronous console run.
    /// [JA] 非同期 console 実行を表す task です。
    /// </returns>
    public static async Task Main()
    {
        foreach (var line in await ControlDemo.RunAsync().ConfigureAwait(false))
        {
            Console.WriteLine(line);
        }
    }
}
