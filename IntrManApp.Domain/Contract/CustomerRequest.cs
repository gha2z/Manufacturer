namespace IntrManApp.Shared.Contract
{
    public class CreateCustomerRequest
    {
        public Guid BusinessEntityId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateCustomerRequest
    {
        public Guid BusinessEntityId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class DeleteCustomerRequest
    {
        public Guid BusinessEntityId { get; set; }
    }
}
