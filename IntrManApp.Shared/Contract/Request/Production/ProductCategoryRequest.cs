namespace IntrManApp.Shared.Contract
{
    public class ProductCategoryRequest
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
