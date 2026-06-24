# Example: Characterization Test Before a Legacy Change

A small **C#/xUnit** illustration of how to change untested legacy code under CD. Like the [Database Migrations](../../../session-3/examples/db-migrations/README.md) example, it is a **teaching reference, not a buildable project** — read the files as annotated illustrations.

It is in **C#** on purpose: the untested code that needs this technique lives in RealManage's established .NET estate. (This and `db-migrations` are the course's two deliberate C#/.NET exceptions to the "examples are TypeScript + SAM" rule.)

## The situation

`LateFeeCalculator.CalculateLateFee` has computed HOA late fees in the monolith for years. It has **no automated tests** — it has been "tested in production" by every billing run. Now the business wants to change the rule: shorten the grace period and raise the percentage cap. Changing untested code by hand is exactly the scary edit CD has to make routine.

## The four rules, in one change

| File | What it teaches |
| ---- | --------------- |
| [`LateFeeCalculator.cs`](./LateFeeCalculator.cs) | The legacy method (no tests, "tested in prod" — **rule 2**), with the new rule added **behind a feature flag** so it can merge to trunk dark. |
| [`LateFeeCalculatorCharacterizationTests.cs`](./LateFeeCalculatorCharacterizationTests.cs) | **Characterization tests** that pin the *current* behavior before the change (**rule 3**), plus intent-stating tests for the new flagged path (**rule 1**). |

## How it flows

1. **Characterize first (rule 3).** Before touching the method, write tests that pin what it does *today* — including the quirks (no fee at exactly 30 days; the legacy $100 percentage cap). These assert current behavior, not desired behavior, so an unintended change fails loudly.
2. **Add the new rule as new, tested code (rule 1).** `late-fee-v2` is written with its own tests stating the intended contract (15-day grace, $250 cap). AI makes those tests cheap to draft — but they must pin real intent, [independently confirmed](../../../../resources/ai-assisted-delivery.md), or they verify nothing.
3. **Merge dark — keep doing CI.** The change integrates to trunk with `late-fee-v2` **off**. It is integrated but not released, so there is no long-lived branch. The characterization tests prove the legacy path is untouched.
4. **Verify by hand, then flip (rule 4).** Exploratory-test the new fee in `qa` with the flag on — does it behave on real-looking accounts? When it is right, flip the flag in production. The flip is the release; manual verification gated it, not the merge.

See [Testing and CD](../../../../resources/testing-and-cd.md) for the full reasoning.
