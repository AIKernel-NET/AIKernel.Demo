using AIKernel.Contracts;
using AIKernel.Dtos.Context;
using AIKernel.Dtos.Execution;
using AIKernel.Dtos.Security;
using AIKernel.Enums;

namespace AIKernel.Demo.Contracts;

/// <summary>
/// [EN] Demonstrates public contract and DTO usage without depending on runtime internals.
/// [JA] runtime internal に依存せず public contract / DTO usage を示します。
/// </summary>
/// <remarks>
/// [EN] Contracts and DTOs are the data boundary of AIKernel. This demo constructs them directly so readers can learn the shapes before studying Kernel execution, policy evaluation, or replay.
/// [JA] Contracts と DTO は AIKernel の data boundary です。このデモではそれらを直接構築し、Kernel execution、policy evaluation、replay を読む前に形を学べるようにしています。
/// </remarks>
public static class ContractsDemo
{
    /// <summary>
    /// [EN] Runs the minimal contracts golden path.
    /// [JA] 最小 contracts golden path を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The UnifiedContextDto line shows orchestration context, the PolicyEvaluationResult line shows governance output, and the HashChain line shows execution replay identity without requiring any runtime service.
    /// [JA] UnifiedContextDto の行は orchestration context、PolicyEvaluationResult の行は governance output、HashChain の行は runtime service なしで execution replay identity を示します。
    /// </remarks>
    /// <returns>
    /// [EN] A deterministic log of public contract and DTO values.
    /// [JA] public contract と DTO value の決定論的ログです。
    /// </returns>
    public static IReadOnlyList<string> Run()
    {
        var context = new UnifiedContextDto
        {
            Id = "demo.context",
            Orchestration = new OrchestrationContextDto
            {
                Purpose = "demonstrate-contracts",
                Structure = "kernel-provider-vfs-governance",
                CreatedAt = DateTime.UtcNow
            },
            CreatedAt = DateTime.UtcNow,
            SignalToNoiseRatio = 1.0
        };
        var decision = new PolicyEvaluationResult
        {
            AllAllowed = true,
            Decisions = [new AccessDecision { Allowed = true, Reason = "demo" }],
            RiskLevel = "Low"
        };
        var hashChain = new HashChain
        {
            StructureHash = "sha256:structure",
            GenerationHash = "sha256:generation",
            GenerationParentHash = "sha256:generation-parent",
            PolishHash = "sha256:polish",
            PolishParentHash = "sha256:polish-parent"
        };
        var contracts = new[]
        {
            typeof(IUnifiedContextContract).Name,
            typeof(IKernelContextContract).Name,
            typeof(IOrchestrationContract).Name,
            typeof(IMaterialContract).Name,
            typeof(IExpressionContract).Name,
            typeof(IAuditEventContract).Name
        };

        return
        [
            "AIKernel.Demo.Contracts",
            $"context.id={context.Id}",
            $"decision.allowed={decision.AllAllowed}",
            $"decision.first={decision.Decisions[0].Allowed}",
            $"hashchain.algorithm={hashChain.HashAlgorithm}",
            $"hashchain.structure={hashChain.StructureHash}",
            $"contracts={string.Join(',', contracts)}"
        ];
    }
}
