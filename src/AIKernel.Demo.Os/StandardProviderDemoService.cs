using AIKernel.Providers.Standard.Compute;
using AIKernel.Providers.Standard.DependencyInjection;
using AIKernel.Providers.Standard.Processes;
using AIKernel.Providers.Standard.Scheduler;
using Microsoft.Extensions.DependencyInjection;

namespace AIKernel.Demo.Os;

/// <summary>
/// [EN] Interface-backed implementation for the standard-provider demo surface.
/// [JA] standard-provider Demo surface の Interface-backed implementation です。
/// </summary>
public sealed class StandardProviderDemoService : Abstractions.IStandardProviderDemoSurface
{
    /// <summary>EN: Documentation for public API. JA: RunAsync を実行します。</summary>
    /// <inheritdoc />
    public async Task<StandardProviderDemoResult> RunAsync()
    {
        using var services = new ServiceCollection()
            .AddAIKernelStandardProviders()
            .BuildServiceProvider();

        var compute = services.GetRequiredService<CpuComputeProvider>();
        var supervisor = services.GetRequiredService<DefaultProcessSupervisorProvider>();
        var process = await supervisor.CreateProcessAsync("demo.standard.process").ConfigureAwait(false);
        await process.StopAsync().ConfigureAwait(false);

        var scheduler = new SchedulerProvider();
        var job = scheduler.Add("aik system info", TimeSpan.FromMinutes(1));

        return new StandardProviderDemoResult(
            ComputeProviderId: compute.ProviderId,
            VectorAdd: compute.AddVectors([1.0f, 2.0f, 3.0f], [4.0f, 5.0f, 6.0f]),
            ProcessId: process.Id.Value.ToString("D"),
            ProcessState: process.State,
            ScheduledJobName: job.Name,
            ScheduledInterval: job.Interval);
    }
}
