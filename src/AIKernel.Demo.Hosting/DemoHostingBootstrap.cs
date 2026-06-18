using AIKernel.Common.Results;
using AIKernel.Demo.Dsl;
using AIKernel.Demo.Execution;
using AIKernel.Demo.KernelExecution;

namespace AIKernel.Demo.Hosting;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public static class DemoHostingBootstrap
{
    /// <summary>
    /// [EN] Executes a deterministic operation on the demo contract surface.
    /// [JA] Demo 契約サーフェス上で決定論的な操作を実行します。
    /// </summary>
    /// <param name="dslText">EN:  JA: dslText パラメーターです。
    /// [EN] The demo value supplied for <c>dslText</c>.
    /// [JA] <c>dslText</c> として渡される Demo 値です。
    /// </param>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static async Task<Result<DemoKernelRun>> ExecuteSimplePipelineAsync(
        string dslText)
        =>
            await (
                from parsed in DslParserDemo.Parse(dslText)
                from graph in DslToGraphDemo.Convert(parsed)
                from run in ExecuteKernelAsync(graph)
                select run)
            .ConfigureAwait(false);

    private static async Task<Result<DemoKernelRun>> ExecuteKernelAsync(
        DemoExecutionGraph graph)
    {
        using var host = DemoKernelHost.Build(new DemoKernelModule(), new DemoProviderModule());
        var kernel = host.GetKernel();
        var run = await kernel.ExecuteAsync(graph).ConfigureAwait(false);
        return Result<DemoKernelRun>.Success(run);
    }
}
