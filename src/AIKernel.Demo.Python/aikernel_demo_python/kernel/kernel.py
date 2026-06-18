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

from aikernel_demo_python.execution import DemoExecutionEngine, DemoExecutionGraph, DemoExecutionNode


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoTaskManager:
    """[EN]
    Represents the DemoTaskManager public Python API surface.
    
    [JA]
    DemoTaskManager の公開 Python API サーフェスを表します。
    """
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def schedule(self, graph: DemoExecutionGraph) -> tuple[DemoExecutionNode, ...]:
        """[EN]
        Executes the schedule operation.
        Args:
            graph: Input value for schedule.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        schedule 操作を実行します。
        引数:
            graph: schedule に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return tuple(graph.traverse())


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoProviderRouter:
    """[EN]
    Represents the DemoProviderRouter public Python API surface.
    
    [JA]
    DemoProviderRouter の公開 Python API サーフェスを表します。
    """
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def route(self):
        """[EN]
        Executes the route operation.
        Returns:
            None.
        
        [JA]
        route 操作を実行します。
        戻り値:
            ありません。
        """
        from aikernel_demo_python.routing import DemoRoutingDecision

        return DemoRoutingDecision("mock", "demo", "demo-routing")


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoLlmController:
    """[EN]
    Represents the DemoLlmController public Python API surface.

    [JA]
    DemoLlmController の公開 Python API サーフェスを表します。
    """

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def generate(step_name: str) -> str:
        """[EN]
        Executes the generate operation.
        Args:
            step_name: Input value for generate.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        generate 操作を実行します。
        引数:
            step_name: generate に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return f"llm-output({step_name})"


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoKernel:
    """[EN]
    Represents the DemoKernel public Python API surface.
    
    [JA]
    DemoKernel の公開 Python API サーフェスを表します。
    """
    def __init__(self) -> None:
        self._task_manager = DemoTaskManager()
        self._provider_router = DemoProviderRouter()
        self._llm_controller = DemoLlmController()
        self._execution_engine = DemoExecutionEngine()

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    async def execute_async(self, graph: DemoExecutionGraph):
        """[EN]
        Executes the execute async operation.
        Args:
            graph: Input value for execute async.
        
        Returns:
            None.
        
        [JA]
        execute async 操作を実行します。
        引数:
            graph: execute async に渡す入力値です。
        
        戻り値:
            ありません。
        """
        scheduled = self._task_manager.schedule(graph)
        routing = self._provider_router.route()
        outputs = tuple(self._llm_controller.generate(node.step_name) for node in scheduled)
        run = self._execution_engine.execute(graph)
        return run.__class__(
            semantic_deltas=run.semantic_deltas,
            replay_log=run.replay_log,
            provider_id=routing.provider_id,
            model_id=routing.model_id,
            route_reason=routing.route_reason,
            provider_outputs=outputs,
        )
