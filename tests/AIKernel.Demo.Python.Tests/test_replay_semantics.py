from __future__ import annotations

import re

from aikernel_demo_python.replay import DemoHashChain, DemoReplayLog, DemoSemanticDelta
from aikernel_demo_python.semantics import DemoSemanticSlot, DemoSemanticState


def test_replay_hash_chain_is_deterministic() -> None:
    deltas = [DemoSemanticDelta("normalize", "pending", "completed")]
    first = DemoReplayLog.from_deltas(deltas)
    second = DemoReplayLog.from_deltas(deltas)
    assert first.final_hash == second.final_hash
    assert first.entries[0].entry_hash == second.entries[0].entry_hash
    assert first.entries[0].chain_hash == second.entries[0].chain_hash


def test_changing_delta_changes_final_hash() -> None:
    first = DemoReplayLog.from_deltas([DemoSemanticDelta("normalize", "pending", "completed")])
    second = DemoReplayLog.from_deltas([DemoSemanticDelta("provider", "pending", "completed")])
    assert first.final_hash != second.final_hash


def test_entry_hash_chain_hash_and_timestamp_are_separate() -> None:
    entry = DemoReplayLog.from_deltas([DemoSemanticDelta("normalize", "pending", "completed")]).entries[0]
    assert entry.entry_hash != entry.chain_hash
    assert re.fullmatch(r"^T\+\d{4}$", entry.timestamp)


def test_canonical_payload_normalizes_whitespace() -> None:
    a = DemoHashChain.canonicalize_payload(" step\r\n", " a\r\nb ", " T+0000 ")
    b = DemoHashChain.canonicalize_payload("step", "a\nb", "T+0000")
    assert a == b


def test_semantic_state_updates_slots_and_is_immutable() -> None:
    first = DemoSemanticState()
    second = first.apply(DemoSemanticDelta("normalize", "pending", "done"))
    third = second.apply(DemoSemanticDelta("provider", "pending", "ready", DemoSemanticSlot.EXPRESSION))
    assert first.values == {}
    assert list(second.values.keys()) == ["normalize"]
    assert third.orchestration_state["normalize"] == "done"
    assert third.expression_state["provider"] == "ready"


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
