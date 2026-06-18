"""
AIKernel Demo Reference Module

[EN]
Purpose:
    Python reference implementation mirroring the AIKernel demo architecture.

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
    AIKernel Demo architecture を Python で対応させた参照実装です。

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

from dataclasses import dataclass
from enum import Enum
from typing import Iterable


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoPolicyCode(str, Enum):
    """[EN]
    Represents the DemoPolicyCode public Python API surface.
    
    [JA]
    DemoPolicyCode の公開 Python API サーフェスを表します。
    """
    ALLOW = "Allow"
    DENY = "Deny"


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoPolicyDecision:
    """[EN]
    Represents the DemoPolicyDecision public Python API surface.
    
    [JA]
    DemoPolicyDecision の公開 Python API サーフェスを表します。
    """
    allowed: bool
    code: DemoPolicyCode
    reason: str

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def allow(reason: str | None) -> DemoPolicyDecision:
        """[EN]
        Executes the allow operation.
        Args:
            reason: Input value for allow.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        allow 操作を実行します。
        引数:
            reason: allow に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return DemoPolicyDecision(True, DemoPolicyCode.ALLOW, reason or "")

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def deny(reason: str | None) -> DemoPolicyDecision:
        """[EN]
        Executes the deny operation.
        Args:
            reason: Input value for deny.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        deny 操作を実行します。
        引数:
            reason: deny に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return DemoPolicyDecision(False, DemoPolicyCode.DENY, reason or "")


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoPolicyRule:
    """[EN]
    Represents the DemoPolicyRule public Python API surface.
    
    [JA]
    DemoPolicyRule の公開 Python API サーフェスを表します。
    """
    max_length: int
    reason: str

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def evaluate(self, text: str) -> DemoPolicyDecision:
        """[EN]
        Executes the evaluate operation.
        Args:
            text: Input value for evaluate.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        evaluate 操作を実行します。
        引数:
            text: evaluate に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return (
            DemoPolicyDecision.allow("allowed")
            if len(text) <= self.max_length
            else DemoPolicyDecision.deny(self.reason)
        )


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoPolicyEngine:
    """[EN]
    Represents the DemoPolicyEngine public Python API surface.
    
    [JA]
    DemoPolicyEngine の公開 Python API サーフェスを表します。
    """
    def __init__(self, rules: Iterable[DemoPolicyRule] | None = None) -> None:
        self._rules = tuple(rules or (DemoPolicyRule(32, "input-too-large"),))

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def evaluate(self, text: str) -> DemoPolicyDecision:
        """[EN]
        Executes the evaluate operation.
        Args:
            text: Input value for evaluate.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        evaluate 操作を実行します。
        引数:
            text: evaluate に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        for rule in self._rules:
            decision = rule.evaluate(text)
            if not decision.allowed:
                return decision
        return DemoPolicyDecision.allow("allowed")
