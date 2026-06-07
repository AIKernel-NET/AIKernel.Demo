from __future__ import annotations

from dataclasses import dataclass

from aikernel_demo_python.dsl import DslDocument, PipelineRootNode, StepNode
from aikernel_demo_python.pdp import DemoPolicyCode, DemoPolicyDecision


@dataclass(frozen=True)
class DemoContractAlignment:
    routing_provider_id: str
    routing_model_id: str
    dsl_root_type: str
    dsl_first_step_name: str


@dataclass(frozen=True)
class DemoPipelineRun:
    pipeline_id: str
    decision: DemoPolicyCode
    replay_hash: str
    step_count: int
    contract_alignment: DemoContractAlignment


class DemoPipelineCatalog:
    @staticmethod
    def create_default_run() -> DemoPipelineRun:
        decision = DemoPolicyDecision.allow("demo-readonly")
        document = DslDocument(
            PipelineRootNode(
                tuple(StepNode(name) for name in ("normalize", "structure", "provider", "polish"))
            )
        )
        pipeline = document.root
        return DemoPipelineRun(
            pipeline_id="demo.pipeline.default",
            decision=decision.code,
            replay_hash="demo-replay-hash",
            step_count=len(pipeline.steps),
            contract_alignment=DemoContractAlignment(
                "demo.mock",
                "mock-fixed",
                type(pipeline).__name__,
                pipeline.steps[0].name,
            ),
        )
