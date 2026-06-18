"""
AIKernel Demo Reference Module

[EN]
Purpose:
    Replay reference code for deterministic metadata and hash-chain inspection.

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
    決定論的 metadata と hash-chain inspection のための replay 参照コードです。

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

import hashlib


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoHashChain:
    """[EN]
    Represents the DemoHashChain public Python API surface.
    
    [JA]
    DemoHashChain の公開 Python API サーフェスを表します。
    """
    GENESIS_HASH = "GENESIS"

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def compute_entry_hash(entry_payload: str) -> str:
        """[EN]
        Executes the compute entry hash operation.
        Args:
            entry_payload: Input value for compute entry hash.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        compute entry hash 操作を実行します。
        引数:
            entry_payload: compute entry hash に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return DemoHashChain._sha256(entry_payload)

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def compute_next(previous_hash: str, entry_hash: str) -> str:
        """[EN]
        Executes the compute next operation.
        Args:
            previous_hash: Input value for compute next.
            entry_hash: Input value for compute next.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        compute next 操作を実行します。
        引数:
            previous_hash: compute next に渡す入力値です。
            entry_hash: compute next に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return DemoHashChain._sha256(previous_hash + entry_hash)

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def canonicalize_payload(step_name: str, delta_summary: str, timestamp: str) -> str:
        """[EN]
        Executes the canonicalize payload operation.
        Args:
            step_name: Input value for canonicalize payload.
            delta_summary: Input value for canonicalize payload.
            timestamp: Input value for canonicalize payload.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        canonicalize payload 操作を実行します。
        引数:
            step_name: canonicalize payload に渡す入力値です。
            delta_summary: canonicalize payload に渡す入力値です。
            timestamp: canonicalize payload に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return "\n".join(
            [
                f"delta_summary={DemoHashChain._normalize(delta_summary)}",
                f"step_name={DemoHashChain._normalize(step_name)}",
                f"timestamp={DemoHashChain._normalize(timestamp)}",
            ]
        )

    @staticmethod
    def _sha256(value: str) -> str:
        return hashlib.sha256(value.encode("utf-8")).hexdigest()

    @staticmethod
    def _normalize(value: str) -> str:
        return value.replace("\r\n", "\n").replace("\r", "\n").strip()
