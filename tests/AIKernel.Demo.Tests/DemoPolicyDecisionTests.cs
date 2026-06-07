using AIKernel.Demo.PDP;

namespace AIKernel.Demo.Tests;

public sealed class DemoPolicyDecisionTests
{
    [Fact]
    public void AllowUsesTypedPolicyCode()
    {
        var decision = DemoPolicyDecision.Allow("demo-readonly");

        Assert.True(decision.Allowed);
        Assert.Equal(DemoPolicyCode.Allow, decision.Code);
        Assert.Equal("demo-readonly", decision.Reason);
    }

    [Fact]
    public void DenyUsesTypedPolicyCode()
    {
        var decision = DemoPolicyDecision.Deny("demo-blocked");

        Assert.False(decision.Allowed);
        Assert.Equal(DemoPolicyCode.Deny, decision.Code);
        Assert.Equal("demo-blocked", decision.Reason);
    }

    [Fact]
    public void PolicyReasonIsNullSafe()
    {
        var allow = DemoPolicyDecision.Allow(null);
        var deny = DemoPolicyDecision.Deny(null);

        Assert.Equal(string.Empty, allow.Reason);
        Assert.Equal(string.Empty, deny.Reason);
    }

    [Fact]
    public void EmptyReasonDoesNotAffectDecision()
    {
        var allow = DemoPolicyDecision.Allow(string.Empty);
        var deny = DemoPolicyDecision.Deny(string.Empty);

        Assert.True(allow.Allowed);
        Assert.Equal(DemoPolicyCode.Allow, allow.Code);
        Assert.False(deny.Allowed);
        Assert.Equal(DemoPolicyCode.Deny, deny.Code);
    }
}
