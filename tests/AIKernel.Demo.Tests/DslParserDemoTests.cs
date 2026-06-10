using AIKernel.Demo.Dsl;
using AIKernel.Demo.Execution;
using AIKernel.Dtos.Dsl;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DslParserDemoTests
{
    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public void ParseReturnsDslDocumentForValidSteps()
    {
        var result = DslParserDemo.Parse("""
            normalize
            structure
            provider
            polish
            """);

        Assert.True(result.IsSuccessState);
        var root = Assert.IsType<PipelineRootNode>(result.Value.Root);
        Assert.Equal(4, root.Steps.Count);
        Assert.Equal(
            ["normalize", "structure", "provider", "polish"],
            root.Steps.Cast<StepNode>().Select(step => step.Name));
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
    public void ParseFailsForUnknownStep()
    {
        var result = DslParserDemo.Parse("""
            normalize
            INVALID_STEP
            """);

        Assert.False(result.IsSuccessState);
        Assert.Contains("Unknown step: INVALID_STEP", result.Error.Message);
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
    public void ParseIgnoresEmptyAndCommentLines()
    {
        var result = DslParserDemo.Parse("""
            normalize

            # comment
            provider
            """);

        Assert.True(result.IsSuccessState);
        var root = Assert.IsType<PipelineRootNode>(result.Value.Root);
        Assert.Equal(["normalize", "provider"], root.Steps.Cast<StepNode>().Select(step => step.Name));
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
    public void ParseUsesInjectedAllowedSteps()
    {
        var previous = DslParserDemo.AllowedSteps;
        try
        {
            DslParserDemo.AllowedSteps = new HashSet<string>(StringComparer.Ordinal)
            {
                "normalize",
                "call",
                "suspend"
            };

            var result = DslParserDemo.Parse("""
                normalize
                call
                suspend
                """);

            Assert.True(result.IsSuccessState);
            var root = Assert.IsType<PipelineRootNode>(result.Value.Root);
            Assert.Equal(["normalize", "call", "suspend"], root.Steps.Cast<StepNode>().Select(step => step.Name));
        }
        finally
        {
            DslParserDemo.AllowedSteps = previous;
        }
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
    public void ParseUsesOrdinalCaseSensitiveAllowedSteps()
    {
        var previous = DslParserDemo.AllowedSteps;
        try
        {
            DslParserDemo.AllowedSteps = new HashSet<string>(StringComparer.Ordinal)
            {
                "normalize"
            };

            var result = DslParserDemo.Parse("NORMALIZE");

            Assert.False(result.IsSuccessState);
            Assert.Contains("Unknown step: NORMALIZE", result.Error.Message);
        }
        finally
        {
            DslParserDemo.AllowedSteps = previous;
        }
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
    public void ConvertBuildsLinearGraphFromDslDocument()
    {
        var parsed = DslParserDemo.Parse("""
            normalize
            structure
            provider
            polish
            """);

        Assert.True(parsed.IsSuccessState);
        var converted = DslToGraphDemo.Convert(parsed.Value);

        Assert.True(converted.IsSuccessState);
        Assert.IsType<DemoExecutionGraph>(converted.Value);
        Assert.Equal(4, converted.Value.Nodes.Count);
        Assert.Equal(
            ["normalize", "structure", "provider", "polish"],
            converted.Value.Nodes.Select(node => node.StepName));
    }
}
