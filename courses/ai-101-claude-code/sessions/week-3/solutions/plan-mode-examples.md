# Plan Mode Examples - Answer Key

## Overview

This document provides example plan mode outputs that demonstrate effective tactical planning. Use these to evaluate student plans and show what "good" looks like.

---

## Example 1: Multi-File Refactoring

### The Task
"Rename `CalculateFee` to `CalculateLateFee` across the codebase and update all tests."

### Bad Plan (Too Vague)
```
Claude's Plan:
1. Find all files with CalculateFee
2. Rename them
3. Run tests
```

**Why it's bad:** No specificity, no order, no consideration of dependencies.

### Good Plan (Tactical & Specific)
```
Claude's Plan:

I'll rename CalculateFee to CalculateLateFee systematically. Here's my approach:

**Discovery Phase:**
1. Search for all usages of `CalculateFee` in .cs files
2. Categorize findings:
   - Method definitions (where the method is declared)
   - Method calls (where it's invoked)
   - Test files (test method names and assertions)
   - Comments and XML docs (if any)

**Rename Phase (order matters to avoid build breaks):**
3. Rename the method definition first in LateFeeCalculator.cs
4. Update all callers in this order:
   - PaymentService.cs
   - PaymentController.cs
   - Any other service classes
5. Update test files last:
   - LateFeeCalculatorTests.cs
   - Integration tests

**Verification Phase:**
6. Build the solution to catch any missed references
7. Run all tests to verify behavior unchanged
8. Check for any string literals containing "CalculateFee" (logging, error messages)

Shall I proceed with this plan?
```

**Why it's good:**
- Clear phases (Discovery, Rename, Verify)
- Specific file order to prevent intermediate build failures
- Considers edge cases (strings, comments)
- Includes verification step

---

## Example 2: Code Review Fix Organization

### The Task
"I received 8 code review comments. Help me organize and fix them."

### Bad Plan (Just Lists Items)
```
Claude's Plan:
1. Fix comment 1
2. Fix comment 2
3. Fix comment 3
... (continues listing)
```

**Why it's bad:** No grouping, no priority, no efficiency gains.

### Good Plan (Grouped & Prioritized)
```
Claude's Plan:

I've analyzed your 8 code review comments. Here's how I'd group and order them:

**Group A: Security Fixes (Do First - Highest Risk)**
- Comment #3: Add null check before database query
- Comment #7: Sanitize user input in search endpoint

These are security-sensitive and should be fixed first.

**Group B: Related Refactoring (Do Together - Same Files)**
- Comment #1: Extract magic number to constant
- Comment #4: Extract duplicate code to shared method
- Comment #6: Rename variable for clarity

All three are in PaymentService.cs - I'll do them in one pass.

**Group C: Test Updates (Do After Code Changes)**
- Comment #2: Add missing test for edge case
- Comment #8: Fix flaky test assertion

These depend on the code changes above.

**Group D: Documentation (Do Last)**
- Comment #5: Add XML comments to public methods

Non-blocking, can be done after everything else passes.

**Estimated Time:** 30-40 minutes total

Shall I proceed in this order, or would you like to adjust groupings?
```

**Why it's good:**
- Logical groupings by type and risk
- Same-file changes grouped for efficiency
- Dependencies respected (tests after code)
- Time estimate helps planning

---

## Example 3: Phased Feature Implementation

### The Task
"Implement a payment plan feature that allows residents to pay in installments."

### Bad Plan (Too Much Upfront)
```
Claude's Plan:
1. Create PaymentPlan entity with 15 properties
2. Create PaymentPlanService with 12 methods
3. Create PaymentPlanController with 8 endpoints
4. Create 47 unit tests
5. Create 12 integration tests
6. Update database schema
7. Add Angular components
8. Update documentation
...
```

**Why it's bad:** Trying to plan everything upfront. Will become outdated after step 2.

