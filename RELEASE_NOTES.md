# AIKernel.Demo Release Notes

[日本語](RELEASE_NOTES-ja.md)

## 0.1.3

**June 23, 2026 - The visible OS learns to see the GPU.**
**2026年6月23日--可視化された OS が GPU の目を得る。**

AIKernel.Demo 0.1.3 updates the release-validation atlas for the canonical GPU
rev3 design. The Demo repository remains a consumer of public contracts, but it
now includes executable documentation for raw-framebuffer Aisthesis, spatial
reasoning matrices, and offscreen HUD composition.

- Align Demo package references to the canonical AIKernel 0.1.3 package family.
- Add the Wasm GPU rev3 teaching path for `gpu.aisthesis.raw-frame`,
  `gpu.spatial-reasoning`, and `gpu.hud.composite`.
- Add `AIKernel.Demo.Gpu`, a direct console entry point for the same canonical
  GPU rev3 pass sequence.
- Keep the WebGPU demo deterministic by exercising the rev3 pass vocabulary
  through CPU fallback in automated tests.
- Extend Python release-surface parity so the Wasm demo exposes GPU rev3 pass
  IDs, raw framebuffer capture, offscreen HUD composition, and
  `topos,route,threat,zoe` matrix order.

## 0.1.2

**June 16, 2026 - Philosophy Becomes Experience.**
**2026年6月16日--哲学が体験として立ち上がる。**

AIKernel.Demo turns AIKernel concepts, abstractions, and contracts into runnable
experience. AIKernel.Demo は、AIKernel の概念・抽象・契約を実行可能な体験へ
変換する。

AIKernel.Demo 0.1.2 is the release validation workspace for the published
AIKernel 0.1.2 package family. It is not published as a NuGet or PyPI package;
instead, it demonstrates that the released Core, Control, Providers, Wasm, and
Tools packages compose through their public contracts.

- Align Demo package references to the official AIKernel 0.1.2 package family.
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
