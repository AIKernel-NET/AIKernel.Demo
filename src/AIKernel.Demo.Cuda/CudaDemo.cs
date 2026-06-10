using AIKernel.Cuda13.Libtorch2_12.WinX64.Capability;
using AIKernel.Cuda13.Libtorch2_12.WinX64.Interop;
using AIKernel.Cuda13.Libtorch2_12.WinX64.Model;

namespace AIKernel.Demo.Cuda;

/// <summary>
/// [EN] Demonstrates the CUDA 13.0 package through a Windows-only guarded dry-run.
/// [JA] Windows のみの guarded dry-run として CUDA 13.0 package を示します。
/// </summary>
/// <remarks>
/// [EN] CUDA 13.0 is a Windows-native package in this release. The demo always constructs descriptors and requests, but only reports runnable dry-run status on Windows so Linux and CI runs remain deterministic.
/// [JA] CUDA 13.0 はこの release では Windows-native package です。このデモは常に descriptor と request を構築しますが、Linux や CI の実行を決定論的に保つため runnable dry-run status は Windows でのみ返します。
/// </remarks>
public static class CudaDemo
{
    /// <summary>
    /// [EN] Runs the CUDA golden path or returns a deterministic skip outside Windows.
    /// [JA] CUDA golden path を実行するか、Windows 以外では決定論的 skip を返します。
    /// </summary>
    /// <remarks>
    /// [EN] The request is intentionally minimal and does not bind a native model. It teaches the LibTorch capability descriptor and request validation surface without loading GPU resources.
    /// [JA] request は意図的に最小化され、native model を bind しません。GPU resource を読み込まずに LibTorch capability descriptor と request validation surface を学ぶためのものです。
    /// </remarks>
    /// <returns>
    /// [EN] A deterministic CUDA teaching log, or a deterministic skip log outside Windows.
    /// [JA] 決定論的な CUDA 教材ログ、または Windows 以外での決定論的 skip ログです。
    /// </returns>
    public static IReadOnlyList<string> Run()
    {
        var descriptor = LibTorchCapabilityDescriptor.Create();
        var request = LlamaForwardRequest.TryCreate(
            new Dictionary<string, string>
            {
                ["model_handle"] = "1",
                ["input_ids"] = "1,2,3"
            });
        var typeMap = new[]
        {
            typeof(LibTorchCapabilityInvoker).Name,
            typeof(SafeLlamaModelHandle).Name,
            typeof(LlamaModelConfig).Name,
            typeof(LlamaForwardResult).Name
        };

        if (!OperatingSystem.IsWindows())
        {
            return
            [
                "AIKernel.Demo.Cuda",
                "status=skipped",
                "reason=CUDA package is Windows native",
                $"capability={descriptor.CapabilityId}",
                $"cuda.map={string.Join(',', typeMap)}"
            ];
        }

        return
        [
            "AIKernel.Demo.Cuda",
            "status=dry-run",
            $"capability={descriptor.CapabilityId}",
            $"request.valid={request.Succeeded}",
            $"cuda.map={string.Join(',', typeMap)}"
        ];
    }
}
