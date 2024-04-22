namespace IntrManApp.Shared.Contract;

public class CreateRackingPalletRequest
{
    public string Col { get; set; } = string.Empty;
    public short Row { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class UpdateRackingPalletRequest
{
    public string Col { get; set; } = string.Empty;
    public short Row { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class DeleteRackingPalletRequest
{
    public Guid Id { get; set; }
}
