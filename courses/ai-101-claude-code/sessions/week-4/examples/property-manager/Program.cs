using Microsoft.EntityFrameworkCore;
using RealManage.PropertyManager.Data;
using RealManage.PropertyManager.Services;

var builder = Host.CreateApplicationBuilder(args);

// Add services
builder.Services.AddDbContext<PropertyContext>(options =>
    options.UseSqlite("Data Source=properties.db"));

builder.Services.AddScoped<PropertyService>();

var host = builder.Build();

// Ensure database is created
using (var scope = host.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PropertyContext>();
    context.Database.EnsureCreated();
}

// Simple CLI menu
Console.WriteLine("🏠 RealManage Property Manager");
Console.WriteLine("================================");

while (true)
{
    Console.WriteLine("\n1. List Properties");
    Console.WriteLine("2. Add Property");
    Console.WriteLine("3. Search Properties");
    Console.WriteLine("4. Property Details");
    Console.WriteLine("5. Exit");
    Console.Write("\nSelect option: ");
    
    var choice = Console.ReadLine();
    
    if (choice == null) // Handle non-interactive mode
        return;
    
    using var scope = host.Services.CreateScope();
    var propertyService = scope.ServiceProvider.GetRequiredService<PropertyService>();
    
    switch (choice)
    {
        case "1":
            await ListProperties(propertyService);
            break;
        case "2":
            await AddProperty(propertyService);
            break;
        case "3":
            await SearchProperties(propertyService);
            break;
        case "4":
            await ShowPropertyDetails(propertyService);
            break;
        case "5":
            return;
        default:
            Console.WriteLine("Invalid option");
            break;
    }
}

async Task ListProperties(PropertyService service)
{
    var properties = await service.GetAllPropertiesAsync();
    foreach (var prop in properties)
    {
        Console.WriteLine($"{prop.Id}: {prop.Address} - {prop.City}, {prop.State}");
    }
}

async Task AddProperty(PropertyService service)
{
    Console.Write("Address: ");
    var address = Console.ReadLine() ?? "";
    Console.Write("City: ");
    var city = Console.ReadLine() ?? "";
    Console.Write("State: ");
    var state = Console.ReadLine() ?? "";
    Console.Write("Zip: ");
    var zip = Console.ReadLine() ?? "";
    
    var property = new Property
    {
        Address = address,
        City = city,
        State = state,
        ZipCode = zip
    };
    
    await service.AddPropertyAsync(property);
    Console.WriteLine("Property added successfully!");
}

async Task SearchProperties(PropertyService service)
{
    Console.Write("Search term: ");
    var term = Console.ReadLine() ?? "";
    var results = await service.SearchPropertiesAsync(term);
    
    foreach (var prop in results)
    {
        Console.WriteLine($"{prop.Id}: {prop.Address} - {prop.City}, {prop.State}");
    }
}

async Task ShowPropertyDetails(PropertyService service)
{
    Console.Write("Property ID: ");
    if (int.TryParse(Console.ReadLine(), out var id))
    {
        var property = await service.GetPropertyByIdAsync(id);
        if (property != null)
        {
            Console.WriteLine($"Address: {property.Address}");
            Console.WriteLine($"City: {property.City}");
            Console.WriteLine($"State: {property.State}");
            Console.WriteLine($"Zip: {property.ZipCode}");
            // TODO: Show valuation history
            // TODO: Show maintenance requests
            // TODO: Show occupancy status
        }
        else
        {
            Console.WriteLine("Property not found");
        }
    }
}