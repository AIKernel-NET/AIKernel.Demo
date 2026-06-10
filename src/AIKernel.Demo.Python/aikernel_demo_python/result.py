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
from typing import Callable, Generic, TypeVar

T = TypeVar("T")
U = TypeVar("U")


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class Result(Generic[T]):
    """[EN]
    Represents the Result public Python API surface.
    
    [JA]
    Result の公開 Python API サーフェスを表します。
    """
    value: T | None = None
    error: str | None = None

    @property
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def is_success_state(self) -> bool:
        """[EN]
        Executes the is success state operation.
        Returns:
            Result produced by the operation.
        
        [JA]
        is success state 操作を実行します。
        戻り値:
            操作によって生成される結果です。
        """
        return self.error is None

    @property
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def is_failure_state(self) -> bool:
        """[EN]
        Executes the is failure state operation.
        Returns:
            Result produced by the operation.
        
        [JA]
        is failure state 操作を実行します。
        戻り値:
            操作によって生成される結果です。
        """
        return self.error is not None

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def map(self, mapper: Callable[[T], U]) -> Result[U]:
        """[EN]
        Executes the map operation.
        Args:
            mapper: Input value for map.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        map 操作を実行します。
        引数:
            mapper: map に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        if self.is_failure_state:
            return Result.fail(self.error or "Unknown failure.")
        try:
            return Result.ok(mapper(self.value))  # type: ignore[arg-type]
        except Exception as exc:  # fail-closed demo boundary
            return Result.fail(str(exc))

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def bind(self, binder: Callable[[T], Result[U]]) -> Result[U]:
        """[EN]
        Executes the bind operation.
        Args:
            binder: Input value for bind.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        bind 操作を実行します。
        引数:
            binder: bind に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        if self.is_failure_state:
            return Result.fail(self.error or "Unknown failure.")
        try:
            return binder(self.value)  # type: ignore[arg-type]
        except Exception as exc:
            return Result.fail(str(exc))

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def ok(value: T) -> Result[T]:
        """[EN]
        Executes the ok operation.
        Args:
            value: Input value for ok.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        ok 操作を実行します。
        引数:
            value: ok に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return Result(value=value)

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def fail(error: str) -> Result[T]:
        """[EN]
        Executes the fail operation.
        Args:
            error: Input value for fail.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        fail 操作を実行します。
        引数:
            error: fail に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return Result(error=error)
