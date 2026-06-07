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


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class ReplaySummaryFormatter:
    """[EN]
    Represents the ReplaySummaryFormatter public Python API surface.

    [JA]
    ReplaySummaryFormatter の公開 Python API サーフェスを表します。
    """

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def format(replay_hash: str | None, step_count: int) -> str:
        """[EN]
        Executes the format operation.
        Args:
            replay_hash: Input value for format.
            step_count: Input value for format.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        format 操作を実行します。
        引数:
            replay_hash: format に渡す入力値です。
            step_count: format に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        if not replay_hash:
            return "steps=0; hash=invalid"
        short_hash = replay_hash[:8] if len(replay_hash) > 8 else replay_hash
        return f"steps={step_count}; hash={short_hash}"
