# Demo Pipelines

AIKernel.Demo.Pipelines provides purpose-specific DAG examples:

- `ChatPipeline` - normalized user input, structure phase, Provider call, output
  polishing, and replay emission.
- `RAGPipeline` - MaterialContext separation through VFS-backed content.
- `ReasoningPipeline` - StructurePhase followed by ExecutionPhase.
- `MultiModelPipeline` - model vector routing and Provider selection.

The first skeleton remains intentionally deterministic:

1. Apply a mock PDP decision.
2. Execute a mock provider call.
3. Emit a replay summary.

Each step is designed to be replaceable with AIKernel.Core Result/ResultStep
pipelines while preserving deterministic replay.
