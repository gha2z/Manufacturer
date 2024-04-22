namespace IntrManApp.Shared.Contract;

public class CustomerRequest
{
    public Guid BusinessEntityId { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
}

public class UpdateCustomerRequest
{
    public Guid Id { get; set; }
    public Guid BusinessEntityId { get; set; } = Guid.Empty;
    public string Name { get; set; } = string.Empty;
}

public class DeleteCustomerRequest
{
    public Guid BusinessEntityId { get; set; } = Guid.Empty;
}
