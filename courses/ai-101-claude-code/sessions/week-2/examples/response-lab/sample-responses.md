# Sample Responses: Before & After

See how specific prompts produce better results.

---

## Scenario 1: Late Fee Question

**Customer Email:**
> My bill says $542 but my dues are only $450. Where did the extra charges come from?

### Vague Prompt Response

**Prompt:** "Respond to a customer asking about extra charges"

**Result:**
> Thank you for reaching out. The extra charges on your account may be due to various fees or adjustments. Please review your statement carefully or contact our office for more details. We're happy to help clarify any questions you may have.

**Problems:** Generic, doesn't explain anything, shifts burden to customer

---

### Specific Prompt Response

**Prompt:**

```
Write a response to a resident confused about their balance.

Details:
- Quarterly dues: $450
- Current balance: $542.85
- They're about 2 months late
- Late fee policy: 10% monthly compound after 30-day grace

Response should:
- Show the math for how fees accumulated
- Be empathetic (don't lecture)
- Offer payment options
- Keep under 150 words
```

**Result:**
> Hi [Name],
>
> I understand the confusion - let me break down your balance:
>
> - Original dues: $450.00
> - Month 1 late fee (10%): $45.00 → Balance: $495.00
> - Month 2 late fee (10% of $495): $49.50 → Balance: $544.50
>
> (Small rounding differences may account for the $542.85 figure.)
>
> You can pay online at [portal], by phone at [number], or by mail. If the full amount is difficult right now, we can discuss a payment plan.
>
> Let me know if you have questions!

**Why it's better:** Shows the math, empathetic tone, offers solutions

---

## Scenario 2: Violation Dispute

**Customer Email:**
> I got a violation for "visible trash cans" but I only put them out the night before pickup! This is ridiculous.

### Vague Prompt Response

**Prompt:** "Respond to someone upset about a violation"

**Result:**
> We apologize for any inconvenience. Violations are issued based on community guidelines. If you believe this was issued in error, you may submit an appeal. Thank you for your understanding.

**Problems:** Dismissive, no explanation, no empathy

---

### Specific Prompt Response

**Prompt:**

```
Draft a response to a resident upset about a trash can violation.

Context:
- They put cans out night before pickup (common practice)
- This is their first violation
- They've lived there 8 years with no issues
- CC&Rs say cans must be out of sight except collection day

Response should:
- Acknowledge their frustration genuinely
- Explain the specific rule
- Note this is a warning (no fine for first offense)
- Offer a practical solution
- Be understanding of their long good standing
```

**Result:**
> Hi Martha,
>
> I completely understand your frustration, especially after 8 years without any issues - that's a great track record!
>
> The CC&Rs (Section 4.2) require trash containers to be stored out of street view except on collection day itself. Many communities interpret "collection day" as the morning of pickup rather than the night before.
>
> The good news: this is just a written warning with no fine. Going forward, if you could wait until the morning of pickup (or store cans where they're not visible from the street), you'll be all set.
>
> Thank you for being such a great community member all these years. Please reach out if you have questions!

**Why it's better:** Validates feelings, explains rule, acknowledges history, offers solution

---

## Key Differences Summary

| Aspect | Vague Prompt | Specific Prompt |
|--------|--------------|-----------------|
| Context | None | Full situation details |
| Tone guidance | None | Empathetic, understanding |
| Structure | None | Clear requirements |
| Length | Undefined | Constrained |
| Result | Generic, unhelpful | Personalized, actionable |
