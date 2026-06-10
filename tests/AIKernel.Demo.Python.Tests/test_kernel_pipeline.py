"""
AIKernel Demo Reference Module

[EN]
Purpose:
    Executable documentation for Python demo behavior and contract boundaries.

Runtime Specifications and Prerequisites:
    This module is intentionally lightweight and deterministic. It is used as a
    learning artifact for the AIKernel 0.1.1 release line, where semantic
    structure, policy decisions, routing, execution, and replay are kept visible.

Architectural Intent:
    The Python demo mirrors the C# demo surface without copying internal runtime
    responsibilities. Each public object should be understandable as a small
    contract-facing step in the larger semantic runtime.

[JA]
目的:
    Python Demo の振る舞いと契約境界を固定する実行可能ドキュメントです。

実行仕様と前提条件:
    この module は軽量かつ決定論的であることを意図しています。AIKernel
    0.1.1 release line の学習教材として、semantic structure、policy
    decision、routing、execution、replay の境界が見えるようにしています。

アーキテクチャ意図:
    Python Demo は C# Demo の公開サーフェスに対応しますが、内部 runtime
    責務は複製しません。各 public object は、semantic runtime 全体の中の
    小さな contract-facing step として読める必要があります。
"""

from __future__ import annotations

import re
import asyncio

from aikernel_demo_python.dsl import DslParserDemo, DslToGraphDemo
from aikernel_demo_python.execution import DemoExecutionGraph
from aikernel_demo_python.kernel import DemoKernel
from aikernel_demo_python.pdp import DemoPolicyCode
from aikernel_demo_python.pipelines import DemoPipelineCatalog, DemoPipelineMonad, DemoPipelineRun


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_kernel_executes_all_nodes_in_order() -> None:
    graph = DemoExecutionGraph.from_steps(["normalize", "structure", "provider"])
    run = asyncio.run(DemoKernel().execute_async(graph))
    assert [d.step_name for d in run.semantic_deltas] == ["normalize", "structure", "provider"]


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_kernel_produces_expected_semantic_deltas() -> None:
    run = asyncio.run(DemoKernel().execute_async(DemoExecutionGraph.from_steps(["normalize"])))
    delta = run.semantic_deltas[0]
    assert delta.before == "pending"
    assert delta.after == "completed:01-normalize"


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_kernel_replay_hash_is_deterministic() -> None:
    graph = DemoExecutionGraph.from_steps(["normalize", "structure", "provider"])
    first = asyncio.run(DemoKernel().execute_async(graph))
    second = asyncio.run(DemoKernel().execute_async(graph))
    assert first.final_hash == second.final_hash


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_kernel_keeps_routing_and_provider_outputs() -> None:
    graph = DemoExecutionGraph.from_steps(["normalize", "provider"])
    run = asyncio.run(DemoKernel().execute_async(graph))
    assert (run.provider_id, run.model_id, run.route_reason) == ("mock", "demo", "demo-routing")
    assert run.provider_outputs == ("llm-output(normalize)", "llm-output(provider)")


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_default_pipeline_publishes_contract_alignment() -> None:
    run = DemoPipelineCatalog.create_default_run()
    assert isinstance(run, DemoPipelineRun)
    assert run.pipeline_id == "demo.pipeline.default"
    assert run.decision is DemoPolicyCode.ALLOW
    assert run.replay_hash == "demo-replay-hash"
    assert run.step_count == 4
    assert run.contract_alignment.routing_provider_id == "demo.mock"
    assert run.contract_alignment.routing_model_id == "mock-fixed"
    assert run.contract_alignment.dsl_root_type == "PipelineRootNode"
    assert run.contract_alignment.dsl_first_step_name == "normalize"
    assert re.fullmatch(r"^[a-z0-9]+(\.[a-z0-9]+)*$", run.pipeline_id)


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_run_async_composes_pipeline_with_linq_equivalent() -> None:
    result = asyncio.run(DemoPipelineMonad.run_async(" hello "))
    assert result.is_success_state
    assert result.value == "executed(compiled(hello))"


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_run_async_fails_closed_when_input_is_null() -> None:
    result = asyncio.run(DemoPipelineMonad.run_async(None))
    assert result.is_failure_state
    assert "Pipeline input is required." in (result.error or "")


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_dsl_parser_and_graph_conversion() -> None:
    parsed = DslParserDemo.parse("normalize\nstructure\nprovider\npolish")
    assert parsed.is_success_state
    assert [step.name for step in parsed.value.root.steps] == [  # type: ignore[union-attr]
        "normalize",
        "structure",
        "provider",
        "polish",
    ]
    graph = DslToGraphDemo.convert(parsed.value)  # type: ignore[arg-type]
    assert graph.is_success_state
    assert [node.id for node in graph.value.nodes] == [  # type: ignore[union-attr]
        "01-normalize",
        "02-structure",
        "03-provider",
        "04-polish",
    ]
