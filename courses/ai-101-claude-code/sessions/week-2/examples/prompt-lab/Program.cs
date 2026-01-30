using System;
using System.Collections.Generic;
using RealManage.PromptLab;

Console.WriteLine("🎯 RealManage Prompt Lab - Week 2");
Console.WriteLine("===================================\n");

var analyzer = new PromptAnalyzer();
var templateManager = new TemplateManager();
var examples = new HoaPromptExamples();

while (true)
{
    Console.WriteLine("\nSelect an option:");
    Console.WriteLine("1. Analyze a prompt");
    Console.WriteLine("2. Compare two prompts");
    Console.WriteLine("3. Use a template");
    Console.WriteLine("4. View HOA prompt examples");
    Console.WriteLine("5. Test prompt refinement");
    Console.WriteLine("6. Exit");
    Console.Write("\nYour choice: ");
    
    var choice = Console.ReadLine();
    
    switch (choice)
    {
        case "1":
            AnalyzePrompt(analyzer);
            break;
        case "2":
            ComparePrompts(analyzer);
            break;
        case "3":
            UseTemplate(templateManager);
            break;
        case "4":
            ViewExamples(examples);
            break;
        case "5":
            TestRefinement(analyzer);
            break;
        case "6":
            Console.WriteLine("\n👋 Goodbye! Keep prompting effectively!");
            return;
        default:
            Console.WriteLine("❌ Invalid choice. Please try again.");
            break;
    }
}

void AnalyzePrompt(PromptAnalyzer analyzer)
{
    Console.WriteLine("\n📝 Enter your prompt (press Enter twice to finish):");
    var prompt = ReadMultilineInput();
    
    var analysis = analyzer.Analyze(prompt);
    
    Console.WriteLine("\n📊 Prompt Analysis:");
    Console.WriteLine($"Score: {analysis.Score}/100");
    Console.WriteLine($"Clarity: {(analysis.HasClearObjective ? "✅" : "❌")} Clear objective");
    Console.WriteLine($"Context: {(analysis.HasContext ? "✅" : "❌")} Includes context");
    Console.WriteLine($"Specificity: {(analysis.IsSpecific ? "✅" : "❌")} Specific requirements");
    Console.WriteLine($"Structure: {(analysis.IsStructured ? "✅" : "❌")} Well-structured");
    
    if (analysis.Suggestions.Count > 0)
    {
        Console.WriteLine("\n💡 Suggestions:");
        foreach (var suggestion in analysis.Suggestions)
        {
            Console.WriteLine($"  - {suggestion}");
        }
    }
}

void ComparePrompts(PromptAnalyzer analyzer)
{
    Console.WriteLine("\n📝 Enter first prompt (press Enter twice to finish):");
    var prompt1 = ReadMultilineInput();
    
    Console.WriteLine("\n📝 Enter second prompt (press Enter twice to finish):");
    var prompt2 = ReadMultilineInput();
    
    var analysis1 = analyzer.Analyze(prompt1);
    var analysis2 = analyzer.Analyze(prompt2);
    
    Console.WriteLine("\n⚖️ Prompt Comparison:");
    Console.WriteLine($"Prompt 1 Score: {analysis1.Score}/100");
    Console.WriteLine($"Prompt 2 Score: {analysis2.Score}/100");
    
    if (analysis1.Score > analysis2.Score)
    {
        Console.WriteLine("✨ Prompt 1 is better structured!");
    }
    else if (analysis2.Score > analysis1.Score)
    {
        Console.WriteLine("✨ Prompt 2 is better structured!");
    }
    else
    {
        Console.WriteLine("🤝 Both prompts are equally structured!");
    }
}

void UseTemplate(TemplateManager manager)
{
    var templates = manager.GetAvailableTemplates();
    
    Console.WriteLine("\n📋 Available Templates:");
    for (int i = 0; i < templates.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {templates[i]}");
    }
    
    Console.Write("\nSelect template number: ");
    if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= templates.Count)
    {
        var templateName = templates[choice - 1];
        var template = manager.GetTemplate(templateName);
        
        if (template.Variables.Count > 0)
        {
            var values = new Dictionary<string, string>();
            Console.WriteLine("\n📝 Fill in template variables:");
            foreach (var variable in template.Variables)
            {
                Console.Write($"{variable}: ");
                values[variable] = Console.ReadLine() ?? "";
            }
            
            var filled = manager.FillTemplate(templateName, values);
            Console.WriteLine("\n✅ Generated Prompt:");
            Console.WriteLine(filled);
        }
        else
        {
            Console.WriteLine("\n✅ Template:");
            Console.WriteLine(template.Content);
        }
    }
    else
    {
        Console.WriteLine("❌ Invalid selection.");
    }
}

void ViewExamples(HoaPromptExamples examples)
{
    var categories = examples.GetCategories();
    
    Console.WriteLine("\n📚 HOA Prompt Categories:");
    for (int i = 0; i < categories.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {categories[i]}");
    }
    
    Console.Write("\nSelect category: ");
    if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= categories.Count)
    {
        var category = categories[choice - 1];
        var prompts = examples.GetExamples(category);
        
        Console.WriteLine($"\n📝 {category} Examples:");
        foreach (var example in prompts)
        {
            Console.WriteLine($"\n{example.Title}:");
            Console.WriteLine($"Bad ❌: {example.Bad}");
            Console.WriteLine($"Good ✅: {example.Good}");
            Console.WriteLine($"Why: {example.Explanation}");
        }
    }
}

void TestRefinement(PromptAnalyzer analyzer)
{
    Console.WriteLine("\n🔄 Prompt Refinement Exercise");
    Console.WriteLine("Start with a basic prompt and refine it iteratively.\n");
    
    var iterations = new List<(string prompt, int score)>();
    
    while (true)
    {
        Console.WriteLine($"Iteration {iterations.Count + 1} - Enter prompt (or 'done' to finish):");
        var prompt = ReadMultilineInput();
        
        if (prompt.ToLower() == "done")
            break;
            
        var analysis = analyzer.Analyze(prompt);
        iterations.Add((prompt, analysis.Score));
        
        Console.WriteLine($"\nScore: {analysis.Score}/100");
        
        if (analysis.Suggestions.Count > 0)
        {
            Console.WriteLine("Suggestions for improvement:");
            foreach (var suggestion in analysis.Suggestions)
            {
                Console.WriteLine($"  - {suggestion}");
            }
        }
    }
    
    if (iterations.Count > 1)
    {
        Console.WriteLine("\n📈 Refinement Progress:");
        for (int i = 0; i < iterations.Count; i++)
        {
            var improvement = i > 0 ? iterations[i].score - iterations[i-1].score : 0;
            var arrow = improvement > 0 ? "↑" : improvement < 0 ? "↓" : "→";
            Console.WriteLine($"Iteration {i + 1}: {iterations[i].score}/100 {arrow} {(improvement > 0 ? "+" : "")}{improvement}");
        }
    }
}

string ReadMultilineInput()
{
    var lines = new List<string>();
    string? line;
    var emptyLineCount = 0;
    
    while ((line = Console.ReadLine()) != null)
    {
        if (string.IsNullOrEmpty(line))
        {
            emptyLineCount++;
            if (emptyLineCount >= 1)
                break;
        }
        else
        {
            emptyLineCount = 0;
        }
        lines.Add(line);
    }
    
    return string.Join("\n", lines).Trim();
}