namespace IntrManApp.Shared.Contract
{
    public class CreateProductCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateProductCategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class DeleteProductCategoryRequest 
    {
        public Guid Id { get; set; }
    }
}
