using AIKernel.Core.Routing;
using AIKernel.Core.Time;
using AIKernel.Core.Vfs.Memory;
using AIKernel.Abstractions.Security;
using AIKernel.Hosting;
using KernelType = AIKernel.Kernel.Kernel;

namespace AIKernel.Demo.CoreRuntime;

/// <summary>
/// [EN] Demonstrates Core, Hosting, Kernel, VFS, ROM-adjacent routing, Time, and Security entry points.
/// [JA] Core、Hosting、Kernel、VFS、ROM 隣接 routing、Time、Security の入口を示します。
/// </summary>
/// <remarks>
/// [EN] This demo is intentionally small: it does not boot a full Kernel, but it makes the public runtime seams visible so learners can see where routing, capability registration, clock control, VFS, hosting, and secure option resolution enter the OS-shaped API.
/// [JA] このデモは意図的に小さくしています。完全な Kernel boot は行わず、routing、capability registration、clock control、VFS、hosting、secure option resolution が OS-shaped API のどこから入るかを学習者に見える形で示します。
/// </remarks>
public static class CoreRuntimeDemo
{
    /// <summary>
    /// [EN] Runs the minimal Core runtime golden path.
    /// [JA] 最小 Core runtime golden path を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The returned lines are a deterministic teaching log rather than a benchmark. Each line names one contract boundary that application code can compose without depending on Core internals.
    /// [JA] 返却される行は benchmark ではなく、決定論的な教材ログです。各行は application code が Core internal に依存せず合成できる contract boundary を示します。
    /// </remarks>
    /// <returns>
    /// [EN] A readable log describing the Core runtime surface touched by the demo.
    /// [JA] デモが触れた Core runtime surface を説明する読みやすいログです。
    /// </returns>
    public static async Task<IReadOnlyList<string>> RunAsync()
    {
        var router = new PassThroughSemanticRouter();
        var route = await router.RouteAsync("demo core runtime").ConfigureAwait(false);
        var registry = new InMemoryCapabilityRegistry();
        var clock = KernelClock.System();
        var memory = new MemoryFileProvider(new MemoryFileProviderOptions());
        var coreTypes = new[]
        {
            typeof(AIKernelCoreBuilder).Name,
            typeof(AIKernelCoreHostingExtensions).Name,
            typeof(ModelProviderHostingExtensions).Name,
            typeof(SecureCredentialResolver<DemoSecureOptions>).Name,
            typeof(SecureHostingExtensions).Name,
            typeof(SecureOptionsStartupValidator<DemoSecureOptions>).Name,
            typeof(KernelType).Name,
            typeof(global::AIKernel.Kernel.KernelHostingExtensions).Name,
            typeof(global::AIKernel.Kernel.KernelRequestHasher).Name,
            typeof(global::AIKernel.Kernel.KernelTransactionIdFactory).Name,
            typeof(global::AIKernel.Kernel.KernelVfsSessionFactory).Name
        };

        return
        [
            "AIKernel.Demo.CoreRuntime",
            $"router.route={route.Route}",
            $"registry={registry.GetType().Name}",
            $"clock.replaying={clock.IsReplaying}",
            $"vfs.provider={memory.GetType().Name}",
            $"contract.map={string.Join(',', coreTypes)}"
        ];
    }

    /// <summary>
    /// [EN] Minimal secure options type used to demonstrate the public security options contract.
    /// [JA] public security options contract を示すための最小 secure options 型です。
    /// </summary>
    /// <remarks>
    /// [EN] Real applications bind this shape from configuration or secret stores; the demo keeps it local so no external secret provider is required.
    /// [JA] 実 application ではこの形を configuration や secret store から bind します。このデモでは外部 secret provider を不要にするため local に保ちます。
    /// </remarks>
    private sealed class DemoSecureOptions : ISecureOptions
    {
        /// <summary>
        /// [EN] Gets the name of the configured secret key.
        /// [JA] 設定された secret key の名前を取得します。
        /// </summary>
        public string? SecretKeyName { get; init; }

        /// <summary>
        /// [EN] Gets or sets the API key placeholder used by secure option validation examples.
        /// [JA] secure option validation example で使う API key placeholder を取得または設定します。
        /// </summary>
        public string? ApiKey { get; set; }
    }
}
