# /draft-response

Generate a professional customer support response.

## Arguments

- `$0` - Type of issue (late-fee, violation, general, welcome)
- `$1` - Brief description of the situation

## Instructions

You are drafting a customer support response for RealManage HOA management.

Based on the issue type and description, generate a response that:

1. **Acknowledges** the customer's situation appropriately
2. **Explains** relevant policy or information clearly
3. **Provides** specific next steps or options
4. **Maintains** professional, empathetic tone

### Response Guidelines

**For late-fee issues:**

- Explain the 10% monthly compound calculation
- Mention 30-day grace period
- Offer payment options (online, phone, mail)
- Mention payment plans for balances over $500

**For violation issues:**

- Reference the specific CC&R section
- Explain what compliance looks like
- State the timeline (30/60/90 day escalation)
- Include appeal process

**For general inquiries:**

- Answer the question directly
- Provide additional helpful context
- Include relevant links/resources

**For welcome messages:**

- Be warm and welcoming
- Answer their specific questions
- Direct them to portal and resources
- Offer to help with anything else

### Format

Keep responses:

- Under 200 words for most issues
- Under 300 words for complex situations
- Well-structured with clear paragraphs
- Including greeting and professional closing

## Output

A complete customer response ready for review and sending.

## Example Usage

```text
/draft-response late-fee "Resident confused about $542 balance when dues are $450"
```

```text
/draft-response violation "First-time violation for trash cans, resident upset"
```

```text
/draft-response welcome "New resident asking about dues, portal, and paint approval"
```
