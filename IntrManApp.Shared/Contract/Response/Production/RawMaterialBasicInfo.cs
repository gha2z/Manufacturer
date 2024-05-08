namespace IntrManApp.Shared.Contract;

public class RawMaterialBasicInfo
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Names { get; set; } = string.Empty;
    public Guid MeasurementUnitId { get; set; } = Guid.Empty;
    public string MeasurementUnitName { get; set; } = string.Empty;
}