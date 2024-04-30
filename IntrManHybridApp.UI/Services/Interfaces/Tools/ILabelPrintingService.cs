using IntrManApp.Shared.Contract;

namespace IntrManHybridApp.UI.Services
{
    public interface ILabelPrintingService
    {
        public  Task<IEnumerable<string>> PrintCartonIds(IEnumerable<ProductCheckInLineDetailResponse> items, string? printerName);
    }
}
