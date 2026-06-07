"""
AIKernel Demo Reference Module

[EN]
Purpose:
    Replay reference code for deterministic metadata and hash-chain inspection.

Runtime Specifications and Prerequisites:
    This module is intentionally lightweight and deterministic. It is used as a
    learning artifact for the AIKernel 0.1.0 prototype line, where semantic
    structure, policy decisions, routing, execution, and replay are kept visible.

Architectural Intent:
    The Python demo mirrors the C# demo surface without copying internal runtime
    responsibilities. Each public object should be understandable as a small
    contract-facing step in the larger semantic runtime.

[JA]
目的:
    決定論的 metadata と hash-chain inspection のための replay 参照コードです。

実行仕様と前提条件:
    この module は軽量かつ決定論的であることを意図しています。AIKernel
    0.1.0 prototype line の学習教材として、semantic structure、policy
    decision、routing、execution、replay の境界が見えるようにしています。

アーキテクチャ意図:
    Python Demo は C# Demo の公開サーフェスに対応しますが、内部 runtime
    責務は複製しません。各 public object は、semantic runtime 全体の中の
    小さな contract-facing step として読める必要があります。
"""

from __future__ import annotations

from dataclasses import dataclass
from typing import Iterable

from .delta import DemoSemanticDelta
from .hash_chain import DemoHashChain


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoReplayEntry:
    """[EN]
    Represents the DemoReplayEntry public Python API surface.
    
    [JA]
    DemoReplayEntry の公開 Python API サーフェスを表します。
    """
    step_name: str
    delta_summary: str
    entry_hash: str
    chain_hash: str
    timestamp: str

    @property
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def hash(self) -> str:
        """[EN]
        Executes the hash operation.
        Returns:
            Result produced by the operation.
        
        [JA]
        hash 操作を実行します。
        戻り値:
            操作によって生成される結果です。
        """
        return self.chain_hash


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoReplayLog:
    """[EN]
    Represents the DemoReplayLog public Python API surface.
    
    [JA]
    DemoReplayLog の公開 Python API サーフェスを表します。
    """
    entries: tuple[DemoReplayEntry, ...]
    final_hash: str

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def from_deltas(deltas: Iterable[DemoSemanticDelta]) -> DemoReplayLog:
        """[EN]
        Executes the from deltas operation.
        Args:
            deltas: Input value for from deltas.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        from deltas 操作を実行します。
        引数:
            deltas: from deltas に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
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
