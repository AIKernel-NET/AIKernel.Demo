using AIKernel.Demo.Kernel;
using AIKernel.Demo.KernelExecution;
using Microsoft.Extensions.DependencyInjection;

namespace AIKernel.Demo.Hosting;

public sealed class DemoKernelModule : IDemoKernelModule
{
    public void Register(
        IServiceCollection services)
    {
        services.AddSingleton<DemoTaskManager>();
        services.AddSingleton<DemoExecutionEngine>();
        services.AddSingleton<DemoKernel>();
    }
}
