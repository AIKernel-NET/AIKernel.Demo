from __future__ import annotations

from aikernel_demo_python.replay import DemoReplayLog, DemoSemanticDelta

from .graph import DemoExecutionGraph, DemoExecutionNode


class DemoExecutionEngine:
    def execute(self, graph: DemoExecutionGraph):
        from aikernel_demo_python.kernel.run import DemoKernelRun

        deltas = tuple(self._build_delta(node) for node in graph.nodes)
        return DemoKernelRun(
            semantic_deltas=deltas,
            replay_log=DemoReplayLog.from_deltas(deltas),
        )

    @staticmethod
    def _build_delta(node: DemoExecutionNode) -> DemoSemanticDelta:
        return DemoSemanticDelta(node.step_name, "pending", f"completed:{node.id}")
