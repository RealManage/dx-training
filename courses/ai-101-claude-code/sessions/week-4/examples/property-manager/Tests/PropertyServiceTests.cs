using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RealManage.PropertyManager.Data;
using RealManage.PropertyManager.Models;
using RealManage.PropertyManager.Services;

namespace RealManage.PropertyManager.Tests;

public class PropertyServiceTests : IDisposable
{
    private readonly PropertyContext _context;
    private readonly PropertyService _service;
    
    public PropertyServiceTests()
    {
        var options = new DbContextOptionsBuilder<PropertyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new PropertyContext(options);
        _context.Database.EnsureCreated();
        _service = new PropertyService(_context);
    }
    
    [Fact]
    public async Task GetAllProperties_ReturnsAllProperties()
    {
        // Arrange
        var property = new Property
        {
            Address = "999 Test St",
            City = "TestCity",
            State = "TX",
            ZipCode = "99999"
        };
        await _service.AddPropertyAsync(property);
        
        // Act
        var result = await _service.GetAllPropertiesAsync();
        
        // Assert
        result.Should().HaveCountGreaterThan(0);
        result.Should().Contain(p => p.Address == "999 Test St");
    }
    
    [Fact]
    public async Task AddProperty_ValidProperty_AddsSuccessfully()
    {
        // Arrange
        var property = new Property
        {
            Address = "111 New St",
            City = "NewCity",
            State = "TX",
            ZipCode = "11111"
        };
        
        // Act
        var result = await _service.AddPropertyAsync(property);
        
        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
    }
    
    [Fact]
    public async Task GetPropertyById_ExistingId_ReturnsProperty()
    {
        // Arrange
        var property = new Property
        {
            Address = "222 Find St",
            City = "FindCity",
            State = "TX",
            ZipCode = "22222"
        };
        var added = await _service.AddPropertyAsync(property);
        
        // Act
        var result = await _service.GetPropertyByIdAsync(added.Id);
        
        // Assert
        result.Should().NotBeNull();
        result!.Address.Should().Be("222 Find St");
    }
    
    [Fact]
    public async Task SearchProperties_WithMatchingAddress_ReturnsResults()
    {
        // Arrange
        var property = new Property
        {
            Address = "333 Search St",
            City = "SearchCity",
            State = "TX",
            ZipCode = "33333"
        };
        await _service.AddPropertyAsync(property);
        
        // Act
        var results = await _service.SearchPropertiesAsync("Search");
        
        // Assert
        results.Should().HaveCountGreaterThan(0);
        results.Should().Contain(p => p.Address.Contains("Search"));
    }
    
    // TODO: Missing test coverage for:
    // - GetPropertyById with non-existent ID
    // - SearchProperties with empty/null term
    // - SearchProperties with no matches
    // - UpdateProperty (not tested at all!)
    // - Error handling scenarios
    // - Edge cases
    
    public void Dispose()
    {
        _context.Dispose();
    }
}