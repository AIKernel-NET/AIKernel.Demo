using System.Runtime.InteropServices;
using AIKernel.Kernel.Memory;

namespace AIKernel.Demo.Cuda;

/// <summary>Demonstrates a GPU memory transfer through the Kernel CUDA Driver.</summary>
public static class CudaDemo
{
    private const int DriverMissing = 2;
    private const int NoDevice = 3;

    /// <summary>Verifies a VRAM round trip or reports why CUDA is unavailable.</summary>
    public static IReadOnlyList<string> Run()
    {
        if (RuntimeInformation.ProcessArchitecture != Architecture.X64 ||
            !(OperatingSystem.IsWindows() || OperatingSystem.IsLinux()))
        {
            return Skip("the packaged CUDA Driver requires Windows x64 or Linux x64");
        }

        using var device = CudaVirtualMemoryDevice.TryCreate(0, out int status);
        if (device is null)
        {
            return status switch
            {
                DriverMissing => Skip("the NVIDIA driver is unavailable"),
                NoDevice => Skip("no CUDA device is available"),
                _ => throw new InvalidOperationException($"CUDA context creation failed with status {status}.")
            };
        }

        ReadOnlySpan<byte> source = [1, 2, 3, 4];
        Span<byte> destination = stackalloc byte[source.Length];
        using var block = device.Upload(source);
        block.Download(destination);
        if (!source.SequenceEqual(destination))
        {
            throw new InvalidOperationException("The bytes downloaded from VRAM differ from the uploaded bytes.");
        }

        return
        [
            "AIKernel.Demo.Cuda",
            "status=ok",
            "backend=aikernel_cuda_driver",
            $"roundtrip.bytes={source.Length}"
        ];
    }

    private static string[] Skip(string reason) => ["AIKernel.Demo.Cuda", "status=skipped", $"reason={reason}"];
}
