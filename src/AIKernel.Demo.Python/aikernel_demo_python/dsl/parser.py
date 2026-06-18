"""
AIKernel Demo Reference Module

[EN]
Purpose:
    DSL reference code that maps simple text into deterministic semantic structure.

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
    単純なテキストを決定論的な semantic structure へ写像する DSL 参照コードです。

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

from aikernel_demo_python.result import Result

from .ast import DslDocument, PipelineRootNode, StepNode


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DslParserDemo:
    """[EN]
    Represents the DslParserDemo public Python API surface.
    
    [JA]
    DslParserDemo の公開 Python API サーフェスを表します。
    """
    allowed_steps: set[str] = {"normalize", "structure", "provider", "polish"}

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def parse(text: str) -> Result[DslDocument]:
        """[EN]
        Executes the parse operation.
        Args:
            text: Input value for parse.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        parse 操作を実行します。
        引数:
            text: parse に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        steps: list[StepNode] = []
        for raw_line in text.splitlines():
            name = raw_line.strip()
            if not name or name.startswith("#"):
                continue
            if name not in DslParserDemo.allowed_steps:
                return Result.fail(f"Unknown step: {name}")
            steps.append(StepNode(name))
        return Result.ok(DslDocument(PipelineRootNode(tuple(steps))))
