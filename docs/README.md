# AIKernel.Demo Documentation

AIKernel.Demo is the runnable sample workspace for the AIKernel package family.
Use these pages to choose a demo, run it locally, and understand which runtime
repository owns the underlying behavior.

## Start Here

- [User Guide](user-guide/index.md)
- [Architecture](architecture/index.md)
- [Pipelines](pipelines/index.md)
- [Architecture 日本語](architecture/index-ja.md)
- [Pipelines 日本語](pipelines/index-ja.md)

## Demo Principles

- Demo code consumes AIKernel packages; it does not define runtime contracts.
- Deterministic replay and PDP examples are teaching surfaces for Core and
  Tools behavior.
- Runtime execution engines belong to Control, Providers, or Wasm repositories.
