using AIKernel.Demo.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIKernel.Demo.Hosting;

public sealed class DemoProviderModule : IDemoKernelModule
{
    public void Register(
        IServiceCollection services)
    {
        services.AddSingleton<DemoProviderRouter>();
        services.AddSingleton<DemoLlmController>();
    }
}
