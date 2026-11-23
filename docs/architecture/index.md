# AIKernel.Demo Architecture

[日本語](index-ja.md)

AIKernel.Demo is a demonstration workspace, not a contract owner. It composes
published AIKernel packages and external Provider / Capability modules to show
the Knowledge OS execution model in console, API-host, and browser-facing forms.

## Initial Boundaries

- Console: local smoke execution for one deterministic inference pipeline.
- WebApi: ASP.NET Core host for a fixed pipeline summary at `GET /demo/pipeline`,
  with a redirect from `GET /`. See [Web API Demo](../../README.md#web-api-demo).
- Wasm: browser-facing Playground for Context, Execution, VFS, DAG steps, and
  PromptRules signature checks.
- CoreRuntime: Core routing, capability registry, kernel clock, VFS, Hosting,
  Security, and Kernel helper surfaces.
- Contracts: Contracts, DTOs, Enums, and execution HashChain DTO boundary.
- Control: Control/Bonsai governance through deterministic emulator execution.
- Providers: official external Provider dry-run surfaces for ChatHistory,
  ChatOpenAI, CudaCompute, LocalLlm, MicrosoftAI, and DynamicPipelineCompiler.
- StandardProviders: standard OS driver surfaces for file system, logging,
  network, event bus, and profiler.
- Tools: instrumentation, canonical formatting, replay, inspector, ROM, clock,
  and VFS helper surfaces.
- Cuda: verifies a VRAM round trip through the CUDA Driver bundled with `AIKernel.Kernel`.
- Providers.Mock: deterministic Provider behavior for contract and router demos.
- Vfs.Git: Git-backed VFS Provider boundary for MaterialContext/RAG separation.
- Pipelines: Chat, RAG, Reasoning, Multi-step, and Multi-model DAG examples.
- PDP: policy decision examples where LLMs propose and PDP decides.
- ReplayInspector: replay-log readability and deterministic rerun inspection.

AIKernel.Demo consumes public package surfaces only. Core contracts and
deterministic behavior belong in AIKernel.Core, physical execution engines in
AIKernel.Control, standard OS drivers in AIKernel.Providers, browser/WASM runtime
behavior in AIKernel.Wasm, and CLI/tooling behavior in AIKernel.Tools.
