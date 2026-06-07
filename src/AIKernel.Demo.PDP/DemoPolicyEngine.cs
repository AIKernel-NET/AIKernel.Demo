namespace AIKernel.Demo.PDP;

public sealed class DemoPolicyEngine
{
    private readonly IReadOnlyList<DemoPolicyRule> _rules;

    public DemoPolicyEngine(
        IReadOnlyList<DemoPolicyRule> rules)
        => _rules = rules;

    public DemoPolicyDecision Evaluate(
        string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        foreach (var rule in _rules)
        {
            var decision = rule.Evaluate(input);
            if (!decision.Allowed)
                return decision;
        }

        return DemoPolicyDecision.Allow("all-rules-allowed");
    }
}
