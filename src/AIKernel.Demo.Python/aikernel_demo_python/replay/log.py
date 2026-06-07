from __future__ import annotations

from dataclasses import dataclass
from typing import Iterable

from .delta import DemoSemanticDelta
from .hash_chain import DemoHashChain


@dataclass(frozen=True)
class DemoReplayEntry:
    step_name: str
    delta_summary: str
    entry_hash: str
    chain_hash: str
    timestamp: str

    @property
    def hash(self) -> str:
        return self.chain_hash


@dataclass(frozen=True)
class DemoReplayLog:
    entries: tuple[DemoReplayEntry, ...]
    final_hash: str

    @staticmethod
    def from_deltas(deltas: Iterable[DemoSemanticDelta]) -> DemoReplayLog:
        previous = DemoHashChain.GENESIS_HASH
        entries: list[DemoReplayEntry] = []
        for index, delta in enumerate(deltas):
            summary = f"{delta.before}->{delta.after}"
            timestamp = f"T+{index:04d}"
            payload = DemoHashChain.canonicalize_payload(delta.step_name, summary, timestamp)
            entry_hash = DemoHashChain.compute_entry_hash(payload)
            chain_hash = DemoHashChain.compute_next(previous, entry_hash)
            entries.append(DemoReplayEntry(delta.step_name, summary, entry_hash, chain_hash, timestamp))
            previous = chain_hash
        return DemoReplayLog(tuple(entries), previous)
