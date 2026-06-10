"""
AIKernel Demo Reference Module

[EN]
Purpose:
    Monad-style pipeline reference code for composing fail-closed demo steps.

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
    fail-closed な Demo step を合成する monad-style pipeline 参照コードです。

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

from aikernel_demo_python.dsl import DslDocument, PipelineRootNode, StepNode
from aikernel_demo_python.pdp import DemoPolicyCode, DemoPolicyDecision


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoContractAlignment:
    """[EN]
    Represents the DemoContractAlignment public Python API surface.
    
    [JA]
    DemoContractAlignment の公開 Python API サーフェスを表します。
    """
    routing_provider_id: str
    routing_model_id: str
    dsl_root_type: str
    dsl_first_step_name: str


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoPipelineRun:
    """[EN]
    Represents the DemoPipelineRun public Python API surface.
    
    [JA]
    DemoPipelineRun の公開 Python API サーフェスを表します。
    """
    pipeline_id: str
    decision: DemoPolicyCode
    replay_hash: str
    step_count: int
    contract_alignment: DemoContractAlignment


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoPipelineCatalog:
    """[EN]
    Represents the DemoPipelineCatalog public Python API surface.

    [JA]
    DemoPipelineCatalog の公開 Python API サーフェスを表します。
    """

    @staticmethod
    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def create_default_run() -> DemoPipelineRun:
        """[EN]
        Executes the create default run operation.
        Returns:
            Result produced by the operation.
        
        [JA]
        create default run 操作を実行します。
        戻り値:
            操作によって生成される結果です。
        """
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
