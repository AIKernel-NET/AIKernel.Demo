"""
AIKernel Demo Release Surfaces

[EN]
Purpose:
    One-to-one Python teaching surfaces for the AIKernel.Demo 0.1.2 C# demo
    projects.

Runtime Specifications and Prerequisites:
    These helpers are deterministic dry-run demos. They do not call external
    services, download models, load native CUDA libraries, or require browser
    WebGPU.

Architectural Intent:
    The C# demo projects are executable package-coverage samples. This module
    mirrors their public teaching intent in Python so readers can compare the
    same CoreRuntime, Contracts, Control, Providers, StandardProviders, Tools,
    Wasm, and Cuda concepts across both languages.

[JA]
目的:
    AIKernel.Demo 0.1.2 の C# demo project に 1 対 1 で対応する Python 教材面です。

実行仕様と前提条件:
    これらの helper は決定論的な dry-run demo です。外部 service、model download、
    native CUDA library、browser WebGPU を要求しません。

アーキテクチャ意図:
    C# demo project は package coverage を示す実行可能 sample です。この module は
    CoreRuntime、Contracts、Control、Providers、StandardProviders、Tools、Wasm、
    Cuda の同じ概念を Python 側でも比較できるようにします。
"""

from __future__ import annotations

from dataclasses import dataclass

from aikernel_demo_python.control_boundary import (
    ControlExecutionRequest,
    ControlExecutionResult,
    ControlStateSnapshot,
)
from aikernel_demo_python.dsl import DslParserDemo, DslToGraphDemo
from aikernel_demo_python.execution import DemoExecutionGraph
from aikernel_demo_python.kernel import DemoKernel
from aikernel_demo_python.pdp import DemoPolicyCode, DemoPolicyDecision, DemoPolicyEngine, DemoPolicyRule
from aikernel_demo_python.pipelines import DemoPipelineCatalog
from aikernel_demo_python.providers.mock import MockProvider
from aikernel_demo_python.replay import DemoHashChain, DemoReplayLog, DemoSemanticDelta
from aikernel_demo_python.replay_inspector import ReplaySummaryFormatter
from aikernel_demo_python.routing import DemoRoutingPolicy
from aikernel_demo_python.vfs import DemoVfsSnapshot


@dataclass(frozen=True)
class DemoSurfaceResult:
    """[EN]
    Represents one Python release-surface demo result.

    [JA]
    Python release-surface demo の 1 件分の結果を表します。
    """

    name: str
    csharp_project: str
    python_surface: str
    lines: tuple[str, ...]


def run_core_runtime_demo() -> DemoSurfaceResult:
    """[EN]
    Mirrors AIKernel.Demo.CoreRuntime with routing, VFS, Kernel, and pipeline
    boundaries.

    [JA]
    routing、VFS、Kernel、pipeline 境界を使って AIKernel.Demo.CoreRuntime に
    対応します。
    """

    route = DemoRoutingPolicy.route(4, ["local", "remote"])
    snapshot = DemoVfsSnapshot().add_file("/demo/readme.txt", "AIKernel core runtime demo")
    pipeline = DemoPipelineCatalog.create_default_run()
    return DemoSurfaceResult(
        "CoreRuntime",
        "AIKernel.Demo.CoreRuntime",
        "aikernel_demo_python.release_surfaces.run_core_runtime_demo",
        (
            "AIKernel.Demo.CoreRuntime",
            f"router.provider={route.provider_id}",
            f"vfs.exists={'/demo/readme.txt' in snapshot.files}",
            f"pipeline.id={pipeline.pipeline_id}",
            "kernel.surface=DemoKernel",
        ),
    )


def run_contracts_demo() -> DemoSurfaceResult:
    """[EN]
    Mirrors AIKernel.Demo.Contracts with DTO-like immutable boundary values.

    [JA]
    DTO 風の immutable boundary value を使って AIKernel.Demo.Contracts に
    対応します。
    """

    hash_chain = DemoHashChain.compute_next(
        "sha256:generation-parent",
        DemoHashChain.compute_entry_hash("sha256:structure"),
    )
    decision = DemoPolicyDecision.allow("demo")
    return DemoSurfaceResult(
        "Contracts",
        "AIKernel.Demo.Contracts",
        "aikernel_demo_python.release_surfaces.run_contracts_demo",
        (
            "AIKernel.Demo.Contracts",
            "context.id=demo.context",
            f"decision.allowed={decision.allowed}",
            f"hashchain.prefix={hash_chain[:12]}",
            "contracts=UnifiedContextDto,PolicyEvaluationResult,HashChain",
        ),
    )


def run_control_demo() -> DemoSurfaceResult:
    """[EN]
    Mirrors AIKernel.Demo.Control with fail-closed policy and Control DTOs.

    [JA]
    fail-closed policy と Control DTO を使って AIKernel.Demo.Control に対応します。
    """

    graph = DemoExecutionGraph.from_steps(["prepare"])
    request = ControlExecutionRequest("demo.control.execution", graph.graph_id, {"mode": "dry-run"})
    snapshot = ControlStateSnapshot(request.execution_id, graph.nodes[0].id, {"phase": "prepare"})
    result = ControlExecutionResult(request.execution_id, "Completed", {"replay_hash": "demo"})
    policy = DemoPolicyEngine([DemoPolicyRule(99, "allow")]).evaluate("ok")
    return DemoSurfaceResult(
        "Control",
        "AIKernel.Demo.Control",
        "aikernel_demo_python.release_surfaces.run_control_demo",
        (
            "AIKernel.Demo.Control",
            f"graph={request.graph_id}",
            f"snapshot.node={snapshot.node_id}",
            f"status={result.status}",
            f"policy.allowed={policy.allowed}",
        ),
    )


