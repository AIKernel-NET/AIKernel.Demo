"""
AIKernel Demo Reference Module

[EN]
Purpose:
    Executable documentation for Python demo behavior and contract boundaries.

Runtime Specifications and Prerequisites:
    This module is intentionally lightweight and deterministic. It is used as a
    learning artifact for the AIKernel 0.1.2 release line, where semantic
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
    0.1.2 release line の学習教材として、semantic structure、policy
    decision、routing、execution、replay の境界が見えるようにしています。

アーキテクチャ意図:
    Python Demo は C# Demo の公開サーフェスに対応しますが、内部 runtime
    責務は複製しません。各 public object は、semantic runtime 全体の中の
    小さな contract-facing step として読める必要があります。
"""

from __future__ import annotations

import re

from aikernel_demo_python.replay import DemoHashChain, DemoReplayLog, DemoSemanticDelta
from aikernel_demo_python.semantics import DemoSemanticSlot, DemoSemanticState


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_replay_hash_chain_is_deterministic() -> None:
    deltas = [DemoSemanticDelta("normalize", "pending", "completed")]
    first = DemoReplayLog.from_deltas(deltas)
    second = DemoReplayLog.from_deltas(deltas)
    assert first.final_hash == second.final_hash
    assert first.entries[0].entry_hash == second.entries[0].entry_hash
    assert first.entries[0].chain_hash == second.entries[0].chain_hash


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_changing_delta_changes_final_hash() -> None:
    first = DemoReplayLog.from_deltas([DemoSemanticDelta("normalize", "pending", "completed")])
    second = DemoReplayLog.from_deltas([DemoSemanticDelta("provider", "pending", "completed")])
    assert first.final_hash != second.final_hash


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_entry_hash_chain_hash_and_timestamp_are_separate() -> None:
    entry = DemoReplayLog.from_deltas([DemoSemanticDelta("normalize", "pending", "completed")]).entries[0]
    assert entry.entry_hash != entry.chain_hash
    assert re.fullmatch(r"^T\+\d{4}$", entry.timestamp)


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_canonical_payload_normalizes_whitespace() -> None:
    a = DemoHashChain.canonicalize_payload(" step\r\n", " a\r\nb ", " T+0000 ")
    b = DemoHashChain.canonicalize_payload("step", "a\nb", "T+0000")
    assert a == b


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_semantic_state_updates_slots_and_is_immutable() -> None:
    first = DemoSemanticState()
    second = first.apply(DemoSemanticDelta("normalize", "pending", "done"))
    third = second.apply(DemoSemanticDelta("provider", "pending", "ready", DemoSemanticSlot.EXPRESSION))
    assert first.values == {}
    assert list(second.values.keys()) == ["normalize"]
    assert third.orchestration_state["normalize"] == "done"
    assert third.expression_state["provider"] == "ready"


# EN: Test intent: executable documentation for a demo invariant. / JA: テスト意図: Demo invariant を実行可能な文書として固定します。
def test_semantic_state_sequence_order_matters() -> None:
    first = DemoSemanticState.from_deltas(
        [
            DemoSemanticDelta("normalize", "pending", "one"),
            DemoSemanticDelta("normalize", "one", "two"),
        ]
    )
    second = DemoSemanticState.from_deltas(
        [
            DemoSemanticDelta("normalize", "one", "two"),
            DemoSemanticDelta("normalize", "pending", "one"),
        ]
    )
    assert first.values["normalize"] != second.values["normalize"]
