"""
AIKernel Demo Reference Module

[EN]
Purpose:
    Monad-style pipeline reference code for composing fail-closed demo steps.

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
    fail-closed な Demo step を合成する monad-style pipeline 参照コードです。

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

from aikernel_demo_python.result import Result


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoPipelineMonad:
    """[EN]
    Represents the DemoPipelineMonad public Python API surface.

    [JA]
    DemoPipelineMonad の公開 Python API サーフェスを表します。
    """

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    async def run_async(input_text: str | None) -> Result[str]:
        """[EN]
        Executes the run async operation.
        Args:
            input_text: Input value for run async.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        run async 操作を実行します。
        引数:
            input_text: run async に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return (
            await DemoPipelineMonad._normalize_async(input_text)
        ).bind(DemoPipelineMonad._compile).bind(DemoPipelineMonad._execute)

    @staticmethod
    async def _normalize_async(input_text: str | None) -> Result[str]:
        if input_text is None:
            return Result.fail("Pipeline input is required.")
        return Result.ok(input_text.strip())

    @staticmethod
    def _compile(normalized: str) -> Result[str]:
        return Result.ok(f"compiled({normalized})")

    @staticmethod
    def _execute(compiled: str) -> Result[str]:
        return Result.ok(f"executed({compiled})")
