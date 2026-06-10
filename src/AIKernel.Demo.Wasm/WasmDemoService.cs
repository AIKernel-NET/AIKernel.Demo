using AIKernel.Abstractions.Compute;
using AIKernel.Common.Results;
using AIKernel.Wasm.Comput;
using AIKernel.Wasm.Runtime;

namespace AIKernel.Demo.Wasm;

/// <summary>
/// [EN] Interface-backed implementation for the WASM demo surface.
/// [JA] WASM Demo surface の Interface-backed implementation です。
/// </summary>
public sealed class WasmDemoService : Abstractions.IWasmDemoSurface
{
    /// <inheritdoc />
    public async Task<WasmProcessDemoResult> RunProcessLifecycleAsync()
    {
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

    /// <inheritdoc />
    public async Task<WebGpuFallbackDemoResult> RunWebGpuFallbackVectorAddAsync()
    {
        var provider = new WebGpuComputeProvider(new WebGpuComputeSettings
        {
            ForceCpuFallback = true,
            BackendName = "demo-cpu-fallback"
        });

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

    private static byte[] ToBytes(IReadOnlyList<float> values)
    {
        var bytes = new byte[values.Count * sizeof(float)];
        for (var index = 0; index < values.Count; index++)
        {
            BitConverter.GetBytes(values[index]).CopyTo(bytes, index * sizeof(float));
        }

        return bytes;
    }

    private static string ProcessName(AIKernel.Abstractions.Processes.IProcess process)
        => WasmProcessOption(process).Match(() => "demo.wasm.process", value => value.Name);

    private static Option<WasmProcess> WasmProcessOption(AIKernel.Abstractions.Processes.IProcess process)
        => process switch
        {
            WasmProcess wasmProcess => Option<WasmProcess>.Some(wasmProcess),
            _ => Option<WasmProcess>.None()
        };

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
