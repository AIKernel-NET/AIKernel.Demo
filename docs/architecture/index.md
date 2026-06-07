# AIKernel.Demo Architecture

AIKernel.Demo is a demonstration workspace, not a contract owner. It composes
published AIKernel packages and external Capability modules to show the
Knowledge OS execution model in console, API-host, and browser-facing forms.

## Initial Boundaries

- Console: local smoke execution for one deterministic inference pipeline.
- WebApi: OpenAI-compatible server/API host demo with ProviderRouter and PDP.
- Wasm: browser-facing Playground for Context, Execution, VFS, DAG steps, and
  PromptRules signature checks.
- Providers.Mock: deterministic Provider behavior for contract and router demos.
- Vfs.Git: Git-backed VFS Provider boundary for MaterialContext/RAG separation.
- Pipelines: Chat, RAG, Reasoning, Multi-step, and Multi-model DAG examples.
- PDP: policy decision examples where LLMs propose and PDP decides.
- ReplayInspector: replay-log readability and deterministic rerun inspection.

Execution engines belong in AIKernel.Control. AIKernel.Demo demonstrates how to
consume them without taking a runtime dependency direction back into demos.