### Good Plan (Just-In-Time Phasing)
```
Claude's Plan:

This is a multi-phase feature. I'll plan **Phase 1 only** - we'll plan subsequent phases after this one works.

**Phase 1: Core Data Model (Today's Focus)**

Goal: Create the minimal PaymentPlan entity and basic CRUD.

1. **Entity Creation:**
   - Create `PaymentPlan` class with essential properties only:
     - Id, ResidentId, TotalAmount, NumberOfInstallments, CreatedDate
   - Skip optional properties until needed

2. **Database Migration:**
   - Add migration for PaymentPlans table
   - Run migration locally to verify

3. **Basic Repository:**
   - Create IPaymentPlanRepository interface
   - Implement basic Create/GetById/GetByResident

4. **Unit Tests:**
   - Test entity creation
   - Test repository CRUD operations
   - Target: 80-90% coverage on new code

**Definition of Done for Phase 1:**
- Entity exists with tests
- Migration runs successfully
- Can create and retrieve payment plans

**What's NOT in Phase 1:**
- Installment calculations (Phase 2)
- API endpoints (Phase 3)
- Frontend (Phase 4)

Ready to start Phase 1?
```

**Why it's good:**
- Only plans immediate phase
- Clear definition of done
- Explicit about what's deferred
- Follows just-in-time planning principle

---

## Example 4: Bug Investigation

### The Task
"Users report interest calculations are wrong after 90 days."

### Bad Plan (Jumps to Fixing)
```
Claude's Plan:
1. Open LateFeeCalculator.cs
2. Change the interest formula
3. Done!
```

**Why it's bad:** Assumes we know the bug before investigating. Might fix the wrong thing.

### Good Plan (Investigate Then Fix)
```
Claude's Plan:

I'll approach this as a proper investigation before fixing anything.

**Investigation Phase:**

1. **Reproduce the Issue:**
   - Find or create a test case with >90 days late payment
   - Document: What value do we get? What value should we get?
   - Example: $1000 principal, 100 days late
     - Expected (compound at 10%/month): ~$1331
     - Actual: ??? (need to run code to see)

2. **Trace the Code Path:**
   - Identify which method handles 90+ day calculations
   - Check: Is it CalculateLateFee? CalculateCompoundInterest?
   - Look for branching logic around day thresholds

3. **Identify Root Cause:**
   - Document exactly what's wrong (formula? constant? logic?)
   - Write a failing test that proves the bug

**Fix Phase (after investigation confirms root cause):**

4. **Write Failing Test First (TDD):**
   - Test case: 100 days late, $1000 principal
   - Assert expected compound interest value

5. **Implement Fix:**
   - Only change what's necessary
   - Keep the fix minimal

6. **Verify:**
   - Run the new test (should pass)
   - Run all existing tests (should still pass)
   - Manual spot-check with calculator

Shall I start the investigation?
```

**Why it's good:**
- Doesn't assume the answer
- Follows scientific method (hypothesize, test, verify)
- Uses TDD (failing test before fix)
- Minimal, targeted fix

---

## Example 5: Adding a New API Endpoint

### The Task
"Add a GET endpoint to retrieve payment history for a resident."

### Bad Plan (Missing Context)
```
Claude's Plan:
1. Add GetPaymentHistory method to controller
2. Return payments
```

**Why it's bad:** No security, no validation, no error handling, no tests.

### Good Plan (Production-Ready Thinking)
```
Claude's Plan:

I'll add a secure, well-tested endpoint for payment history.

**1. Define the Contract:**
   - Endpoint: GET /api/payments/history/{residentId}
   - Response: Array of PaymentHistoryDto (not raw entities!)
   - Include: Amount, Date, Status, PaymentMethod
   - Exclude: Internal IDs, timestamps, audit fields

**2. Security Considerations:**
   - Add [Authorize] attribute
   - Verify caller can only access their own data (or is admin)
   - Rate limiting consideration for large histories

**3. Implementation Order:**

   a. Create PaymentHistoryDto:
      - Only public-safe fields
      - Located in DTOs folder

   b. Add service method:
      - IPaymentService.GetPaymentHistory(residentId)
      - Include pagination (skip, take) for large histories

   c. Add controller endpoint:
      - GET with [Authorize]
      - Validate residentId matches authenticated user
      - Return 403 if unauthorized
      - Return 404 if resident not found
      - Return empty array (not 404) if no payments

**4. Tests:**
   - Unit test: Service returns correct data
   - Unit test: Authorization check works
   - Unit test: Pagination works
   - Integration test: Full endpoint with auth

**5. Documentation:**
   - Add XML comments for Swagger
   - Update API docs if needed

Shall I proceed with this plan?
```

