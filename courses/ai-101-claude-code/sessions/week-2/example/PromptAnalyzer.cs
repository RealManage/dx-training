namespace RealManage.PromptLab;

public class PromptAnalyzer
{
    public PromptAnalysis Analyze(string prompt)
    {
        var analysis = new PromptAnalysis();
        
        // TODO: This analyzer has a bug - it doesn't properly weight the importance of different factors
        // Students should discover this and improve the scoring algorithm
        
        // Check for clear objective
        analysis.HasClearObjective = ContainsActionVerb(prompt);
        
        // Check for context
        analysis.HasContext = prompt.Length > 50; // Bug: Too simplistic
        
        // Check for specificity
        analysis.IsSpecific = ContainsSpecificRequirements(prompt);
        
        // Check for structure
        analysis.IsStructured = ContainsStructuralElements(prompt);
        
        // Calculate score (Bug: Equal weighting isn't ideal)
        var score = 0;
        if (analysis.HasClearObjective) score += 25;
        if (analysis.HasContext) score += 25;
        if (analysis.IsSpecific) score += 25;
        if (analysis.IsStructured) score += 25;
        
        analysis.Score = score;
        
        // Generate suggestions
        GenerateSuggestions(analysis, prompt);
        
        return analysis;
    }
    
    private bool ContainsActionVerb(string prompt)
    {
        var actionVerbs = new[] { 
            "create", "write", "build", "implement", "design", 
            "fix", "refactor", "optimize", "analyze", "review",
            "test", "document", "explain", "generate", "calculate"
        };
        
        var lowerPrompt = prompt.ToLower();
        return actionVerbs.Any(verb => lowerPrompt.Contains(verb));
    }
    
    private bool ContainsSpecificRequirements(string prompt)
    {
        // Check for specific technical terms, numbers, or constraints
        var specificIndicators = new[] {
            "c#", ".net", "angular", "typescript", "xunit",
            "95%", "coverage", "async", "repository", "pattern",
            "hoa", "violation", "payment", "tdd", "test"
        };
        
        var lowerPrompt = prompt.ToLower();
        return specificIndicators.Any(indicator => lowerPrompt.Contains(indicator));
    }
    
    private bool ContainsStructuralElements(string prompt)
    {
        // Check for lists, XML tags, or markdown
        return prompt.Contains("-") || 
               prompt.Contains("*") || 
               prompt.Contains("<") || 
               prompt.Contains("#") ||
               prompt.Contains("\n");
    }
    
    private void GenerateSuggestions(PromptAnalysis analysis, string prompt)
    {
        if (!analysis.HasClearObjective)
        {
            analysis.Suggestions.Add("Start with a clear action verb (create, write, fix, etc.)");
        }
        
        if (!analysis.HasContext)
        {
            analysis.Suggestions.Add("Provide more context about the system or domain");
        }
        
        if (!analysis.IsSpecific)
        {
            analysis.Suggestions.Add("Include specific requirements (language, framework, coverage %)");
        }
        
        if (!analysis.IsStructured)
        {
            analysis.Suggestions.Add("Use bullet points or XML tags to structure complex prompts");
        }
        
        if (!prompt.ToLower().Contains("test"))
        {
            analysis.Suggestions.Add("Remember to request tests with 95% coverage");
        }
        
        if (prompt.Length < 30)
        {
            analysis.Suggestions.Add("Your prompt might be too vague - add more detail");
        }
    }
}

public class PromptAnalysis
{
    public int Score { get; set; }
    public bool HasClearObjective { get; set; }
    public bool HasContext { get; set; }
    public bool IsSpecific { get; set; }
    public bool IsStructured { get; set; }
    public List<string> Suggestions { get; set; } = new();
}