from __future__ import annotations

import re

from aikernel_demo_python.control_boundary import (
    ControlExecutionRequest,
    ControlExecutionResult,
    ControlStateSnapshot,
)
from aikernel_demo_python.execution import DemoExecutionGraph
from aikernel_demo_python.pdp import DemoPolicyCode, DemoPolicyDecision, DemoPolicyEngine, DemoPolicyRule
from aikernel_demo_python.routing import DemoRoutingPolicy


def test_policy_decision_is_typed_and_null_safe() -> None:
    allow = DemoPolicyDecision.allow(None)
    deny = DemoPolicyDecision.deny("")
    assert allow.allowed and allow.code is DemoPolicyCode.ALLOW and allow.reason == ""
    assert not deny.allowed and deny.code is DemoPolicyCode.DENY and deny.reason == ""


def test_policy_engine_fails_closed_on_first_matching_rule() -> None:
    engine = DemoPolicyEngine([DemoPolicyRule(10, "rule1"), DemoPolicyRule(4, "rule2")])
    decision = engine.evaluate("hello")
    assert not decision.allowed
    assert decision.reason == "rule2"


def test_routing_policy_selects_expected_provider() -> None:
    small = DemoRoutingPolicy.route(4, ["local", "remote"])
    large = DemoRoutingPolicy.route(64, ["local", "remote"])
    fallback = DemoRoutingPolicy.route(8, ["edge"])
    assert small.provider_id == "demo.local"
    assert large.provider_id == "demo.remote"
    assert fallback.provider_id == "demo.mock"
    assert fallback.model_id == "mock-fixed"
    assert fallback.route_reason == "no-matching-provider-tier"
    assert re.fullmatch(r"^[a-z0-9-]+$", fallback.route_reason)


def test_control_boundary_uses_contract_dtos() -> None:
    graph = DemoExecutionGraph.from_steps(["normalize"])
    request = ControlExecutionRequest("exec-1", graph.graph_id, {"tenant_id": "demo"})
    snapshot = ControlStateSnapshot(request.execution_id, graph.nodes[0].id, {"node_id": "01"})
    result = ControlExecutionResult(request.execution_id, "Completed", {"replay_hash": "abc"})
    assert request.graph_id == "demo.graph.default"
    assert re.fullmatch(r"^[a-z0-9_]+$", next(iter(snapshot.metadata.keys())))
    assert result.status == "Completed"


def test_capability_operation_names_are_canonical_and_unique() -> None:
    operations = ["chat.completion", "pipeline.validate", "tensor.layernorm"]
    assert len(operations) == len(set(operations))
    assert all(" " not in op for op in operations)
    assert all(re.fullmatch(r"^[a-z0-9]+(\.[a-z0-9]+)*$", op) for op in operations)


def test_operator_ids_follow_canonical_format() -> None:
    graph = DemoExecutionGraph.from_steps(["normalize", "provider"])
    assert all(re.fullmatch(r"^[a-z0-9]+(\.[a-z0-9]+)*$", node.operator_id) for node in graph.nodes)
