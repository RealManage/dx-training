namespace RealManage.PropertyManager.Models;

public class Property
{
    public int Id { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastModifiedDate { get; set; }
    
    // TODO: Add these via TDD
    // public List<Valuation> Valuations { get; set; } = new();
    // public List<MaintenanceRequest> MaintenanceRequests { get; set; } = new();
    // public OccupancyStatus OccupancyStatus { get; set; }
    // public decimal? CurrentRent { get; set; }
}