# Demo Pipelines

[English](index.md)

AIKernel.Demo.Pipelines は用途別 DAG example を提供します。

- `ChatPipeline` - normalized user input、structure phase、Provider call、
  output polishing、replay emission
- `RAGPipeline` - VFS-backed content による MaterialContext separation
- `ReasoningPipeline` - StructurePhase の後に ExecutionPhase を実行
- `MultiModelPipeline` - model vector routing と Provider selection

最初の skeleton は意図的に決定論的です。

1. Mock PDP decision を適用します。
2. Mock provider call を実行します。
3. Replay summary を emit します。

各 step は AIKernel.Core の Result / ResultStep pipeline に置き換え可能であり、
deterministic replay を維持するよう設計されています。
