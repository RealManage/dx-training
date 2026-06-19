#!/usr/bin/env bash
#
# Smoke test — prove a freshly deployed Violations API actually serves traffic
# before the pipeline promotes the SAME artifact onward. The deploy stage calls
# this after every `sam deploy`; a non-zero exit fails the stage and stops
# promotion. This is what makes "verified, not hoped" true (CD 101, Session 3).
#
# It is deliberately tiny: ONE real request against the deployed endpoint.
#
# Two design choices worth teaching:
#   1. It reads the invoke URL from the stack's OWN CloudFormation output, so the
#      test follows the artifact to whatever environment just deployed — no
#      hard-coded endpoints.
#   2. It probes with an EMPTY body so the request mutates nothing. A smoke test
#      runs against prod on every deploy; it must never write junk rows. With an
#      empty body our handler returns:
#        - 501 if the feature is deployed DARK (flag off) — still a healthy deploy
#        - 400 if the feature is released (flag on): validation rejects the empty
#          body BEFORE any DynamoDB write
#      Either proves the full path (API Gateway -> Lambda -> our code) is live.
#      Anything else — 000 (unreachable), 403 (auth misconfig), 5xx (crash) —
#      means the deploy is broken and must not be promoted.
#
set -euo pipefail

ENV="${1:?usage: smoke-test.sh <environment>}"
SERVICE_NAME="${SERVICE_NAME:-aws-violations-api}"
STACK_NAME="${ENV}-${SERVICE_NAME}"

API_URL="$(aws cloudformation describe-stacks \
  --stack-name "${STACK_NAME}" \
  --query "Stacks[0].Outputs[?OutputKey=='ApiUrl'].OutputValue" \
  --output text)"

if [[ -z "${API_URL}" || "${API_URL}" == "None" ]]; then
  echo "SMOKE FAIL: stack ${STACK_NAME} has no ApiUrl output" >&2
  exit 1
fi

echo "Smoke-testing ${ENV} at ${API_URL}"

status="$(curl -s -o /dev/null -w '%{http_code}' \
  --max-time 10 \
  -X POST "${API_URL}" \
  -H 'content-type: application/json' \
  -d '{}')"

case "${status}" in
  400 | 501)
    echo "SMOKE OK (${status}): ${ENV} is serving traffic"
    ;;
  *)
    echo "SMOKE FAIL (${status}): ${ENV} did not respond healthily" >&2
    exit 1
    ;;
esac
