using AIKernel.Providers.Standard.EventBus;
using AIKernel.Providers.Standard.FileSystem;
using AIKernel.Providers.Standard.Logging;
using AIKernel.Providers.Standard.Network;
using AIKernel.Providers.Standard.Profiler;

namespace AIKernel.Demo.StandardProviders;

/// <summary>
/// [EN] Demonstrates standard OS driver providers beyond compute/process/scheduler.
/// [JA] compute / process / scheduler 以外の標準 OS driver Provider を示します。
/// </summary>
/// <remarks>
/// [EN] Standard providers are the built-in driver layer. This demo uses in-memory and console-safe surfaces so readers can observe file system, event bus, logging, network type boundaries, and profiling without touching external services.
/// [JA] Standard provider は built-in driver layer です。このデモでは in-memory と console-safe な surface を使うため、外部 service に触れず file system、event bus、logging、network type boundary、profiling を観察できます。
/// </remarks>
public static class StandardProvidersDemo
{
    /// <summary>
    /// [EN] Runs the standard provider golden path without external I/O.
    /// [JA] 外部 I/O なしで standard provider golden path を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] MemoryFileSystemProvider shows a safe VFS-backed file example, EventBusProvider shows publish/subscribe flow, ConsoleLoggingProvider emits a visible log line, and ProfilerProvider captures a local resource snapshot.
    /// [JA] MemoryFileSystemProvider は安全な VFS-backed file example、EventBusProvider は publish / subscribe flow、ConsoleLoggingProvider は visible log line、ProfilerProvider は local resource snapshot を示します。
    /// </remarks>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] A deterministic log describing the standard provider surfaces touched by the demo.
    /// [JA] デモが触れた standard provider surface を説明する決定論的ログです。
    /// </returns>
    public static async Task<IReadOnlyList<string>> RunAsync()
    {
        var fs = new MemoryFileSystemProvider();
        fs.WriteText("/demo/readme.txt", "AIKernel standard provider demo");

        var eventBus = new EventBusProvider();
        var observed = 0;
        eventBus.Subscribe<string>("demo.standard.event", payload =>
        {
            observed += payload.Length;
            return Task.CompletedTask;
        });
        await eventBus.PublishAsync("demo.standard.event", "ok").ConfigureAwait(false);

        var consoleLogger = new ConsoleLoggingProvider();
        consoleLogger.Write("info", "standard provider demo");

        var profiler = new ProfilerProvider();
        var snapshot = profiler.Capture();

        var typeMap = new[]
        {
            typeof(FileSystemProviderBase).Name,
            typeof(MemoryFileSystemProvider).Name,
            typeof(PhysicalFileSystemProvider).Name,
            typeof(ZipFileSystemProvider).Name,
            typeof(LoggingProviderBase).Name,
            typeof(FileLoggingProvider).Name,
            typeof(NetworkProvider).Name,
            typeof(HttpNetworkProvider).Name,
            typeof(WebSocketNetworkProvider).Name,
            typeof(EventBusProvider).Name,
            typeof(ProfilerProvider).Name
        };

        return
        [
            "AIKernel.Demo.StandardProviders",
            $"fs.exists={fs.Exists("/demo/readme.txt")}",
            $"fs.text={fs.ReadText("/demo/readme.txt")}",
            $"event.observed={observed}",
            $"profiler.memory={snapshot.ManagedMemoryBytes}",
            $"standard.map={string.Join(',', typeMap)}"
        ];
    }
}
