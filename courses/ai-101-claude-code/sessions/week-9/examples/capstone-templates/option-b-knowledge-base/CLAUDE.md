# Capstone Option B: Self-Service Knowledge Base

## Project Purpose
Build an AI-powered knowledge base for HOA residents to search CCRs using
natural language queries and get relevant answers without calling support.

## Tech Stack
- C# .NET 10 for backend API
- xUnit, FluentAssertions, Moq
- (Optional) Angular 17 for frontend

## Requirements

### Core Features (Must Have)
1. CCR document parsing and indexing
2. Natural language search with relevance scoring
3. FAQ generation from search patterns
4. Search analytics and cost tracking

### Domain Context
- CCRs are organized by sections (e.g., "Section 4.2.1 - Landscaping")
- Common searches: pet rules, parking, architectural changes, dues
- Residents need quick, accurate answers
- Board needs to see what residents are searching for

### Custom Skill Required
Create `/search-ccr <query>` skill that:
1. Parses the natural language query
2. Finds relevant CCR sections
3. Returns ranked results with excerpts
4. Logs search for analytics

### Cost Tracking Required
Track AI token usage for each search operation

## Getting Started

```bash
# Build the starter
dotnet build

# Run tests (should see failures - TDD!)
dotnet test

# Start Claude Code
claude
```

## Suggested Implementation Order

1. Define CCR document model and search result model
2. Write tests for document parsing
3. Implement document parser
4. Write tests for keyword extraction
5. Implement keyword extraction
6. Write tests for relevance scoring
7. Implement search with scoring
8. Add FAQ generation logic
9. Create search skill
10. Integration tests

## Success Criteria

```
[ ] CCR documents parsed correctly
[ ] Natural language queries return relevant sections
[ ] Results ranked by relevance
[ ] FAQ generated from popular searches
[ ] Cost per query tracked
[ ] Test coverage >= 90%
```

## Sample CCR Data

Use this sample data for testing:

```
Section 4.2.1 - Landscaping Standards
All front lawns must be maintained in a neat and orderly condition.
Grass height shall not exceed 6 inches.
Landscaping changes require ARC approval.

Section 5.4.0 - Pet Regulations
Pets must be leashed in common areas.
Maximum of 2 pets per household.
Aggressive breeds require board approval.

Section 6.1.1 - Parking Rules
Each unit is assigned 2 parking spaces.
Street parking limited to 72 hours.
Commercial vehicles prohibited.
```
