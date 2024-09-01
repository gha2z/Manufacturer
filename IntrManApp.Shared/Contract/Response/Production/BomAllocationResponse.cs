using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntrManApp.Shared.Contract;

public class BomAllocationResponse
{
    public Guid RawMaterialId { get; set; }
    public string RawMaterialNames { get; set; } = string.Empty;
    public decimal WeightRequired { get; set; }
    public Guid BomMeasurementUnitId { get; set; }
    public string BomMeasurementUnitInitial { get; set; } = string.Empty;
    public string BomMeasurementUnitName { get; set; } = string.Empty;
    public Guid InventoryId { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
    public decimal WeightAllocated { get; set; }
    public Guid WeightAllocatedMeasurementUnitId { get; set; } 
    public string WeightAllocatedMeasurementUnitInitial { get; set; } = string.Empty;
    public string WeightAllocatedMeasurementUnitName { get; set; } = string.Empty;
    public DateTime? ProductionDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
}

