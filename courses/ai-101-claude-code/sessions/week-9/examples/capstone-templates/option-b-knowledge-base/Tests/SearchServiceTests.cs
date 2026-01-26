using RealManage.KnowledgeBase.Models;

namespace RealManage.KnowledgeBase.Tests;

/// <summary>
/// Starter tests for knowledge base search service.
/// These define the expected behavior - implement ISearchService to make them pass.
/// Remove the Skip attribute and implement each test as you build the service.
/// </summary>
public class SearchServiceTests
{
    [Fact(Skip = "Implement: SearchService.ParseQuery() should extract keywords like 'dog' from natural language")]
    public void ParseQuery_ExtractsKeywords()
    {
        throw new NotImplementedException("Create SearchService and implement ParseQuery");
    }

    [Fact(Skip = "Implement: SearchService.Search() should return CCR sections matching the query")]
    public void Search_ReturnsRelevantSections()
    {
        throw new NotImplementedException("Implement Search with relevance scoring");
    }

    [Fact(Skip = "Implement: Exact keyword matches should score higher than partial matches")]
    public void CalculateRelevance_ScoresHigherForExactMatch()
    {
        throw new NotImplementedException("Implement CalculateRelevance algorithm");
    }

    [Fact(Skip = "Implement: Generate FAQs from frequently searched terms")]
    public void GenerateFaqs_CreatesFromPopularSearches()
    {
        throw new NotImplementedException("Implement GenerateFaqs from search history");
    }

    [Fact(Skip = "Implement: Track token usage for cost analytics")]
    public void TrackCost_RecordsTotalTokens()
    {
        throw new NotImplementedException("Implement RecordSearch and GetAnalytics");
    }

    /// <summary>
    /// Sample CCR data for testing. Use this in your test implementations.
    /// </summary>
    public static List<CcrSection> GetSampleSections() => new()
    {
        new CcrSection
        {
            Id = "sec-4-2-1",
            SectionNumber = "4.2.1",
            Title = "Landscaping Standards",
            Content = "All front lawns must be maintained in a neat and orderly condition. Grass height shall not exceed 6 inches.",
            Keywords = ["lawn", "landscaping", "grass", "yard"]
        },
        new CcrSection
        {
            Id = "sec-5-4-0",
            SectionNumber = "5.4.0",
            Title = "Pet Regulations",
            Content = "Pets must be leashed in common areas. Maximum of 2 pets per household. Aggressive breeds require board approval.",
            Keywords = ["pet", "dog", "cat", "animal", "leash"]
        },
        new CcrSection
        {
            Id = "sec-6-1-1",
            SectionNumber = "6.1.1",
            Title = "Parking Rules",
            Content = "Each unit is assigned 2 parking spaces. Street parking limited to 72 hours. Commercial vehicles prohibited.",
            Keywords = ["parking", "car", "vehicle", "garage"]
        }
    };
}
