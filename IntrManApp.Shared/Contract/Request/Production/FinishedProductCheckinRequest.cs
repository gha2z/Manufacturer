namespace IntrManApp.Shared.Contract;

public class FinishedProductInternalCheckinRequest
{
    public Guid Id { get; set; } = Guid.Empty;
    public DateTime? CheckInDate { get; set; }
    public byte? CheckInType { get; set; } 
    public byte? RevisionNumber { get; set; }
    public DateTime? ModifierDate { get; set; }
    public List<FinishedProductInternalCheckinLineRequest> ProductInternalCheckinLines { get; set; } = [];
}

public class FinishedProductInternalCheckinLineRequest
{
    public string BatchNumber { get; set; } = string.Empty;
    public Guid InventoryId { get; set; }
    public Guid MeasurementUnitId { get; set; }
    public decimal Quantity { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public Guid LocationId { get; set; }
    public Guid RackingPalletId { get; set; }
    public InventoryItem? FinishedProduct { get; set; }
    public LocationResponse? Location { get; set; }
    public RackingPalletResponse? RackingPallet { get; set; }
}