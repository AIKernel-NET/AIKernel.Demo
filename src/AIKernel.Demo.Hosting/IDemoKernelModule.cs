using Microsoft.Extensions.DependencyInjection;

namespace AIKernel.Demo.Hosting;

public interface IDemoKernelModule
{
    void Register(IServiceCollection services);
}
