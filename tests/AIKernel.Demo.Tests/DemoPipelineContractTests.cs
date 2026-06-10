using AIKernel.Demo.Pipelines;
using AIKernel.Demo.PDP;
using System.Text.RegularExpressions;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoPipelineContractTests
{
    private static readonly Regex IdentifierPattern =
        new(@"^[a-z][a-z0-9-]*(\.[a-z][a-z0-9-]*)*$", RegexOptions.CultureInvariant);

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public void DefaultPipelinePublishesRoutingAndDslContractAlignment()
    {
        var run = DemoPipelineCatalog.CreateDefaultRun();

        Assert.Equal("demo.pipeline.default", run.PipelineId);
        Assert.Equal(DemoPolicyCode.Allow, run.Decision);
        Assert.Equal("demo-replay-hash", run.ReplayHash);
        Assert.Equal(4, run.StepCount);
        Assert.Equal("demo.mock", run.ContractAlignment.RoutingProviderId);
        Assert.Equal("mock-fixed", run.ContractAlignment.RoutingModelId);
        Assert.Equal("Pipeline", run.ContractAlignment.DslRootType);
        Assert.Equal("normalize", run.ContractAlignment.DslFirstStepName);
    }

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public void ContractAlignmentUsesOnlyPublicDemoContractDto()
    {
        var run = DemoPipelineCatalog.CreateDefaultRun();

        Assert.IsAssignableFrom<DemoContractAlignment>(run.ContractAlignment);
        Assert.True(run.ContractAlignment.GetType().IsPublic);
        Assert.True(run.ContractAlignment.GetType().IsSealed);
        Assert.True(IsRecord(run.ContractAlignment.GetType()));
        Assert.Equal(typeof(DemoContractAlignment), run.ContractAlignment.GetType());
    }

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public void PipelineIdFollowsCanonicalFormat()
    {
        var run = DemoPipelineCatalog.CreateDefaultRun();

        Assert.Matches(IdentifierPattern, run.PipelineId);
    }

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public void ContractAlignmentFieldsAreNonNull()
    {
        var run = DemoPipelineCatalog.CreateDefaultRun();

        Assert.NotNull(run.ContractAlignment.RoutingProviderId);
        Assert.NotNull(run.ContractAlignment.RoutingModelId);
        Assert.NotNull(run.ContractAlignment.DslRootType);
        Assert.NotNull(run.ContractAlignment.DslFirstStepName);
    }

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public void DemoPipelineCatalogReturnsContractDto()
    {
        var run = DemoPipelineCatalog.CreateDefaultRun();

        Assert.IsAssignableFrom<DemoPipelineRun>(run);
        Assert.True(run.GetType().IsPublic);
        Assert.Equal(typeof(DemoPipelineRun), run.GetType());
    }

    private static bool IsRecord(
        Type type)
        => type.GetMethod("<Clone>$") is not null;
}
