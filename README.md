# AIKernel.Demo

[日本語 README](README-ja.md)

AIKernel.Demo is the user-facing sample workspace for the AIKernel 0.1.1 package
family. It makes AIKernel.Core abstractions visible through runnable console,
API-host, browser, VFS, PDP, pipeline, and replay-inspection demos.

In the AIOS SDK, AIKernel.Demo is the official examples workspace, similar to an
OS `/usr/share/examples` tree. It shows how users can assemble kernel runtime,
providers, control, WASM, GPU, and tools layers into their own AIOS
distribution.

AIKernel also provides an official AIOS distribution, codenamed
**AIKernel.Monolith**. Monolith has begun development as the standard AIOS that
integrates all SDK layers after the 0.1.x line stabilizes; Demo remains the
teaching surface for understanding those layers.

## Repository Role

AIKernel.Demo demonstrates how applications compose Core, Kernel, Capability
modules, VFS, PDP, pipelines, and Replay inspection without putting demo-only
code into contract or runtime repositories.

The repository marks the transition from the completed 0.1.0 prototype
validation phase to the 0.1.1 release line, where Demo consumes the published
Core, Control, Providers, Wasm, and Tools package contracts.

Demo is a consumer. Runtime execution engines belong in AIKernel.Control.
External Provider and Capability modules belong in AIKernel.Providers or their
own dedicated runtime repositories. CLI, replay, inspectors, and instrumentation
belong in AIKernel.Tools.

Release notes:

- [English](RELEASE_NOTES.md)
- [日本語](RELEASE_NOTES-ja.md)

## Projects

- `AIKernel.Demo.Console` - minimal CLI demo for one inference pipeline using
  Kernel/Provider DI, `IInputNormalizer -> StructurePhase -> Provider ->
  OutputPolisher`, deterministic replay logging, and selectable Mock/OpenAI/Local
  providers.
- `AIKernel.Demo.WebApi` - OpenAI-compatible API-host demo implementing
  `/v1/chat/completions` through AIKernel, capability-based ProviderRouter
  selection, PDP Allow/Deny, and ReplayLog HTTP response headers.
- `AIKernel.Demo.Wasm` - browser Playground for Context, Execution, VFS
  visualization, WebAssembly provider calls, WebGPU CPU fallback, DAG step
  animation, and PromptRules signature verification.
- `AIKernel.Demo.Os` - standard OS provider demo that consumes
  `AIKernel.Providers.Standard` for CPU compute, process supervision, and safe
  scheduler examples.
- `AIKernel.Demo.CoreRuntime` - Core runtime demo for routing, capability
  registry, clock, VFS, Hosting, and Kernel helper surfaces.
- `AIKernel.Demo.Contracts` - public Contracts, DTOs, Enums, and execution
  HashChain DTOs as data-only boundary objects.
- `AIKernel.Demo.Control` - Control/Bonsai emulator demo for deterministic
  governance execution.
- `AIKernel.Demo.Providers` - official external Provider dry-run demo for
  ChatHistory, ChatOpenAI, CudaCompute, LocalLlm, MicrosoftAI, and
  DynamicPipelineCompiler.
- `AIKernel.Demo.StandardProviders` - standard OS driver demo for file system,
  logging, event bus, network metadata, and profiler surfaces.
- `AIKernel.Demo.Tools` - instrumentation, canonical formatting, replay,
  inspector, ROM, and export helper demo.
- `AIKernel.Demo.Cuda` - Windows-native CUDA 13.0 dry-run demo with deterministic
  skip outside Windows.
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

- [Documentation index](docs/README.md)
- [User Guide](docs/user-guide/index.md)
- [Architecture](docs/architecture/index.md)
- [Pipelines](docs/pipelines/index.md)

Japanese:

- [Documentation index 日本語](docs/README-ja.md)
- [User Guide 日本語](docs/user-guide/index-ja.md)
- [Architecture 日本語](docs/architecture/index-ja.md)
- [Pipelines 日本語](docs/pipelines/index-ja.md)

## Quick Start

Start with the release build, then run the smallest demos that prove the public
0.1.1 package surfaces are available. These commands do not require external
network access, secrets, model downloads, or native CUDA hardware.

First demo to run: `AIKernel.Demo.CoreRuntime`.

