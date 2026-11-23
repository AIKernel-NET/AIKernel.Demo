using AIKernel.Demo.Cuda;

/// <summary>
/// [EN] Console entry point for the Kernel CUDA Driver demo.
/// [JA] Kernel CUDA Driver デモのコンソールエントリポイントです。
/// </summary>
public static class Program
{
    /// <summary>
    /// [EN] Runs the CUDA memory transfer and prints its result.
    /// [JA] CUDA メモリ転送を実行し、結果を出力します。
    /// </summary>
    public static void Main()
    {
        foreach (var line in CudaDemo.Run())
        {
            Console.WriteLine(line);
        }
    }
}
