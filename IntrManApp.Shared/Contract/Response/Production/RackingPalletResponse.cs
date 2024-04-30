namespace IntrManApp.Shared.Contract;

public class RackingPalletResponse
{
    public Guid Id { get; set; }
    public string Col { get; set; } = string.Empty;
    public short Row { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ColRow { get; set; } = string.Empty;
}
