"""Check README examples over HTTP after a Release build; no Python packages needed."""

from collections import deque
from contextlib import ExitStack, contextmanager
import http.client
import json
import os
from pathlib import Path
import queue
import re
import subprocess
import sys
import threading
import time
import unittest


ROOT = Path(__file__).resolve().parents[1]
READMES = ("README.md", "README-ja.md")


@contextmanager
def running_host():
    """Use a single owned process and an OS-assigned loopback port, then stop it."""
    dll = ROOT / "src/AIKernel.Demo.WebApi/bin/Release/net10.0/AIKernel.Demo.WebApi.dll"
    if not dll.is_file():
        raise RuntimeError("Run dotnet build AIKernel.Demo.slnx -c Release first.")
    env = dict(os.environ, ASPNETCORE_ENVIRONMENT="Production", DOTNET_ENVIRONMENT="Production")
    env["Logging__LogLevel__Microsoft.Hosting.Lifetime"] = "Information"
    process = subprocess.Popen(
        ["dotnet", str(dll), "--urls", "http://127.0.0.1:0"],
        cwd=dll.parent, env=env, stdin=subprocess.DEVNULL,
        stdout=subprocess.PIPE, stderr=subprocess.STDOUT,
        encoding="utf-8", errors="replace",
        creationflags=getattr(subprocess, "CREATE_NO_WINDOW", 0),
    )
    lines = queue.Queue()
    recent = deque(maxlen=30)

    def read_output():
        for line in process.stdout:
            lines.put(line)

    reader = threading.Thread(target=read_output, daemon=True)
    reader.start()
    try:
        deadline = time.monotonic() + 90
        while time.monotonic() < deadline:
            try:
                line = lines.get(timeout=0.2)
            except queue.Empty:
                if process.poll() is not None:
                    break
                continue
            recent.append(line)
            match = re.search(r"Now listening on: http://127\.0\.0\.1:(\d+)", line)
            if match:
                yield int(match.group(1))
                return
        raise RuntimeError("Web API did not become ready:\n" + "".join(recent))
    finally:
        if process.poll() is None:
            process.terminate()
            try:
                process.wait(timeout=10)
            except subprocess.TimeoutExpired:
                process.kill()
                process.wait(timeout=10)
        reader.join(timeout=5)
        process.stdout.close()


class WebApiDocumentationTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        stack = ExitStack()
        cls.addClassCleanup(stack.close)
        cls.port = stack.enter_context(running_host())

    def request(self, method, path):
        # HTTPConnection does not follow redirects or use proxy environment variables.
        connection = http.client.HTTPConnection("127.0.0.1", self.port, timeout=30)
        try:
            connection.request(method, path)
            response = connection.getresponse()
            body = response.read(1_048_577)
            self.assertLessEqual(len(body), 1_048_576)
            return response.status, {name.lower(): value for name, value in response.getheaders()}, body
        finally:
            connection.close()

    def section(self, readme):
        text = (ROOT / readme).read_text(encoding="utf-8")
        heading = "## Web API Demo\n"
        self.assertIn(heading, text, readme)
        return text.split(heading, 1)[1].split("\n## ", 1)[0]

    def test_readme_request_tables_match_http_statuses(self):
        expected_requests = {("GET", "/"), ("GET", "/demo/pipeline"), ("POST", "/v1/chat/completions")}
        for readme in READMES:
            with self.subTest(readme=readme):
                rows = re.findall(r"^\| `(GET|POST) ([^`]+)` \| `(\d{3})` \|", self.section(readme), re.MULTILINE)
                self.assertEqual(expected_requests, {(method, path) for method, path, _ in rows})
                self.assertEqual(len(expected_requests), len(rows))
                for method, path, expected in rows:
                    with self.subTest(method=method, path=path):
                        status, _, _ = self.request(method, path)
                        self.assertEqual(int(expected), status)

    def test_readme_json_examples_match_response(self):
        status, headers, body = self.request("GET", "/demo/pipeline")
        self.assertEqual(200, status)
        self.assertEqual("application/json", headers["content-type"].split(";")[0].strip().lower())
        actual = json.loads(body)
        for readme in READMES:
            with self.subTest(readme=readme):
                examples = re.findall(r"```json\n(.*?)\n```", self.section(readme), re.DOTALL)
                self.assertEqual(1, len(examples))
                expected = json.loads(examples[0])
                # Sorted JSON ignores property ordering but preserves number/bool/string types.
                self.assertEqual(json.dumps(expected, sort_keys=True), json.dumps(actual, sort_keys=True))

    def test_root_redirects_to_pipeline_summary(self):
        status, headers, _ = self.request("GET", "/")
        self.assertEqual(302, status)
        self.assertEqual("/demo/pipeline", headers["location"])

    def test_replay_summary_is_returned_in_body_without_replay_headers(self):
        status, headers, body = self.request("GET", "/demo/pipeline")
        self.assertEqual(200, status)
        self.assertIsInstance(json.loads(body)["replay"], str)
        self.assertFalse([name for name in headers if "replay" in name.lower()])


if __name__ == "__main__":
    unittest.main(testRunner=unittest.TextTestRunner(stream=sys.stdout, verbosity=2))
