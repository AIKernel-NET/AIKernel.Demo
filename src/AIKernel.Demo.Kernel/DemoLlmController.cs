namespace AIKernel.Demo.Kernel;

public sealed class DemoLlmController
{
    public string Generate(
        string input)
    {
        ArgumentNullException.ThrowIfNull(input);
        return $"llm-output({input})";
    }
}
