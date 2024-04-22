namespace IntrManApp.Shared.Contract;

public class ProductCheckInLineDetailResponse
{
    public Guid? LineId { get; set; }
    public Guid InventoryId { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
}