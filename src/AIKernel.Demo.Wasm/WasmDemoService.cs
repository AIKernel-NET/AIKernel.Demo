using AIKernel.Abstractions.Compute;
using AIKernel.Common.Results;
using AIKernel.Wasm.Comput;
using AIKernel.Wasm.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace AIKernel.Demo.Wasm;

/// <summary>
/// [EN] Interface-backed implementation for the WASM demo surface.
/// [JA] WASM Demo surface の Interface-backed implementation です。
/// </summary>
/// <remarks>
/// [EN] This service is written as reference code for learners: it keeps the runtime context, provider objects, process lifecycle, WebGPU registration, and CPU fallback path explicit instead of hiding them behind a test fixture.
/// [JA] この service は学習者向けの参照コードとして書かれています。runtime context、provider object、process lifecycle、WebGPU registration、CPU fallback path を test fixture の裏に隠さず明示します。
/// </remarks>
public sealed class WasmDemoService : Abstractions.IWasmDemoSurface
{
    /// <summary>
    /// [EN] Runs a minimal WASM process lifecycle through the Core process abstraction.
    /// [JA] Core process abstraction を通じて最小 WASM process lifecycle を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The method creates the supporting providers to show the OS-shaped surface around a WASM process. The process payload is a tiny deterministic module marker, not a real application.
    /// [JA] この method は WASM process の周辺にある OS-shaped surface を示すため、補助 Provider を作成します。process payload は実 application ではなく、小さな決定論的 module marker です。
    /// </remarks>
    /// <returns>
    /// [EN] A lifecycle snapshot containing id, name, running state, and final state.
    /// [JA] id、name、running state、final state を含む lifecycle snapshot です。
    /// </returns>
    public async Task<WasmProcessDemoResult> RunProcessLifecycleAsync()
    {
        using var runtime = new WasmRuntime();
        var context = runtime.CreateContext();
        var memory = new WasmMemoryProvider();
        var fileSystem = new WasmFileSystemProvider();
        var events = new WasmEventProvider();
        var audio = new WasmAudioProvider();
        var screenshot = new WasmScreenshotProvider();
        var saveState = new WasmSaveStateProvider();
        var stdin = new WasmStdinProvider();
        var time = new WasmTimeProvider();
        var provider = new WasmProcessProvider();
        var process = await provider.CreateProcessAsync(
            "demo.wasm.process",
            new WasmProcessOptions(ModuleBytes: [0x00, 0x61, 0x73, 0x6d]))
            .ConfigureAwait(false);

        await process.StartAsync().ConfigureAwait(false);
        var running = process.State;
        await process.StopAsync().ConfigureAwait(false);

        return new WasmProcessDemoResult(
            ProcessId: process.Id.Value.ToString("D"),
            ProcessName: ProcessName(process),
            RunningState: running,
            FinalState: process.State);
    }

    /// <summary>
    /// [EN] Runs vector addition through the WebGPU provider while forcing deterministic CPU fallback.
    /// [JA] deterministic CPU fallback を強制しながら WebGPU Provider 経由で vector addition を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] Automated demos should not require browser GPU hardware. This method still touches the WebGPU provider, native/wasm backend types, descriptor mapper, sample kernel, and Python bridge before executing the safe fallback path.
    /// [JA] 自動デモは browser GPU hardware を要求すべきではありません。この method は安全な fallback path を実行する前に、WebGPU Provider、native / wasm backend 型、descriptor mapper、sample kernel、Python bridge に触れます。
    /// </remarks>
    /// <returns>
    /// [EN] A deterministic vector-add result and provider fallback status.
    /// [JA] 決定論的な vector-add result と Provider fallback status です。
    /// </returns>
    public async Task<WebGpuFallbackDemoResult> RunWebGpuFallbackVectorAddAsync()
    {
        var provider = new WebGpuComputeProvider(new WebGpuComputeSettings
        {
            ForceCpuFallback = true,
            BackendName = "demo-cpu-fallback"
        });
        using var services = new ServiceCollection()
            .AddWebGpuComputeProvider(new WebGpuComputeSettings
            {
                ForceCpuFallback = true,
                BackendName = "demo-service-registration"
            })
            .BuildServiceProvider();
        _ = services.GetRequiredService<WebGpuComputeProvider>();
        _ = new WebGpuNativeBackend();
        _ = new WebGpuWasmBackend();
        _ = WebGpuSampleKernels.VectorAdd;
        _ = WebGpuComputePythonBridge.CreateInvoker();
        _ = WebGpuComputeCapabilityContracts.ToContract(
            new WebGpuComputeCapabilityDescriptor(
                "aikernel.wasm.webgpu",
                "demo",
                new Dictionary<string, string> { ["mode"] = "dry-run" }));

        var left = new[] { 1.0f, 2.0f, 3.0f, 4.0f };
        var right = new[] { 10.0f, 20.0f, 30.0f, 40.0f };
        var byteCount = left.Length * sizeof(float);
        using var leftBuffer = await provider.CreateBufferAsync(byteCount).ConfigureAwait(false);
        using var rightBuffer = await provider.CreateBufferAsync(byteCount).ConfigureAwait(false);
        using var outputBuffer = await provider.CreateBufferAsync(byteCount).ConfigureAwait(false);

        await provider.WriteBufferAsync(leftBuffer, ToBytes(left)).ConfigureAwait(false);
        await provider.WriteBufferAsync(rightBuffer, ToBytes(right)).ConfigureAwait(false);
        await provider.ExecuteKernelAsync(ComputeKernel.CreateVectorAdd(left.Length), leftBuffer, rightBuffer, outputBuffer)
            .ConfigureAwait(false);

        var outputBytes = new byte[byteCount];
        await provider.ReadBufferAsync(outputBuffer, outputBytes).ConfigureAwait(false);

        return new WebGpuFallbackDemoResult(
            ProviderId: provider.ProviderId,
            UsingCpuFallback: provider.UsingCpuFallback,
            VectorAdd: ToFloats(outputBytes));
    }

