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
from typing import Iterable


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoRoutingDecision:
    """[EN]
    Represents the DemoRoutingDecision public Python API surface.
    
    [JA]
    DemoRoutingDecision の公開 Python API サーフェスを表します。
    """
    provider_id: str
    model_id: str
    route_reason: str


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoRoutingPolicy:
    """[EN]
    Represents the DemoRoutingPolicy public Python API surface.
    
    [JA]
    DemoRoutingPolicy の公開 Python API サーフェスを表します。
    """
    LOCAL_THRESHOLD = 32

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def route(input_size: int, provider_tiers: Iterable[str]) -> DemoRoutingDecision:
        """[EN]
        Executes the route operation.
        Args:
            input_size: Input value for route.
            provider_tiers: Input value for route.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        route 操作を実行します。
        引数:
            input_size: route に渡す入力値です。
            provider_tiers: route に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        tiers = set(provider_tiers)
        if input_size <= DemoRoutingPolicy.LOCAL_THRESHOLD and "local" in tiers:
            return DemoRoutingDecision("demo.local", "local-fixed", "local-threshold")
        if input_size > DemoRoutingPolicy.LOCAL_THRESHOLD and "remote" in tiers:
            return DemoRoutingDecision("demo.remote", "remote-fixed", "remote-threshold")
        return DemoRoutingDecision("demo.mock", "mock-fixed", "no-matching-provider-tier")
