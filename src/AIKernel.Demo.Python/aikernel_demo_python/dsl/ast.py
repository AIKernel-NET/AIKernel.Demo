"""
AIKernel Demo Reference Module

[EN]
Purpose:
    DSL reference code that maps simple text into deterministic semantic structure.

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
    単純なテキストを決定論的な semantic structure へ写像する DSL 参照コードです。

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


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class StepNode:
    """[EN]
    Represents the StepNode public Python API surface.
    
    [JA]
    StepNode の公開 Python API サーフェスを表します。
    """
    name: str


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class PipelineRootNode:
    """[EN]
    Represents the PipelineRootNode public Python API surface.
    
    [JA]
    PipelineRootNode の公開 Python API サーフェスを表します。
    """
    steps: tuple[StepNode, ...]


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DslDocument:
    """[EN]
    Represents the DslDocument public Python API surface.
    
    [JA]
    DslDocument の公開 Python API サーフェスを表します。
    """
    root: PipelineRootNode
