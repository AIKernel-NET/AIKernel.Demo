# AIKernel.Demo.Python

Python teaching/demo port of the AIKernel.Demo C# samples.

This is not a production runtime. It is a compact, contract-pure demonstration
of the AIKernel 0.1.2 ideas:

- Interface-Led Architecture through small DTOs and `Protocol` contracts.
- Deterministic Replay through `DemoSemanticDelta`, `DemoReplayLog`, and
  `DemoHashChain`.
- Semantic Runtime separation through orchestration, expression, and material
  slots.
- Fail-closed parsing, policy, routing, replay formatting, and pipeline
  composition.

## Run

From the `AIKernel.Demo` repository root:

```powershell
py -m pytest tests/AIKernel.Demo.Python.Tests
```

The tests add `src/AIKernel.Demo.Python` to `sys.path` through `conftest.py`,
so no package install is required for local validation.

## Modules

| Module | Role |
| --- | --- |
| `release_surfaces` | One-to-one Python counterparts for the eight 0.1.2 C# golden-path demos. |
| `execution` | Deterministic graph and node DTOs plus the tiny execution engine. |
| `kernel` | Task scheduling, provider routing, LLM output mock, and kernel run DTO. |
| `pipelines` | Default pipeline catalog and Python monad-style composition demo. |
| `pdp` | Fail-closed allow/deny policy DTOs and engine. |
| `replay` | Semantic delta, entry hash, chain hash, timestamp, and replay log. |
| `semantics` | Slot-separated semantic state. |
| `routing` | Deterministic local/remote/mock provider routing. |
| `vfs` | Immutable snapshot, canonical paths, and deterministic snapshot hash. |
| `dsl` | Minimal line-based DSL parser and DSL-to-graph conversion. |
| `providers.mock` | Deterministic SHA-256 mock provider. |
| `replay_inspector` | Fail-closed replay summary formatting. |
| `control_boundary` | Python `Protocol`/DTO equivalents for Control boundary demos. |

## C# / Python Pairing

The 0.1.2 release demos are paired across languages. C# projects demonstrate
the package-facing .NET surface, while `release_surfaces` gives Python readers
the same dry-run teaching map without external network calls, native CUDA
loading, model downloads, or browser WebGPU requirements.

| C# demo | Python counterpart |
| --- | --- |
| `AIKernel.Demo.CoreRuntime` | `run_core_runtime_demo()` |
| `AIKernel.Demo.Contracts` | `run_contracts_demo()` |
| `AIKernel.Demo.Control` | `run_control_demo()` |
| `AIKernel.Demo.Providers` | `run_providers_demo()` |
| `AIKernel.Demo.StandardProviders` | `run_standard_providers_demo()` |
| `AIKernel.Demo.Tools` | `run_tools_demo()` |
| `AIKernel.Demo.Wasm` | `run_wasm_demo()` |
| `AIKernel.Demo.Cuda` | `run_cuda_demo()` |

## Notes

The code intentionally avoids framework-heavy abstractions. It mirrors the C#
demo semantics closely enough for documentation, onboarding, and prototype
validation, while remaining easy to inspect in a single sitting.
