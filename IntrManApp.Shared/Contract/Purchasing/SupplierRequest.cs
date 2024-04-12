namespace IntrManApp.Shared.Contract
{
    public class CreateSupplierRequest
    {
        public Guid BusinessEntityId { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateSupplierRequest
    {
        public Guid BusinessEntityId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class DeleteSupplierRequest
    {
        public Guid BusinessEntityId { get; set; }
    }
}
