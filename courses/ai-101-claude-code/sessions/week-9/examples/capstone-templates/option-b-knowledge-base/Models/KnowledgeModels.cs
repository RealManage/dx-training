namespace RealManage.KnowledgeBase.Models;

/// <summary>
/// Represents a CCR document section.
/// </summary>
public record CcrSection
{
    public required string Id { get; init; }
    public required string SectionNumber { get; init; }
    public required string Title { get; init; }
    public required string Content { get; init; }
    public required List<string> Keywords { get; init; }
}

/// <summary>
/// Represents a search result with relevance scoring.
/// </summary>
public record SearchResult
{
    public required CcrSection Section { get; init; }
    public required double RelevanceScore { get; init; }
    public required string MatchedExcerpt { get; init; }
    public required List<string> MatchedKeywords { get; init; }
}

/// <summary>
/// Represents a search query with parsed components.
/// </summary>
public record SearchQuery
{
    public required string OriginalQuery { get; init; }
    public required List<string> ExtractedKeywords { get; init; }
    public required string? Intent { get; init; } // "question", "lookup", "rule"
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}

/// <summary>
/// Frequently asked question generated from search patterns.
/// </summary>
public record Faq
{
    public required string Question { get; init; }
    public required string Answer { get; init; }
    public required string SourceSection { get; init; }
    public required int TimesAsked { get; init; }
}

/// <summary>
/// Search analytics for tracking patterns.
/// </summary>
public record SearchAnalytics
{
    public required int TotalSearches { get; init; }
    public required Dictionary<string, int> PopularKeywords { get; init; }
    public required Dictionary<string, int> PopularSections { get; init; }
    public required decimal TotalTokensUsed { get; init; }
    public required decimal TotalCost { get; init; }
}
