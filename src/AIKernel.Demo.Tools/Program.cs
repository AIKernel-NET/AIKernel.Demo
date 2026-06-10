using AIKernel.Demo.Tools;

/// <summary>
/// [EN] Console entry point for the Tools and instrumentation teaching demo.
/// [JA] Tools と instrumentation 教材デモの console entry point です。
/// </summary>
/// <remarks>
/// [EN] The runner delegates to <see cref="ToolsDemo"/> so operational evidence handling remains readable in one place.
/// [JA] この runner は <see cref="ToolsDemo"/> に委譲し、operational evidence handling を 1 か所で読みやすく保ちます。
/// </remarks>
public static class Program
{
    /// <summary>
    /// [EN] Runs the tools demo and writes each instrumentation log line.
    /// [JA] tools demo を実行し、instrumentation log の各行を書き込みます。
    /// </summary>
    public static void Main()
    {
        foreach (var line in ToolsDemo.Run())
        {
            Console.WriteLine(line);
        }
    }
}
