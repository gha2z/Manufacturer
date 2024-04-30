namespace IntrManApp.Shared.Contract;

public class CustomerResponse
{
    public Guid BusinessEntityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
