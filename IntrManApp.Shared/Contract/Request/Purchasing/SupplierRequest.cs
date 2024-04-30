namespace IntrManApp.Shared.Contract;

public class SupplierRequest
{
    public Guid BusinessEntityId { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
