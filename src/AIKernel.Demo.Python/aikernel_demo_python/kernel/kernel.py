from __future__ import annotations

from aikernel_demo_python.execution import DemoExecutionEngine, DemoExecutionGraph, DemoExecutionNode


class DemoTaskManager:
    def schedule(self, graph: DemoExecutionGraph) -> tuple[DemoExecutionNode, ...]:
        return tuple(graph.traverse())


class DemoProviderRouter:
    def route(self):
        from aikernel_demo_python.routing import DemoRoutingDecision

        return DemoRoutingDecision("mock", "demo", "demo-routing")


class DemoLlmController:
    @staticmethod
    def generate(step_name: str) -> str:
        return f"llm-output({step_name})"


class DemoKernel:
    def __init__(self) -> None:
        self._task_manager = DemoTaskManager()
        self._provider_router = DemoProviderRouter()
        self._llm_controller = DemoLlmController()
        self._execution_engine = DemoExecutionEngine()

    async def execute_async(self, graph: DemoExecutionGraph):
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
