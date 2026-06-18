using AIKernel.Providers.ChatHistory;
using AIKernel.Providers.ChatHistory.Models;
using AIKernel.Providers.ChatOpenAI;
using AIKernel.Providers.CudaCompute;
using AIKernel.Providers.DynamicPipelineCompiler;
using AIKernel.Providers.LocalLlm;
using AIKernel.Providers.MicrosoftAI;

namespace AIKernel.Demo.Providers;

/// <summary>
/// [EN] Demonstrates official external providers through dry-run metadata and descriptors.
/// [JA] 公式外部 Provider を dry-run metadata / descriptor 経由で示します。
/// </summary>
/// <remarks>
/// [EN] Providers are loaded as external drivers in the AIKernel OS model. This demo initializes representative providers and reads their descriptors, but deliberately avoids network calls, model inference, native dispatch, or service credentials.
/// [JA] Provider は AIKernel OS model における外部 driver として読み込まれます。このデモは代表 Provider を初期化して descriptor を読みますが、network call、model inference、native dispatch、service credential は意図的に使いません。
/// </remarks>
public static class ProvidersDemo
{
    /// <summary>
    /// [EN] Runs the minimal external provider golden path without network calls.
    /// [JA] network call なしで最小 external provider golden path を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The provider id and capability id lines teach the registration vocabulary used by dynamic loading, manifests, and capability invocation.
    /// [JA] provider id と capability id の行は、dynamic loading、manifest、capability invocation で使われる registration vocabulary を学ぶためのものです。
    /// </remarks>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] A deterministic dry-run log of official Provider surfaces.
    /// [JA] 公式 Provider surface の決定論的 dry-run ログです。
    /// </returns>
    public static async Task<IReadOnlyList<string>> RunAsync()
    {
        var chatHistory = new ChatHistoryProvider();
        await chatHistory.InitializeAsync().ConfigureAwait(false);
        var chatHistoryDescriptor = chatHistory.ToCapabilityDescriptor();

        var openAi = new ChatOpenAIProvider();
        await openAi.InitializeAsync().ConfigureAwait(false);

        var localLlm = new LocalLlmProvider();
        await localLlm.InitializeAsync().ConfigureAwait(false);

        var compiler = new DynamicPipelineCompilerProvider();
        await compiler.InitializeAsync().ConfigureAwait(false);
        var compilerDescriptor = compiler.ToCapabilityDescriptor();

        var cuda = new CudaComputeProvider();
        await cuda.InitializeAsync().ConfigureAwait(false);
        var cudaDescriptor = cuda.ToCapabilityDescriptor();

        var modelTypes = new[]
        {
            typeof(ChatHistoryRecord).Name,
            typeof(ChatHistory).Name,
            typeof(ChatOpenAIClient).Name,
            typeof(ChatOpenAIInvoker).Name,
            typeof(CudaComputeCapabilityContracts).Name,
            typeof(CudaComputeInvoker).Name,
            typeof(CudaComputePythonBridge).Name,
            typeof(CudaComputeSettings).Name,
            typeof(LocalLlmInvoker).Name,
            typeof(DynamicPipelineCompilerInvoker).Name,
            typeof(OpenAICompatibleProvider).Name,
            typeof(OpenAICompatibleResponseMapper).Name,
            typeof(OpenAICompatibleProviderCapabilities).Name,
            typeof(OpenAICompatibleProviderOptionsValidator).Name,
            typeof(ProviderApiException).Name,
            typeof(AIKernel.Providers.MicrosoftAI.DependencyInjection.OpenAIHostingExtensions).Name
        };

        return
        [
            "AIKernel.Demo.Providers",
            $"chat-history.provider={chatHistory.ProviderId}",
            $"chat-history.capability={chatHistoryDescriptor.CapabilityId}",
            $"openai.provider={openAi.ProviderId}",
            $"local-llm.provider={localLlm.ProviderId}",
            $"compiler.capability={compilerDescriptor.CapabilityId}",
            $"cuda.capability={cudaDescriptor.CapabilityId}",
            $"provider.map={string.Join(',', modelTypes)}"
        ];
    }
}
