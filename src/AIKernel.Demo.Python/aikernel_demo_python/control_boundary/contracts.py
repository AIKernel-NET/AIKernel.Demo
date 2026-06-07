"""
AIKernel Demo Reference Module

[EN]
Purpose:
    Python reference implementation mirroring the AIKernel demo architecture.

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
    AIKernel Demo architecture を Python で対応させた参照実装です。

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
from typing import Protocol


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class IExecutionNode(Protocol):
    """[EN]
    Represents the IExecutionNode public Python API surface.
    
    [JA]
    IExecutionNode の公開 Python API サーフェスを表します。
    """
    id: str
    operator_id: str
    metadata: dict[str, str]


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class IExecutionGraph(Protocol):
    """[EN]
    Represents the IExecutionGraph public Python API surface.
    
    [JA]
    IExecutionGraph の公開 Python API サーフェスを表します。
    """
    graph_id: str
    nodes: tuple[IExecutionNode, ...]


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class ControlExecutionRequest:
    """[EN]
    Represents the ControlExecutionRequest public Python API surface.
    
    [JA]
    ControlExecutionRequest の公開 Python API サーフェスを表します。
    """
    execution_id: str
    graph_id: str
    metadata: dict[str, str]


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class ControlStateSnapshot:
    """[EN]
    Represents the ControlStateSnapshot public Python API surface.
    
    [JA]
    ControlStateSnapshot の公開 Python API サーフェスを表します。
    """
    execution_id: str
    node_id: str
    metadata: dict[str, str]


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class ControlExecutionResult:
    """[EN]
    Represents the ControlExecutionResult public Python API surface.
    
    [JA]
    ControlExecutionResult の公開 Python API サーフェスを表します。
    """
    execution_id: str
    status: str
    metadata: dict[str, str]
