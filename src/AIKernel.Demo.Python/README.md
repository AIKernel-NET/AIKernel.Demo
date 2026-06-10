# AIKernel.Demo.Python

Python teaching/demo port of the AIKernel.Demo C# samples.

This is not a production runtime. It is a compact, contract-pure demonstration
of the AIKernel 0.1.1 ideas:

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

## Notes

The code intentionally avoids framework-heavy abstractions. It mirrors the C#
demo semantics closely enough for documentation, onboarding, and prototype
validation, while remaining easy to inspect in a single sitting.
