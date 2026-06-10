"""
AIKernel Demo Reference Module

[EN]
Purpose:
    Python reference implementation mirroring the AIKernel demo architecture.

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
    AIKernel Demo architecture を Python で対応させた参照実装です。

実行仕様と前提条件:
    この module は軽量かつ決定論的であることを意図しています。AIKernel
    0.1.1 release line の学習教材として、semantic structure、policy
    decision、routing、execution、replay の境界が見えるようにしています。

アーキテクチャ意図:
    Python Demo は C# Demo の公開サーフェスに対応しますが、内部 runtime
    責務は複製しません。各 public object は、semantic runtime 全体の中の
    小さな contract-facing step として読める必要があります。
"""

from .contracts import (
    ControlExecutionRequest,
    ControlExecutionResult,
    ControlStateSnapshot,
    IExecutionGraph,
    IExecutionNode,
)

__all__ = [
    "ControlExecutionRequest",
    "ControlExecutionResult",
    "ControlStateSnapshot",
    "IExecutionGraph",
    "IExecutionNode",
]
