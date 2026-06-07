namespace AIKernel.Demo.Providers.Mock;

public sealed record MockProviderResponse(
    string ProviderId,
    string Output,
    string OutputHash);
