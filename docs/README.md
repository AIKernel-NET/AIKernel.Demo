# AIKernel.Demo Documentation

AIKernel.Demo is the runnable sample workspace for the AIKernel package family.
Use these pages to choose a demo, run it locally, and understand which runtime
repository owns the underlying behavior.

These docs describe Demo as the AIOS SDK official examples workspace, similar
to an OS `/usr/share/examples` tree. Demo shows how users assemble Core,
Providers, Control, Wasm, GPU, and Tools layers without making examples own
runtime behavior.

AIKernel.Monolith is the official AIOS distribution now in development. It is
planned as the standard reference distribution that integrates all SDK layers
after the 0.1.x line stabilizes; Demo remains the learning path for those layers.

## Cross-Repository Alignment

Shared repository boundaries, 0.1.1.1 local NuGet versioning, and the
NuGet-only / no-PyPI rule for this update line are defined by
[AIKernel Repository Alignment v0.1.1.1](https://github.com/AIKernel-NET/AIKernel.NET/blob/main/docs/development/repository-alignment-v0.1.1.1.md).

Demo owns runnable examples that consume the package family. It must not define
runtime contracts, production package ownership, or shared scenario semantics.

## Start Here

- [User Guide](user-guide/index.md)
- [User Guide 日本語](user-guide/index-ja.md)
- [Architecture](architecture/index.md)
- [Pipelines](pipelines/index.md)
- [Architecture 日本語](architecture/index-ja.md)
- [Pipelines 日本語](pipelines/index-ja.md)

## Which Page Should I Read?

- Read the User Guide when you want commands and expected demo behavior.
- Read Architecture when you want to understand repository ownership and why
  Demo does not implement runtime behavior.
- Read Pipelines when you want to understand the DAG-style sample execution
  model.

## Demo Principles

- Demo code consumes AIKernel packages; it does not define runtime contracts.
- Deterministic replay and PDP examples are teaching surfaces for Core and
  Tools behavior.
- Runtime contracts and deterministic core behavior belong to AIKernel.Core.
- Physical execution engines belong to AIKernel.Control.
- Standard providers and OS driver implementations belong to AIKernel.Providers.
- Browser and WebAssembly runtime behavior belongs to AIKernel.Wasm.
- CLI, replay, inspectors, and tooling belong to AIKernel.Tools.
