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

from __future__ import annotations

from dataclasses import dataclass
from typing import Iterable


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoExecutionNode:
    """[EN]
    Represents the DemoExecutionNode public Python API surface.
    
    [JA]
    DemoExecutionNode の公開 Python API サーフェスを表します。
    """
    id: str
    operator_id: str
    metadata: dict[str, str]

    @property
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def step_name(self) -> str:
        """[EN]
        Executes the step name operation.
        Returns:
            Result produced by the operation.
        
        [JA]
        step name 操作を実行します。
        戻り値:
            操作によって生成される結果です。
        """
        return self.id.split("-", 1)[1] if "-" in self.id else self.id


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoExecutionGraph:
    """[EN]
    Represents the DemoExecutionGraph public Python API surface.
    
    [JA]
    DemoExecutionGraph の公開 Python API サーフェスを表します。
    """
    graph_id: str
    nodes: tuple[DemoExecutionNode, ...]

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def from_steps(steps: Iterable[str]) -> DemoExecutionGraph:
        """[EN]
        Executes the from steps operation.
        Args:
            steps: Input value for from steps.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        from steps 操作を実行します。
        引数:
            steps: from steps に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        nodes = tuple(
            DemoExecutionNode(
                id=f"{index:02d}-{step}",
                operator_id=f"demo.operator.{step}",
                metadata={"step_name": step},
            )
            for index, step in enumerate(steps, start=1)
        )
        return DemoExecutionGraph("demo.graph.default", nodes)

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def traverse(self) -> tuple[DemoExecutionNode, ...]:
        """[EN]
        Executes the traverse operation.
        Returns:
            Result produced by the operation.
        
        [JA]
        traverse 操作を実行します。
        戻り値:
            操作によって生成される結果です。
        """
        return self.nodes
