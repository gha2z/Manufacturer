namespace IntrManApp.Shared.Common;

public enum InventoryFlag
{
    Checkin = 1,
    Checkout = 2,
    Return = 3,
    Production_Waiting = 4,
    Production_NotStarted = 5,
    Production_InProgress = 6,
    Production_Completed = 7,
    Production_Cancelled = 8,
    Checkin_FromProduction = 9,
    New_Delivery_Order = 10,
    Dispathing = 11,
    Dispatched = 12
}