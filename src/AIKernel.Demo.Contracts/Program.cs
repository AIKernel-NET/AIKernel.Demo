using AIKernel.Demo.Contracts;

/// <summary>
/// [EN] Console entry point for the Contracts and DTO teaching demo.
/// [JA] Contracts と DTO 教材デモの console entry point です。
/// </summary>
/// <remarks>
/// [EN] The entry point delegates to <see cref="ContractsDemo"/> so the executable remains a thin runner and the readable lesson stays in the demo class.
/// [JA] この entry point は <see cref="ContractsDemo"/> に委譲し、実行ファイルを薄い runner に保ち、読みやすい教材を demo class 側に置きます。
/// </remarks>
public static class Program
{
    /// <summary>
    /// [EN] Runs the contracts demo and writes the boundary-object log.
    /// [JA] contracts demo を実行し、boundary object のログを書き込みます。
    /// </summary>
    public static void Main()
    {
        foreach (var line in ContractsDemo.Run())
        {
            Console.WriteLine(line);
        }
    }
}