def run_providers_demo() -> DemoSurfaceResult:
    """[EN]
    Mirrors AIKernel.Demo.Providers with provider IDs and deterministic mock
    invocation.

    [JA]
    provider ID と deterministic mock invocation を使って AIKernel.Demo.Providers
    に対応します。
    """

    response = MockProvider().generate("inspect the kernel")
    return DemoSurfaceResult(
        "Providers",
        "AIKernel.Demo.Providers",
        "aikernel_demo_python.release_surfaces.run_providers_demo",
        (
            "AIKernel.Demo.Providers",
            "chat-history.provider=chat.history",
            "openai.provider=openai.chat",
            "local-llm.provider=local.llm",
            "compiler.capability=dynamic.pipeline.compiler",
            "cuda.capability=cuda.compute",
            f"mock.output={response.output_hash[:12]}",
        ),
    )


def run_standard_providers_demo() -> DemoSurfaceResult:
    """[EN]
    Mirrors AIKernel.Demo.StandardProviders with in-memory OS-driver surfaces.

    [JA]
    in-memory OS driver surface を使って AIKernel.Demo.StandardProviders に
    対応します。
    """

    snapshot = DemoVfsSnapshot().add_file("/demo/readme.txt", "AIKernel standard provider demo")
    event_observed = len("ok")
    return DemoSurfaceResult(
        "StandardProviders",
        "AIKernel.Demo.StandardProviders",
        "aikernel_demo_python.release_surfaces.run_standard_providers_demo",
        (
            "AIKernel.Demo.StandardProviders",
            f"fs.exists={'/demo/readme.txt' in snapshot.files}",
            f"fs.text={snapshot.read_file('/demo/readme.txt')}",
            f"event.observed={event_observed}",
            "profiler.memory=deterministic",
        ),
    )


def run_tools_demo() -> DemoSurfaceResult:
    """[EN]
    Mirrors AIKernel.Demo.Tools with canonical replay and inspection surfaces.

    [JA]
    canonical replay と inspection surface を使って AIKernel.Demo.Tools に対応します。
    """

    replay = DemoReplayLog.from_deltas([DemoSemanticDelta("boot", "pending", "completed")])
    summary = ReplaySummaryFormatter.format(replay.final_hash, len(replay.entries))
    return DemoSurfaceResult(
        "Tools",
        "AIKernel.Demo.Tools",
        "aikernel_demo_python.release_surfaces.run_tools_demo",
        (
            "AIKernel.Demo.Tools",
            f"replay.events={len(replay.entries)}",
            f"replay.hash={replay.final_hash[:12]}",
            f"inspect.length={len(summary)}",
            "export.surface=markdown,rom",
        ),
    )


def run_wasm_demo() -> DemoSurfaceResult:
    """[EN]
    Mirrors AIKernel.Demo.Wasm with deterministic process and WebGPU fallback
    vocabulary.

    [JA]
    deterministic process と WebGPU fallback vocabulary を使って AIKernel.Demo.Wasm
    に対応します。
    """

    return DemoSurfaceResult(
        "Wasm",
        "AIKernel.Demo.Wasm",
        "aikernel_demo_python.release_surfaces.run_wasm_demo",
        (
            "AIKernel.Demo.Wasm",
            "process.state=Stopped",
            "memory.surface=linear",
            "fs.surface=wasi-bridge",
            "webgpu.fallback=cpu",
        ),
    )


def run_cuda_demo(is_windows: bool = False) -> DemoSurfaceResult:
    """[EN]
    Mirrors AIKernel.Demo.Cuda with deterministic non-Windows skip behavior.

    [JA]
    deterministic non-Windows skip behavior を使って AIKernel.Demo.Cuda に対応します。
    """

    status = "dry-run" if is_windows else "skipped"
    reason = "Windows CUDA dry-run" if is_windows else "CUDA package is Windows native"
    return DemoSurfaceResult(
        "Cuda",
        "AIKernel.Demo.Cuda",
        "aikernel_demo_python.release_surfaces.run_cuda_demo",
        (
            "AIKernel.Demo.Cuda",
            f"status={status}",
            f"reason={reason}",
            "capability=aikernel.cuda13.libtorch2_12.win_x64",
            "request.valid=true",
        ),
    )


def run_all_release_surfaces() -> tuple[DemoSurfaceResult, ...]:
    """[EN]
    Runs the Python counterparts for all eight AIKernel.Demo 0.1.2 C# demos.

    [JA]
    AIKernel.Demo 0.1.2 の 8 つの C# demo に対応する Python surface をすべて
    実行します。
    """

    return (
        run_core_runtime_demo(),
        run_contracts_demo(),
        run_control_demo(),
        run_providers_demo(),
        run_standard_providers_demo(),
        run_tools_demo(),
        run_wasm_demo(),
        run_cuda_demo(),
    )
