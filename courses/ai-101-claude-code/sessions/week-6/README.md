# Week 6: MCP Servers & External Integrations 🔌

**Duration:** 2 hours  
**Format:** Self-paced or instructor-led  
**Prerequisites:** Completed Weeks 1-5

## 🎯 Learning Objectives

By the end of this session, you'll be able to:
- ✅ Understand Model Context Protocol (MCP)
- ✅ Build custom MCP servers for RealManage
- ✅ Integrate with Azure services
- ✅ Connect to SQL Server via Entity Framework
- ✅ Create HOA-specific tools and automations

## 📚 Session Content

Coming soon! This session will cover:

### Part 1: MCP Fundamentals
- Understanding the protocol
- Benefits for HOA management
- Security considerations
- Performance implications

### Part 2: Building MCP Servers
- MCP SDK setup
- Creating custom tools
- Error handling and logging
- Testing MCP servers

### Part 3: RealManage Integrations
- SQL Server data access
- Azure Service Bus messaging
- Azure AD B2C authentication
- Blob storage for documents

### Part 4: HOA-Specific Tools
- `get_hoa_fees(account_id)`
- `track_violation(property_id)`
- `generate_board_report()`
- `process_payment_batch()`

## 🧪 Sandbox Exercises

Create your sandbox from the example (when available):
```bash
# When example is provided:
cp -r example sandbox
cd sandbox
claude
```

Exercises will include:
- Build HOA fees MCP server
- Connect to mock database
- Create violation tracking tool

## 📖 Resources

- [MCP Integration Guide](https://docs.anthropic.com/en/docs/claude-code/mcp)
- [Model Context Protocol](https://docs.anthropic.com/en/docs/mcp)
- [MCP SDK Documentation](https://docs.anthropic.com/en/docs/claude-code/sdk)
- [MCP Quickstart](https://modelcontextprotocol.io/quickstart/server)

## 🚀 Next Week

[Week 7: Real-World Scenarios →](../week-7/README.md)