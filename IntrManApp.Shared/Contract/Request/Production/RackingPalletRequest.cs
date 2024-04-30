namespace IntrManApp.Shared.Contract;

public class RackingPalletRequest
{
    public Guid Id { get; set; } = Guid.Empty;    
    public string Col { get; set; } = string.Empty;
    public short Row { get; set; }
    public string Description { get; set; } = string.Empty;
}