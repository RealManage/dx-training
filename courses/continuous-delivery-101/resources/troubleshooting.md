# CD Troubleshooting — Objections, Blockers, and Answers

Continuous Delivery adoption rarely fails on technology. It stalls on habits, fears, and a few recurring objections. This guide gives you straight answers to the ones you will hear most.

## Common objections

### "Our releases are too risky to do more often."

**The reality:** risk comes from *batch size*, not frequency. A weekly release bundles dozens of changes; when it breaks, you cannot tell which change did it. Ten small deploys a week each change one thing — when one breaks, the cause is obvious and the rollback is trivial. Frequency is how you *reduce* risk, not raise it.

### "We don't have time to set up all this CI/CD machinery."

**The reality:** most CD value comes from practices you can adopt *today* with the tools you already have — smaller branches, daily integration, stop-the-line. You are already paying the cost of big-batch delivery in merge pain, release-day firefighting, and rollback panic. CD moves that cost up front, where it's cheaper.

### "We have GitLab CI, so we already do CI/CD."

**The reality:** owning a pipeline tool is not doing CI. CI is the *discipline* of integrating to trunk daily with fast automated feedback. If your branches live for a week, you have a build server, not Continuous Integration. See [minimums-reference](./minimums-reference.md).

### "Trunk-based development will break `main` constantly."

**The reality:** that fear is what tests and feature flags are for. Small changes + tests-before-merge + flags for incomplete work keep `main` green. Long-lived branches don't prevent breakage — they *defer and concentrate* it into one painful merge.

### "We can't merge a feature that isn't finished."

**The reality:** yes you can — behind a feature flag, turned off. The code path ships dark, integrates daily, and gets revealed when ready. This is exactly how you decouple deploy from release. See [Session 2](../sessions/session-2/README.md).

### "Compliance / change management requires a manual approval."

**The reality:** legitimate — and CD accommodates it. Keep the distinction sharp: a human *authorizing* a release (accepting risk, honoring a change window, a regulated sign-off) is a fine **permanent** control; a human re-deciding *readiness* the pipeline already proved is **debt**. Remove the second, keep the first — deliberately. And notice CD often gives you *stronger* controls than the manual gate did: mandatory MR review is segregation of duties, and the pipeline is a complete, tamper-resistant audit trail. See [governance-and-compliance](./governance-and-compliance.md).

### "Continuous Delivery means losing control — code just ships."

**The reality:** that's Continuous *Deployment*, and it's optional. Continuous *Delivery* keeps every change deployable and lets a human decide *when* to release. You gain control: you can release any time you choose, instead of only on the weekly window.

### "If we ship continuously, what happens to our weekly release email?"

**The reality:** it doesn't go away — it changes what it's built from. Today that email is assembled from stories tagged with a weekly *deploy*'s release number. Under CD there's no weekly bundle, and deploy ≠ release, so you build the notes from **user-facing releases** — the feature-flag flips users actually feel — instead of the deploy log. Cadence stays your choice: keep the weekly digest if clients like it. The full playbook, including the release manager's evolved role, is in [communicating-releases](./communicating-releases.md).

### "Isn't CD a lot of ongoing overhead — daily reviews, flag upkeep, stop-the-line?"

**The reality:** yes, and we don't pretend otherwise. Daily integration is an interruption tax, the review-turnaround target is a real commitment, and feature flags are inventory you must actively retire. But you're already paying *more* — later and unpredictably — in merge hell, release-day firefighting, and rollback panic. CD moves the cost up front where it's smaller and predictable, and the one cost that compounds (flag debt) gets a mechanism, not a plea for discipline. The honest accounting is in [what CD costs you](./what-cd-costs.md).

---

## Technical blockers and fixes

### Problem: branches keep living for days

**Symptoms:** big MRs, frequent conflicts, "I'll merge once it's all done."

**Fixes:**

- Decompose the work before starting (see [decompose-a-branch](../sessions/session-2/exercises/decompose-a-branch.md))
- Merge a thin vertical slice behind a flag on day one
- Set a hard team rule: a branch open more than a day gets split or merged

### Problem: tests are too slow, so people skip them

**Symptoms:** developers push without running tests; pipeline feedback takes 20+ minutes.

**Fixes:**

- Keep the unit suite fast (seconds) and run it pre-merge; push slow integration tests to a later stage
- Parallelize jobs and cache dependencies (`node_modules`, SAM build cache)
- Treat a slow pipeline as a bug worth fixing — fast feedback is a feature

### Problem: different artifacts are built for each environment

**Symptoms:** `sam build` runs separately in dev, qa, and prod; "works in qa, breaks in prod."

**Fixes:**

- Build and package once; store the artifact tagged by `CI_COMMIT_SHA`
- Promote the *same* packaged artifact to each environment; vary only config
- Deploy config with the artifact (`configuration/{env}.config`), never patch a live environment by hand

### Problem: no safe way to recover from a bad deploy

**Symptoms:** when something breaks, recovery is a panicked hotfix; there's no default playbook, and nobody has tested rolling back.

**Fixes:**

- **Default to failing forward.** Because the pipeline makes shipping small and safe, the normal response is to ship a small fix through it — the defect gets *fixed*, not deferred.
- **Keep rollback as a rehearsed emergency lever** for problems that are costly *and* time-sensitive:
  - For Lambda: use versions + an alias (`live`); roll back by shifting the alias to the previous version
  - For CloudFormation/SAM: redeploy the previous immutable artifact; rely on stack rollback on failure
- **Mind the data trap:** rolling code back doesn't roll data back — keep changes backward-compatible (expand/contract) so both directions stay safe
- Rehearse both loops in qa before you need them in prod (see [recovery on AWS](../sessions/session-3/examples/rollback-on-aws.md))

### Problem: a stateful change (e.g., a DynamoDB schema shift) blocks small deploys

**Symptoms:** "we can't deploy incrementally because of the database change."

**Fixes:**

- Use expand/contract (parallel-change): add the new shape, write to both, migrate, then remove the old — each step is a small, backward-compatible deploy
- Keep changes backward-compatible so new work doesn't break delivered work (a CI minimum)

### Problem: deploying to a shared env still happens from someone's laptop

**Symptoms:** "just run `sam deploy` to push the fix to qa."

**Fixes:**

- Make the pipeline the *only* path to shared environments; laptops deploy only to personal sandbox stacks
- Use OIDC so the pipeline — not a person — holds deploy permissions

---

## When to ask for help

Escalate to your tech lead or `#dx-training` if:

- You cannot get the pilot service onto a single pipeline path after Phase 2 effort
- Compliance requirements seem to forbid automation entirely (they usually don't — they constrain it)
- DORA metrics get *worse* after changes (often a sign batch size didn't actually shrink)
- The team agrees with CD in principle but reverts to long-lived branches under deadline pressure
