# Demo Pipelines

[日本語](index-ja.md)

AIKernel.Demo.Pipelines provides purpose-specific DAG examples:

- `ChatPipeline` - normalized user input, structure phase, Provider call, output
  polishing, and replay emission.
- `RAGPipeline` - MaterialContext separation through VFS-backed content.
- `ReasoningPipeline` - StructurePhase followed by ExecutionPhase.
- `MultiModelPipeline` - model vector routing and Provider selection.
- `DslParserDemo` - line-based DSL parsing into `DslDocument` and
  `PipelineRootNode` contract DTOs.
- `DemoPipelineMonad` - Result/Task<Result<T>> composition using LINQ query
  syntax and fail-closed short-circuit behavior.

## Contract-Aligned Skeleton

The first skeleton remains intentionally deterministic and contract-pure:

1. Apply a mock PDP decision.
2. Execute a mock provider call.
3. Emit a replay summary.

The default DSL path contains four semantic steps:

1. `normalize`
2. `structure`
3. `provider`
4. `polish`

`StepCount` is derived from the parsed DSL document so the replay summary stays
aligned with the semantic structure.

## Monad LINQ Teaching Surface

The monad demo intentionally exposes the shape users should copy when composing
AIKernel pipelines:

```csharp
from normalized in NormalizeAsync(input)
from compiled in CompileAsync(normalized)
from executed in ExecuteAsync(compiled)
select executed;
```

Each step is designed to be replaceable with AIKernel.Core Result/ResultStep
pipelines while preserving deterministic replay. Python mirrors the same
contract semantics with `Result.bind` and async demo helpers.
