---
order: 5
---
# Setup Guide: Connect Claude Code to Datadog

> **Do this before the session.** It takes about 10 minutes. Nobody's install time comes out of the session, so arrive with the verification step passing.

You'll add the Datadog MCP server to Claude Code, sign in to Datadog once, and run one prompt that returns real data from our account.

**Tested with:** Claude Code 2.1.282 on Windows 11, in both PowerShell and Git Bash, on 2026-09-25.

---

## Before you start

- [ ] **Claude Code is installed and signed in.** `claude --version` prints a version. If not, see the AI 101 [setup verification](../ai-101-claude-code/sessions/week-1/setup-verification.md).
- [ ] **You can open Datadog.** Go to <https://us5.datadoghq.com> in your browser and confirm you see our dashboards. Our account is on Datadog's **US5** site; the `us5` in every URL matters.

---

## Step 1: Add the Datadog MCP server

Run this once. It is the same command in both shells.

**Windows (PowerShell):**

```powershell
claude mcp add --transport http --scope user datadog-mcp "https://mcp.us5.datadoghq.com/v1/mcp?toolsets=all"
```

**Windows (Git Bash):**

```bash
claude mcp add --transport http --scope user datadog-mcp "https://mcp.us5.datadoghq.com/v1/mcp?toolsets=all"
```

Three parts of that line matter:

- `--scope user` makes the server available in every folder. Without it, Claude Code adds it only for the folder you're in.
- `us5` is our Datadog site.
- `?toolsets=all` turns on every tool family, including profiling and Error Tracking. Without it you only get the core tools. Keep the quotes around the URL.

Expected output:

```text
Added HTTP MCP server datadog-mcp with URL: https://mcp.us5.datadoghq.com/v1/mcp to user config
File modified: C:\Users\<you>\.claude.json
```

> The confirmation line leaves off `?toolsets=all`. That's only the message. Step 2 shows the full URL was saved.

---

## Step 2: Check what was saved

```bash
claude mcp get datadog-mcp
```

Expected output:

```text
datadog-mcp:
  Scope: User config (available in all your projects)
  Status: ! Needs authentication
  Type: http
  URL: https://mcp.us5.datadoghq.com/v1/mcp?toolsets=all
```

Check two things: **Scope** says *User config*, and the **URL** ends in `?toolsets=all`. "Needs authentication" is expected until Step 3.

---

## Step 3: Sign in to Datadog

1. Start Claude Code: `claude`
2. Type `/mcp` and press Enter.
3. Select **datadog-mcp** and choose to authenticate.
4. Your browser opens a Datadog page asking you to authorize Claude Code. Check that the address starts with `https://us5.datadoghq.com`, then approve.
5. Go back to the terminal. Exit Claude Code (`/exit`) and start it again so it loads the Datadog tools.

Run `claude mcp get datadog-mcp` again. The status should now read:

```text
  Status: ✔ Connected
```

---

## Step 4: One-minute verification

Start Claude Code and paste:

```text
Using the Datadog MCP, list the services that have continuous profiling data in the last hour.
```

**It works if** Claude lists about a dozen .NET services, including `resident-api`, `management-api` and `identity-api`. It may note that the list isn't in any particular order.

**Something is wrong if** Claude says it has no profiling tools, asks you to authenticate, or returns nothing. Go to [Troubleshooting](#troubleshooting).

Before the session, check both:

- [ ] `claude mcp get datadog-mcp` shows **User config**, the URL ends in `?toolsets=all`, and status is **✔ Connected**.
- [ ] The verification prompt lists our profiled services.

You're ready for the session.

---

## Troubleshooting

### "Needs authentication" or Claude asks you to sign in

You haven't finished Step 3, or the sign-in expired. Run `/mcp` inside Claude Code, select **datadog-mcp** and authenticate again. Then restart Claude Code.

### Claude says it has no profiling tools

The URL is missing `?toolsets=all`, so only the core tools loaded, without profiling or Error Tracking. Nothing warns you about this: the status still says Connected. Fix the URL (below), then restart Claude Code.

### Changing the URL: "MCP server datadog-mcp already exists"

You can't add a server twice under the same name. Remove it, add it again, then restart Claude Code:

```bash
claude mcp remove datadog-mcp -s user
claude mcp add --transport http --scope user datadog-mcp "https://mcp.us5.datadoghq.com/v1/mcp?toolsets=all"
```

Then repeat Step 3 if Claude Code asks you to sign in again.

### Wrong Datadog site

If the URL says `mcp.datadoghq.com` (no `us5`) or another site, Claude Code still adds it and reports "Needs authentication", so nothing looks wrong yet. The problem shows at sign-in: the browser opens a site that doesn't have our account, or Claude returns no data. Check the URL with `claude mcp get datadog-mcp` and fix it as above.

### It works in one folder but not others

The server was added without `--scope user`, so it only exists for that folder. `claude mcp get datadog-mcp` shows **Local config (private to you in this project)**. Remove it with `claude mcp remove datadog-mcp -s local` and add it again with `--scope user`.

### Still stuck

Post in **#ai-exchange** with the output of `claude mcp get datadog-mcp` (it contains no secrets), or ask the session presenter before the session.

---

## Removing it later

```bash
claude mcp remove datadog-mcp -s user
```
