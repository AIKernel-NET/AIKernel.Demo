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

from aikernel_demo_python.replay import DemoReplayLog, DemoSemanticDelta

from .graph import DemoExecutionGraph, DemoExecutionNode


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoExecutionEngine:
    """[EN]
    Represents the DemoExecutionEngine public Python API surface.
    
    [JA]
    DemoExecutionEngine の公開 Python API サーフェスを表します。
    """
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def execute(self, graph: DemoExecutionGraph):
        """[EN]
        Executes the execute operation.
        Args:
            graph: Input value for execute.
        
        Returns:
            None.
        
        [JA]
        execute 操作を実行します。
        引数:
            graph: execute に渡す入力値です。
        
        戻り値:
            ありません。
        """
        from aikernel_demo_python.kernel.run import DemoKernelRun

        deltas = tuple(self._build_delta(node) for node in graph.nodes)
        return DemoKernelRun(
            semantic_deltas=deltas,
            replay_log=DemoReplayLog.from_deltas(deltas),
        )

    @staticmethod
    def _build_delta(node: DemoExecutionNode) -> DemoSemanticDelta:
        return DemoSemanticDelta(node.step_name, "pending", f"completed:{node.id}")
