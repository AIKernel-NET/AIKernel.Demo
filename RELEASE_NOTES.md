# AIKernel.Demo Release Notes

[日本語](RELEASE_NOTES-ja.md)

## 0.1.1

**June 10th, 2026 - Verifying the Semantic OS end-to-end.**
**2026年6月10日--Semantic OS を端から端まで検証する。**

Verifying the Semantic OS end-to-end: all eight demo layers validate the 0.1.1
semantic boundary. Semantic OS の端から端までの検証--8 つのデモ層が 0.1.1 の
セマンティック境界を検証する。

AIKernel.Demo 0.1.1 is the release validation workspace for the published
AIKernel 0.1.1 package family. It is not published as a NuGet or PyPI package;
instead, it demonstrates that the released Core, Control, Providers, Wasm, and
Tools packages compose through their public contracts.

- Align Demo package references to the official AIKernel 0.1.1 package family.
- Consume `AIKernel.Providers.Standard` and `AIKernel.Wasm.WebGpuComputeProvider`
  by their published package IDs.
- Keep Console, WebApi, Wasm, OS provider, VFS, PDP, pipeline, replay, and Python
  demo surfaces as contract-level examples.
- Preserve Demo as a readable release atlas for users validating the runtime
  after package registration.

## 0.1.0

> [EN] Demo 0.1.0 becomes the readable atlas of the runtime: every semantic boundary is shown, not hidden.
>
> [JA] Demo 0.1.0 はランタイムの可読アトラスへ──すべての意味境界は隠されず、可視化される。

AIKernel.Demo 0.1.0 is the teaching and validation workspace for the AIKernel
0.1.0 package family.

- Provide Console, WebApi, Wasm, Mock Provider, VFS Git, Pipelines, PDP,
  ReplayInspector, DSL, Monad LINQ, and Python demo surfaces.
- Demonstrate contract-pure DTO usage across Demo, Control, Tools, Core, and
  AIKernel.NET without depending on internal implementation types.
- Show DSL parsing, AST traversal, DSL-to-graph conversion, Result monad LINQ
  composition, deterministic routing, policy decisions, semantic deltas,
  ReplayLog, HashChain, VFS snapshots, and fail-closed summaries.
- Include bilingual educational comments and documentation for C# and Python
  demo code.
- Keep Demo as a consumer of runtime contracts rather than an execution engine.

Demo 0.1.0 is designed to be read: it turns the semantic runtime into a visible
atlas for users, implementers, and future capability authors.