    /// <summary>
    /// [EN] Converts float values into little-endian bytes for compute buffers.
    /// [JA] compute buffer 用に float 値を little-endian byte へ変換します。
    /// </summary>
    /// <param name="values">
    /// [EN] Vector values written into a compute buffer.
    /// [JA] compute buffer に書き込む vector 値です。
    /// </param>
    /// <returns>
    /// [EN] The byte representation used by the compute provider.
    /// [JA] compute provider が使用する byte 表現です。
    /// </returns>
    private static byte[] ToBytes(IReadOnlyList<float> values)
    {
        var bytes = new byte[values.Count * sizeof(float)];
        for (var index = 0; index < values.Count; index++)
        {
            BitConverter.GetBytes(values[index]).CopyTo(bytes, index * sizeof(float));
        }

        return bytes;
    }

    /// <summary>
    /// [EN] Reads the WASM process name through an Option-based cast.
    /// [JA] Option ベースの cast を通じて WASM process name を読み取ります。
    /// </summary>
    /// <param name="process">
    /// [EN] Process returned from the Core process abstraction.
    /// [JA] Core process abstraction から返された process です。
    /// </param>
    /// <returns>
    /// [EN] The WASM process name, or the deterministic demo fallback name.
    /// [JA] WASM process name、または決定論的な demo fallback name です。
    /// </returns>
    private static string ProcessName(AIKernel.Abstractions.Processes.IProcess process)
        => WasmProcessOption(process).Match(() => "demo.wasm.process", value => value.Name);

    /// <summary>
    /// [EN] Converts the process abstraction into an optional concrete WASM process.
    /// [JA] process abstraction を optional concrete WASM process に変換します。
    /// </summary>
    /// <remarks>
    /// [EN] The demo uses Option instead of null so readers can see the AIKernel preference for explicit presence checks.
    /// [JA] このデモは null ではなく Option を使い、AIKernel が明示的な存在確認を好むことを読者に示します。
    /// </remarks>
    /// <param name="process">
    /// [EN] Process returned from the provider.
    /// [JA] Provider から返された process です。
    /// </param>
    /// <returns>
    /// [EN] Some when the process is a WasmProcess; otherwise None.
    /// [JA] process が WasmProcess の場合は Some、それ以外は None です。
    /// </returns>
    private static Option<WasmProcess> WasmProcessOption(AIKernel.Abstractions.Processes.IProcess process)
        => process switch
        {
            WasmProcess wasmProcess => Option<WasmProcess>.Some(wasmProcess),
            _ => Option<WasmProcess>.None()
        };

    /// <summary>
    /// [EN] Converts compute-buffer bytes back into float values.
    /// [JA] compute-buffer byte を float 値へ戻します。
    /// </summary>
    /// <param name="bytes">
    /// [EN] Bytes read from the output compute buffer.
    /// [JA] output compute buffer から読み取った byte です。
    /// </param>
    /// <returns>
    /// [EN] Float values produced by the vector-add operation.
    /// [JA] vector-add operation が生成した float 値です。
    /// </returns>
    private static IReadOnlyList<float> ToFloats(byte[] bytes)
    {
        var values = new float[bytes.Length / sizeof(float)];
        for (var index = 0; index < values.Length; index++)
        {
            values[index] = BitConverter.ToSingle(bytes, index * sizeof(float));
        }

        return values;
    }
}
