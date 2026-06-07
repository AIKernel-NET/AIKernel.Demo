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

from dataclasses import dataclass, field
from types import MappingProxyType
from typing import Mapping

from .slot import DemoSemanticSlot


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoSemanticState:
    """[EN]
    Represents the DemoSemanticState public Python API surface.
    
    [JA]
    DemoSemanticState の公開 Python API サーフェスを表します。
    """
    orchestration_state: Mapping[str, str] = field(default_factory=dict)
    expression_state: Mapping[str, str] = field(default_factory=dict)
    material_state: Mapping[str, str] = field(default_factory=dict)

    @property
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def values(self) -> Mapping[str, str]:
        """[EN]
        Executes the values operation.
        Returns:
            Result produced by the operation.
        
        [JA]
        values 操作を実行します。
        戻り値:
            操作によって生成される結果です。
        """
        return self.orchestration_state

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def apply(self, delta) -> DemoSemanticState:
        """[EN]
        Executes the apply operation.
        Args:
            delta: Input value for apply.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        apply 操作を実行します。
        引数:
            delta: apply に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        orchestration = dict(self.orchestration_state)
        expression = dict(self.expression_state)
        material = dict(self.material_state)
        target = {
            DemoSemanticSlot.ORCHESTRATION: orchestration,
            DemoSemanticSlot.EXPRESSION: expression,
            DemoSemanticSlot.MATERIAL: material,
        }[delta.slot]
        target[delta.step_name] = delta.after
        return DemoSemanticState(
            MappingProxyType(dict(sorted(orchestration.items()))),
            MappingProxyType(dict(sorted(expression.items()))),
            MappingProxyType(dict(sorted(material.items()))),
        )

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def from_deltas(deltas) -> DemoSemanticState:
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
        state = DemoSemanticState()
        for delta in deltas:
            state = state.apply(delta)
        return state
