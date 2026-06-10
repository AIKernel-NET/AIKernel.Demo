# AIKernel.Demo リリースノート

[English](RELEASE_NOTES.md)

## 0.1.1

> [EN] Demo 0.1.1 validates the published AIKernel package family as one readable runtime surface.
>
> [JA] Demo 0.1.1 は、公開済み AIKernel package family をひとつの可読 runtime surface として検証する。

AIKernel.Demo 0.1.1 は、公開済み AIKernel 0.1.1 package family の release validation
workspace です。NuGet / PyPI package として公開する対象ではなく、公開済みの Core、
Control、Providers、Wasm、Tools package が public contract 経由で合成できることを示します。

- Demo の package reference を公式 AIKernel 0.1.1 package family に揃えました。
- `AIKernel.Providers.Standard` と `AIKernel.Wasm.WebGpuComputeProvider` を公開済み
  package ID で消費します。
- Console、WebApi、Wasm、OS provider、VFS、PDP、pipeline、replay、Python demo surface を
  contract-level example として維持します。
- Package 登録後に runtime を検証するための、読める release atlas として Demo を保ちます。

## 0.1.0

> [EN] Demo 0.1.0 becomes the readable atlas of the runtime: every semantic boundary is shown, not hidden.
>
> [JA] Demo 0.1.0 はランタイムの可読アトラスへ──すべての意味境界は隠されず、可視化される。

AIKernel.Demo 0.1.0 は、AIKernel 0.1.0 package family の teaching / validation workspace です。

- Console、WebApi、Wasm、Mock Provider、VFS Git、Pipelines、PDP、ReplayInspector、DSL、Monad LINQ、Python demo surface を提供します。
- Demo、Control、Tools、Core、AIKernel.NET の間で、internal implementation type に依存しない contract-pure DTO usage を示します。
- DSL parsing、AST traversal、DSL-to-graph conversion、Result monad LINQ composition、deterministic routing、policy decision、semantic delta、ReplayLog、HashChain、VFS snapshot、fail-closed summary を可視化します。
- C# / Python demo code には、教材として読める bilingual comment / documentation を含めます。
- Demo は runtime contract の consumer であり、execution engine ではありません。

Demo 0.1.0 は読まれるためのデモです。Semantic Runtime を、利用者・実装者・将来の Capability author に向けた可視アトラスへ変換します。