**Why it's good:**
- Thinks about security upfront
- Uses DTOs (not raw entities)
- Considers pagination for scale
- Clear behavior for edge cases (empty list vs 404)
- Includes tests as part of the plan

---

## Plan Mode Anti-Patterns to Avoid

### Anti-Pattern 1: The Novel
```
// BAD: 50+ step plan covering every edge case
1. First, let's consider all the ways this could fail...
2. We need to think about backwards compatibility...
3. What if the database is unavailable?
4. What about leap years?
... (continues for pages)
```

**Fix:** Keep plans to 5-10 actionable steps. Defer edge cases to later phases.

### Anti-Pattern 2: The Obvious
```
// BAD: Planning trivial tasks
Plan for adding a constant:
1. Open the file
2. Find where constants are declared
3. Add the constant
4. Save the file
```

**Fix:** Don't use plan mode for simple tasks. Just do it.

### Anti-Pattern 3: The Crystal Ball
```
// BAD: Trying to predict the future
Phase 1: Add feature
Phase 2: Handle the 7 edge cases I predict
Phase 3: Performance optimize the bottleneck I assume will exist
Phase 4: Refactor based on feedback I haven't received yet
```

**Fix:** Only plan what you know. Future phases get planned when you reach them.

### Anti-Pattern 4: The No-Dependencies Plan
```
// BAD: Ignoring order dependencies
1. Write unit tests
2. Update the database schema
3. Implement the service
4. Create the entity
```

**Fix:** Order steps by dependencies (entity -> migration -> service -> tests).

---

## Iteration Examples

Good plan mode involves refining the plan through conversation.

### Example Iteration
```
You: "I need to add caching to the payment service"

Claude (Plan v1):
1. Add IMemoryCache to PaymentService constructor
2. Cache GetPaymentsByResident results
3. Add cache invalidation on ProcessPayment

You: "What about cache expiration?"

Claude (Plan v2):
1. Add IMemoryCache to PaymentService constructor
2. Define cache options:
   - 5 minute sliding expiration
   - 30 minute absolute expiration
3. Cache GetPaymentsByResident results with key "payments_{residentId}"
4. Invalidate cache on ProcessPayment and RefundPayment
5. Add configuration for cache durations (appsettings.json)

You: "We should also handle distributed cache for production"

Claude (Plan v3):
1. Abstract caching behind ICacheService interface
2. Implement MemoryCacheService for development
3. Implement RedisCacheService for production
4. Register appropriate implementation based on environment
5. Cache GetPaymentsByResident with configurable expiration
6. Invalidate on write operations
7. Add health check for Redis connectivity

This iteration is what makes plan mode valuable!
```

---

## When to Exit Plan Mode

Exit plan mode and start executing when:

1. **The plan feels complete** - You've covered the main steps
2. **No more questions** - You've resolved ambiguities through iteration
3. **Clear first step** - You know exactly what to do first
4. **Reasonable scope** - Plan is 5-10 steps, not 50
5. **Dependencies clear** - You know the order

**Signs you should keep iterating:**
- "Wait, what about...?"
- Plan has ambiguous steps ("handle errors appropriately")
- No clear first action
- You're unsure about the approach

---

## Summary

**Good plans have:**
- Clear phases/groupings
- Specific files and methods mentioned
- Order based on dependencies
- Verification/testing steps
- Reasonable scope (not everything upfront)

**Good iteration involves:**
- Asking clarifying questions
- Adding forgotten edge cases
- Reordering based on priorities
- Splitting large steps into smaller ones
- Deferring future phases

**Exit plan mode when:**
- First step is crystal clear
- You've addressed major concerns
- Plan is actionable, not theoretical

---

*Use these examples to evaluate student plans and provide feedback during the session.*
