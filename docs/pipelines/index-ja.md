# Demo Pipelines

[English](index.md)

AIKernel.Demo.Pipelines は用途別 DAG example を提供します。

- `ChatPipeline` - normalized user input、structure phase、Provider call、
  output polishing、replay emission
- `RAGPipeline` - VFS-backed content による MaterialContext separation
- `ReasoningPipeline` - StructurePhase の後に ExecutionPhase を実行
- `MultiModelPipeline` - model vector routing と Provider selection
- `DslParserDemo` - 行ベース DSL を `DslDocument` と `PipelineRootNode`
  contract DTO へ変換
- `DemoPipelineMonad` - LINQ query syntax と fail-closed short-circuit を使う
  Result / Task<Result<T>> composition demo

## Contract-Aligned Skeleton

最初の skeleton は意図的に決定論的かつ contract-pure です。

1. Mock PDP decision を適用します。
2. Mock provider call を実行します。
3. Replay summary を emit します。

既定の DSL path は 4 つの semantic step を含みます。

1. `normalize`
2. `structure`
3. `provider`
4. `polish`

`StepCount` は parsed DSL document から導出されるため、replay summary は
semantic structure と一致します。

## Monad LINQ Teaching Surface

Monad demo は、AIKernel pipeline を合成する利用者が写経できる形を意図的に公開します。

```csharp
from normalized in NormalizeAsync(input)
from compiled in CompileAsync(normalized)
from executed in ExecuteAsync(compiled)
select executed;
```

各 step は AIKernel.Core の Result / ResultStep pipeline に置き換え可能であり、
deterministic replay を維持するよう設計されています。Python 版も同じ contract
semantics を `Result.bind` と async demo helper で反映します。
