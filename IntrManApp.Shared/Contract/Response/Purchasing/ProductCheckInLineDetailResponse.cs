namespace IntrManApp.Shared.Contract;

public class ProductCheckInLineDetailResponse
{
    public Guid? LineId { get; set; }
    public Guid InventoryId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string BatchNumber { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public Guid MeasurementUnitId { get; set; }
    public string MeasurementUnitName { get; set; } = string.Empty;
    public DateTime ProductionDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public Guid LocationId { get; set; }
    public string LocationName { get; set; } = string.Empty;
    public Guid RackingPalletId { get; set; }
    public string RackingPalletName { get; set; } = string.Empty;
    public Guid? CheckinId { get; set; }
    public DateTime CheckinDate { get; set; }
    public int TotalBatches { get; set; }
}