# AIKernel.Demo Architecture

[English](index.md)

AIKernel.Demo は demonstration workspace であり、contract owner ではありません。
公開済みの AIKernel package と外部 Provider / Capability module を合成し、
console、API host、browser-facing surface で Knowledge OS execution model を示します。

## Initial Boundaries

- Console: 1 つの deterministic inference pipeline の local smoke execution
- WebApi: ProviderRouter と PDP を持つ OpenAI-compatible server / API host demo
- Wasm: Context、Execution、VFS、DAG step、PromptRules signature check の
  browser-facing Playground
- CoreRuntime: Core routing、capability registry、kernel clock、VFS、Hosting、
  Security、Kernel helper surface
- Contracts: Contracts、DTO、Enums、execution HashChain DTO boundary
- Control: deterministic emulator execution を通じた Control / Bonsai governance
- Providers: ChatHistory、ChatOpenAI、CudaCompute、LocalLlm、MicrosoftAI、
  DynamicPipelineCompiler の公式 external Provider dry-run surface
- StandardProviders: file system、logging、network、event bus、profiler の
  standard OS driver surface
- Tools: instrumentation、canonical formatting、replay、inspector、ROM、clock、
  VFS helper surface
- Cuda: Windows-native CUDA 13.0 descriptor / request dry-run。Windows 以外では
  deterministic skip
- Providers.Mock: contract と router demo のための deterministic Provider behavior
- Vfs.Git: MaterialContext / RAG separation のための Git-backed VFS Provider boundary
- Pipelines: Chat、RAG、Reasoning、Multi-step、Multi-model DAG examples
- PDP: LLM が提案し PDP が決定する policy decision examples
- ReplayInspector: replay-log readability と deterministic rerun inspection

AIKernel.Demo は public package surface のみを消費します。Core contract と
deterministic behavior は AIKernel.Core、physical execution engine は
AIKernel.Control、standard OS driver は AIKernel.Providers、browser / WASM runtime
behavior は AIKernel.Wasm、CLI / tooling behavior は AIKernel.Tools に属します。
