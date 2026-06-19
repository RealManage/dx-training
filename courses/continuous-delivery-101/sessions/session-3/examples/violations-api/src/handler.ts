/**
 * HOA Violations API — Lambda handler (CD 101 teaching example).
 *
 * Design choices that serve Continuous Delivery:
 *   - Pure business logic (escalationLevel) is separated from I/O so it's fast
 *     to unit-test — fast tests keep the CI gate fast, which keeps it trusted.
 *   - Side effects (DynamoDB, SNS) are injected as `deps`, so handler logic is
 *     tested without touching AWS.
 *   - Feature flags gate new behavior so this ships to prod dark and is released
 *     by flipping config, not by deploying a branch.
 */

import type {
  APIGatewayProxyEvent,
  APIGatewayProxyResult,
} from "aws-lambda";

// ---------------------------------------------------------------------------
// Domain logic — pure, no I/O. HOA escalation: each prior violation escalates
// the notice. (Warning -> 30-day -> 60-day -> 90-day.)
// ---------------------------------------------------------------------------

export type EscalationLevel = "WARNING" | "30_DAY" | "60_DAY" | "90_DAY";

export function escalationLevel(priorCount: number): EscalationLevel {
  if (priorCount <= 0) return "WARNING";
  if (priorCount === 1) return "30_DAY";
  if (priorCount === 2) return "60_DAY";
  return "90_DAY";
}

// ---------------------------------------------------------------------------
// Feature flags — default OFF (safe). Read from env; config travels with the
// artifact, so the same bytes behave per-environment by config alone.
// ---------------------------------------------------------------------------

function flagEnabled(name: string, env = process.env): boolean {
  return (env[name] ?? "false").toLowerCase() === "true";
}

// ---------------------------------------------------------------------------
// Injected side effects. Real implementations use the AWS SDK; tests pass fakes.
// ---------------------------------------------------------------------------

export interface ViolationInput {
  propertyId: string;
  description: string;
}

export interface RecordedViolation extends ViolationInput {
  violationId: string;
  level: EscalationLevel;
}

export interface Deps {
  countPriorViolations: (propertyId: string) => Promise<number>;
  save: (v: RecordedViolation) => Promise<void>;
  publishEvent: (v: RecordedViolation) => Promise<void>;
  newId: () => string;
  env?: NodeJS.ProcessEnv;
}

// ---------------------------------------------------------------------------
// Use case — orchestrates domain logic + side effects, gated by flags.
// ---------------------------------------------------------------------------

export async function recordViolation(
  input: ViolationInput,
  deps: Deps,
): Promise<APIGatewayProxyResult> {
  const env = deps.env ?? process.env;

  // Dark until released via config: the endpoint deploys but does nothing yet.
  if (!flagEnabled("FLAG_VIOLATIONS_RECORD", env)) {
    return json(501, { message: "Not implemented" });
  }

  if (!input.propertyId || !input.description) {
    return json(400, { message: "propertyId and description are required" });
  }

  const priorCount = await deps.countPriorViolations(input.propertyId);
  const violation: RecordedViolation = {
    ...input,
    violationId: deps.newId(),
    level: escalationLevel(priorCount),
  };

  await deps.save(violation);

  // The notification path is independently flagged — it can be released later.
  if (flagEnabled("FLAG_VIOLATIONS_NOTIFY", env)) {
    await deps.publishEvent(violation);
  }

  return json(201, violation);
}

function json(status: number, body: unknown): APIGatewayProxyResult {
  return {
    statusCode: status,
    headers: { "content-type": "application/json" },
    body: JSON.stringify(body),
  };
}

// ---------------------------------------------------------------------------
// Lambda entry point — wires real AWS deps. (Kept thin; logic is tested above.)
// ---------------------------------------------------------------------------

export async function handler(
  event: APIGatewayProxyEvent,
): Promise<APIGatewayProxyResult> {
  if (event.httpMethod === "POST") {
    const input = JSON.parse(event.body ?? "{}") as ViolationInput;
    return recordViolation(input, await awsDeps());
  }
  // GET history endpoint omitted for brevity — see the decompose exercise.
  return json(405, { message: "Method not allowed" });
}

// Lazily construct AWS clients so unit tests never import the SDK path.
async function awsDeps(): Promise<Deps> {
  const { DynamoDBClient } = await import("@aws-sdk/client-dynamodb");
  const { DynamoDBDocumentClient, PutCommand, QueryCommand } = await import(
    "@aws-sdk/lib-dynamodb"
  );
  const { SNSClient, PublishCommand } = await import("@aws-sdk/client-sns");
  const { randomUUID } = await import("node:crypto");

  const ddb = DynamoDBDocumentClient.from(new DynamoDBClient({}));
  const sns = new SNSClient({});
  const table = process.env.VIOLATIONS_TABLE!;
  const topicArn = process.env.VIOLATION_EVENTS_TOPIC!;

  return {
    newId: () => randomUUID(),
    countPriorViolations: async (propertyId) => {
      const res = await ddb.send(
        new QueryCommand({
          TableName: table,
          KeyConditionExpression: "propertyId = :p",
          ExpressionAttributeValues: { ":p": propertyId },
          Select: "COUNT",
        }),
      );
      return res.Count ?? 0;
    },
    save: async (v) => {
      await ddb.send(new PutCommand({ TableName: table, Item: v }));
    },
    publishEvent: async (v) => {
      await sns.send(
        new PublishCommand({
          TopicArn: topicArn,
          Message: JSON.stringify(v),
          MessageAttributes: {
            eventType: { DataType: "String", StringValue: "ViolationRecorded" },
          },
        }),
      );
    },
  };
}
