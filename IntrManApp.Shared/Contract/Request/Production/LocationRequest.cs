namespace IntrManApp.Shared.Contract
{
    public class LocationRequest
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
