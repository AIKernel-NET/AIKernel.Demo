using System.Security.Cryptography;
using System.Text;

namespace AIKernel.Demo.Providers.Mock;

public sealed class MockProvider
{
    public MockProviderResponse Generate(
        string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var hash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(input)));

        return new MockProviderResponse(hash);
    }
}
