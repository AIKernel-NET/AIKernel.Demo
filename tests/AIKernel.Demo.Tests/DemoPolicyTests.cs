using AIKernel.Demo.PDP;

namespace AIKernel.Demo.Tests;

public sealed class DemoPolicyTests
{
    [Fact]
    public void PolicyAllowsInputWithinLimit()
    {
        var engine = new DemoPolicyEngine([new DemoPolicyRule(8, "too-long")]);

        var decision = engine.Evaluate("hello");

        Assert.True(decision.Allowed);
        Assert.Equal(DemoPolicyCode.Allow, decision.Code);
    }

    [Fact]
    public void PolicyDeniesInputOverLimit()
    {
        var engine = new DemoPolicyEngine([new DemoPolicyRule(4, "too-long")]);

        var decision = engine.Evaluate("hello");

        Assert.False(decision.Allowed);
        Assert.Equal(DemoPolicyCode.Deny, decision.Code);
        Assert.Equal("too-long", decision.Reason);
    }

    [Fact]
    public void FirstFailingRuleDeterminesDecision()
    {
        var engine = new DemoPolicyEngine(
        [
            new DemoPolicyRule(10, "rule1"),
            new DemoPolicyRule(4, "rule2"),
            new DemoPolicyRule(1, "rule3")
        ]);

        var decision = engine.Evaluate("hello");

        Assert.False(decision.Allowed);
        Assert.Equal(DemoPolicyCode.Deny, decision.Code);
        Assert.Equal("rule2", decision.Reason);
    }
}
