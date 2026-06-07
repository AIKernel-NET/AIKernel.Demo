# AIKernel.Demo

[日本語 README](README-ja.md)

AIKernel.Demo is the user-facing sample workspace for the AIKernel 0.1.0 package
family. It makes AIKernel.Core abstractions visible through runnable console,
API-host, browser, VFS, PDP, pipeline, and replay-inspection demos.

## Repository Role

AIKernel.Demo demonstrates how applications compose Core, Kernel, Capability
modules, VFS, PDP, pipelines, and Replay inspection without putting demo-only
code into contract or runtime repositories.

The repository marks the transition from the completed 0.0.x
design-implementation phase to the 0.1.0 prototype validation phase scheduled
for 2026-06-09.

Demo is a consumer. Runtime execution engines belong in AIKernel.Control.
External Capability modules belong in AIKernel.Tools or dedicated Capability
repositories.

## Projects

- `AIKernel.Demo.Console` - minimal CLI demo for one inference pipeline using
  Kernel/Provider DI, `IInputNormalizer -> StructurePhase -> Provider ->
  OutputPolisher`, deterministic replay logging, and selectable Mock/OpenAI/Local
  providers.
- `AIKernel.Demo.WebApi` - OpenAI-compatible API-host demo implementing
  `/v1/chat/completions` through AIKernel, capability-based ProviderRouter
  selection, PDP Allow/Deny, and ReplayLog HTTP response headers.
- `AIKernel.Demo.Wasm` - browser Playground for Context, Execution, VFS
  visualization, WebAssembly provider calls, DAG step animation, and PromptRules
  signature verification.
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
- `AIKernel.Demo.Python` - Python teaching/demo port of the same contract
  semantics: DSL parsing, monad-style pipelines, VFS snapshots, semantic
  deltas, and deterministic replay.

## Documentation

- [Architecture](docs/architecture/index.md)
- [Pipelines](docs/pipelines/index.md)

Japanese:

- [Architecture 日本語](docs/architecture/index-ja.md)
- [Pipelines 日本語](docs/pipelines/index-ja.md)

## Build

```powershell
dotnet build AIKernel.Demo.slnx
dotnet run --project src/AIKernel.Demo.Console/AIKernel.Demo.Console.csproj
dotnet run --project src/AIKernel.Demo.WebApi/AIKernel.Demo.WebApi.csproj
py -m pytest tests/AIKernel.Demo.Python.Tests
```

Common project properties are centralized in `Directory.Build.props`.

## Source Alignment

The demos mirror the Core design decisions: pipelines are DAGs controlled
deterministically by the TaskManager, Providers declare replaceable
Capabilities, LLMs propose while PDP makes final decisions, and replay captures
all material needed to rerun the same execution.

The initial 0.1.0 prototype includes a contract-alignment smoke path in
`AIKernel.Demo.Pipelines`: it constructs routing data through
`AIKernel.Dtos.Routing.KernelProviderRoutingDecision` and DSL semantic IR through
`AIKernel.Dtos.Dsl`. Demo code remains a consumer of AIKernel.NET contracts and
does not depend on Core internal DSL/History runtime types.

During 0.1.0 prototype development, `AIKernelPackageVersion` may point to a
local build such as `0.1.0.2` to avoid NuGet cache collisions. Public release
builds should align the package family to the fixed 0.1.0 release version.

## License

Apache License 2.0.
