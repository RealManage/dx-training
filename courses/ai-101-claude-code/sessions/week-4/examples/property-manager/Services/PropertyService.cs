using Microsoft.EntityFrameworkCore;
using RealManage.PropertyManager.Data;
using RealManage.PropertyManager.Models;

namespace RealManage.PropertyManager.Services;

public class PropertyService
{
    private readonly PropertyContext _context;
    
    public PropertyService(PropertyContext context)
    {
        _context = context;
    }
    
    public async Task<List<Property>> GetAllPropertiesAsync()
    {
        return await _context.Properties
            .OrderBy(p => p.Address)
            .ToListAsync();
    }
    
    public async Task<Property?> GetPropertyByIdAsync(int id)
    {
        return await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<Property> AddPropertyAsync(Property property)
    {
        // TODO: Add validation (via TDD)
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
        return property;
    }
    
    public async Task<List<Property>> SearchPropertiesAsync(string searchTerm)
    {
        // TODO: This needs better testing - edge cases not covered
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<Property>();
        
        var term = searchTerm.ToLower();
        return await _context.Properties
            .Where(p => p.Address.ToLower().Contains(term) ||
                       p.City.ToLower().Contains(term) ||
                       p.State.ToLower().Contains(term))
            .ToListAsync();
    }
    
    public async Task<bool> UpdatePropertyAsync(Property property)
    {
        // TODO: Not tested at all!
        try
        {
            property.LastModifiedDate = DateTime.UtcNow;
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    // TODO: Add these methods via TDD:
    // - AddValuationAsync(int propertyId, decimal amount, DateTime date, string source)
    // - GetValuationHistoryAsync(int propertyId)
    // - CalculateAppreciationRateAsync(int propertyId)
    // - UpdateOccupancyStatusAsync(int propertyId, OccupancyStatus status)
}