using AIKernel.Demo.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace AIKernel.Demo.Hosting;

public sealed class DemoKernelHost : IDisposable
{
    private readonly ServiceProvider _provider;

    private DemoKernelHost(
        ServiceProvider provider)
        => _provider = provider;

    public static DemoKernelHost Build(
        params IDemoKernelModule[] modules)
    {
        ArgumentNullException.ThrowIfNull(modules);

        var services = new ServiceCollection();
        foreach (var module in modules)
            module.Register(services);

        return new DemoKernelHost(services.BuildServiceProvider(validateScopes: true));
    }

    public DemoKernel GetKernel()
        => Get<DemoKernel>();

    public T Get<T>()
        where T : notnull
        => _provider.GetRequiredService<T>();

    public void Dispose()
        => _provider.Dispose();
}