```powershell
dotnet build AIKernel.Demo.slnx -c Release
dotnet run --project src/AIKernel.Demo.CoreRuntime/AIKernel.Demo.CoreRuntime.csproj -c Release
dotnet run --project src/AIKernel.Demo.Contracts/AIKernel.Demo.Contracts.csproj -c Release
dotnet run --project src/AIKernel.Demo.StandardProviders/AIKernel.Demo.StandardProviders.csproj -c Release
```

Common project properties are centralized in `Directory.Build.props`.

## Run the Demo Map

Use this order when you want to understand the 0.1.1 package family as an
OS-shaped runtime rather than as isolated samples.

| Step | Demo | What the user should learn |
| --- | --- | --- |
| 1 | `AIKernel.Demo.CoreRuntime` | How routing, capability registration, VFS, clock, hosting, and kernel helper surfaces fit together. |
| 2 | `AIKernel.Demo.Contracts` | How DTOs, enums, orchestration context, policy results, and execution hash-chain values stay data-only. |
| 3 | `AIKernel.Demo.Control` | How deterministic governance execution is represented through Control and Bonsai surfaces. |
| 4 | `AIKernel.Demo.Providers` | How official extension Providers expose descriptors, IDs, and invokers without performing live external calls. |
| 5 | `AIKernel.Demo.StandardProviders` | How standard OS drivers cover file system, logging, event bus, network metadata, and profiler surfaces. |
| 6 | `AIKernel.Demo.Tools` | How canonical formatting, inspection, replay, ROM, and export helpers support reproducible diagnostics. |
| 7 | `AIKernel.Demo.Wasm` | How the browser/WASM runtime surface is tested through process lifecycle and deterministic WebGPU fallback paths. |
| 8 | `AIKernel.Demo.Cuda` | How Windows-native CUDA package contracts are visible while non-Windows environments skip deterministically. |

Run the console demos in one pass:

```powershell
dotnet run --project src/AIKernel.Demo.Control/AIKernel.Demo.Control.csproj -c Release
dotnet run --project src/AIKernel.Demo.Providers/AIKernel.Demo.Providers.csproj -c Release
dotnet run --project src/AIKernel.Demo.Tools/AIKernel.Demo.Tools.csproj -c Release
dotnet run --project src/AIKernel.Demo.Cuda/AIKernel.Demo.Cuda.csproj -c Release
```

Run the validation tests:

```powershell
dotnet test AIKernel.Demo.slnx -c Release --no-build
py -m pytest tests/AIKernel.Demo.Python.Tests
```

## CLI Tool

`AIKernel.Tools.CLI` is published as a .NET tool package, so it is not consumed
through `PackageReference` by the in-process demo projects. Install it as the
`aik` command and run the OS command surface directly:

```powershell
dotnet tool install -g AIKernel.Tools.CLI --version 0.1.1
aik runtime ping
aik system info
aik system vfs --vfs-root .
aik capabilities list
```

## Source Alignment

The demos mirror the Core design decisions: pipelines are DAGs controlled
deterministically by the TaskManager, Providers declare replaceable
Capabilities, LLMs propose while PDP makes final decisions, and replay captures
all material needed to rerun the same execution.

The 0.1.1 release includes contract-alignment smoke paths in
`AIKernel.Demo.Contracts` and `AIKernel.Demo.Pipelines`: they construct execution
hash-chain data through `AIKernel.Dtos.Execution.HashChain`, routing data through
`AIKernel.Dtos.Routing.KernelProviderRoutingDecision`, and DSL semantic IR
through `AIKernel.Dtos.Dsl`. Demo code remains a consumer of AIKernel.NET
contracts and does not depend on Core internal DSL/History runtime types.

For the 0.1.1 release, `AIKernelPackageVersion` and the Core, Control, Cuda,
Providers, Wasm, and Tools package version properties point to the published
0.1.1 package family. Demo remains a release validation workspace rather than a
package that is published independently.

## Contributor Guidelines

Demo changes must follow the shared AIKernel development discipline:

- [AIKernel Development Guidelines](../AIKernel.NET/docs/guidelines/AIKERNEL_DEVELOPMENT_GUIDELINES.md)
- [AIKernel 開発ガイドライン](../AIKernel.NET/docs/guidelines/AIKERNEL_DEVELOPMENT_GUIDELINES-jp.md)

Demos should remain consumers of public contracts, avoid depending on internal
runtime types, preserve deterministic replay examples, and document any package
version assumptions used for local validation.

## License

Apache License 2.0.
