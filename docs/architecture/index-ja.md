# AIKernel.Demo Architecture

[English](index.md)

AIKernel.Demo は demonstration workspace であり、contract owner ではありません。
公開済みの AIKernel package と外部 Capability module を合成し、console、API host、
browser-facing surface で Knowledge OS execution model を示します。

## Initial Boundaries

- Console: 1 つの deterministic inference pipeline の local smoke execution
- WebApi: ProviderRouter と PDP を持つ OpenAI-compatible server / API host demo
- Wasm: Context、Execution、VFS、DAG step、PromptRules signature check の
  browser-facing Playground
- Providers.Mock: contract と router demo のための deterministic Provider behavior
- Vfs.Git: MaterialContext / RAG separation のための Git-backed VFS Provider boundary
- Pipelines: Chat、RAG、Reasoning、Multi-step、Multi-model DAG examples
- PDP: LLM が提案し PDP が決定する policy decision examples
- ReplayInspector: replay-log readability と deterministic rerun inspection

Execution engine は AIKernel.Control に属します。AIKernel.Demo は、それらを
runtime dependency direction を逆転させずに利用する方法を示します。
