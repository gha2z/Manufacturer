namespace IntrManApp.Shared.Contract
{
    public class CreateLocationRequest
    {
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateLocationRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class DeleteLocationRequest
    {
        public Guid Id { get; set; }
    }
}
