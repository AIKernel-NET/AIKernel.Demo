# AIKernel.Demo
AIKernel.Core の全抽象を “見える化” し、実際に動く最小〜中規模デモを体系的に提供する総合デモ集です。

## Repository Role

AIKernel.Demo is the user-facing sample workspace for the AIKernel 0.1.0
package family. It demonstrates how applications compose Core, Kernel,
Capability modules, VFS, PDP, pipelines, and Replay inspection without putting
demo-only code into the contract or runtime repositories.

The repository marks the transition from the completed 0.0.x
design-implementation phase to the 0.1.0 prototype validation phase scheduled
for 2026-06-09.

## Projects

- `AIKernel.Demo.Console` - minimal CLI demo for one inference pipeline using
  Kernel/Provider DI, `IInputNormalizer -> StructurePhase -> Provider ->
  OutputPolisher`, deterministic replay logging, and selectable Mock/OpenAI/Local
  providers.
- `AIKernel.Demo.WebApi` - OpenAI-compatible API-host demo implementing
  `/v1/chat/completions` through AIKernel, capability-based ProviderRouter
  selection, PDP Allow/Deny, and ReplayLog HTTP response headers.
- `AIKernel.Demo.Wasm` - browser Playground surface for Context, Execution, and
  VFS visualization, WebAssembly provider calls, DAG step animation, and
  PromptRules signature verification.
- `AIKernel.Demo.Providers.Mock` - deterministic mock Provider for contract
  tests, capability declarations such as chat/embedding, fixed responses, and
  ProviderRouter behavior checks.
- `AIKernel.Demo.Vfs.Git` - Git-backed VFS demo that mounts a Git repository as
  a virtual file system and demonstrates RAG MaterialContext separation.
- `AIKernel.Demo.Pipelines` - purpose-built Chat, RAG, Reasoning, and
  Multi-step/Multi-model routing pipeline examples.
- `AIKernel.Demo.PDP` - PDP visualization for Allow, Deny, Require-Review, cost
  limit policies, external-send guards, and audit logs.
- `AIKernel.Demo.ReplayInspector` - deterministic replay inspection surface for
  ReplayLog loading, provider selection replay, PromptRules version comparison,
  and ExecutionState diffs.

## Build

```powershell
dotnet build AIKernel.Demo.slnx
dotnet run --project src/AIKernel.Demo.Console/AIKernel.Demo.Console.csproj
dotnet run --project src/AIKernel.Demo.WebApi/AIKernel.Demo.WebApi.csproj
```

Common project properties are centralized in `Directory.Build.props`.

## Source Alignment

The demos intentionally mirror the Core design decisions: pipelines are DAGs
controlled deterministically by the TaskManager, Providers declare replaceable
Capabilities, LLMs propose while PDP makes final decisions, and replay captures
all material needed to rerun the same execution.
