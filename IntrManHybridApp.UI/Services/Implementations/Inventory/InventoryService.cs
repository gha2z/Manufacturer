
using IntrManApp.Shared.Contract;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace IntrManHybridApp.UI.Services;

public class InventoryService(HttpClient httpClient, ILogger<InventoryService> logger) : IInventoryService
{
    public async Task<Guid> CreateProductCheckout(ProductCheckOutRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            logger.LogInformation($"Creating Inventory Transfer: Post {httpClient.BaseAddress}/productInternalCheckOut/{json}");
            var response = await httpClient.PostAsJsonAsync("productInternalCheckout", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating Raw Materials Checkout");
            return Guid.Empty;
        }
    }

    public async Task<IEnumerable<InventoryItemExtendedFlag>> GetFinishedProductInventoriesAsync()
    {
        List<InventoryItemExtendedFlag> inventoryItems = [];
        try
        {
            logger.LogInformation($"Getting Finished Product Inventory Items : Get {httpClient.BaseAddress}/GetFinishedProductInventories");
            IEnumerable<InventoryItem> response = await
                httpClient.GetFromJsonAsync<IEnumerable<InventoryItem>>($"FinishedProductInventories") ?? [];

            var groupedItems = response
                .GroupBy(x => new {
                    x.CategoryId,
                    x.CategoryName,
                    x.ProductId,
                    x.ProductNumber,
                    x.ProductName,
                    x.Weight,
                    x.MeasurementUnitName,
                    x.Location,
                    x.LocationId
                })
                .Select(x => new InventoryItem
                {
                    CategoryId = x.Key.CategoryId,
                    CategoryName = x.Key.CategoryName,
                    ProductId = x.Key.ProductId,
                    ProductNumber = x.Key.ProductNumber,
                    ProductName = x.Key.ProductName,
                    Weight = x.Key.Weight,
                    MeasurementUnitName = x.Key.MeasurementUnitName,
                    Location = x.Key.Location,
                    LocationId = x.Key.LocationId
                }).ToList();

            foreach (var item in groupedItems)
            {
                InventoryItemExtendedFlag newItem = new()
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    ProductId = item.ProductId,
                    ProductNumber = item.ProductNumber,
                    ProductName = item.ProductName,
                    Weight = item.Weight,
                    MeasurementUnitName = item.MeasurementUnitName,
                    Location = item.Location,
                    LocationId = item.LocationId
                };
                var qtyFlag = response.FirstOrDefault(x => 
                    x.ProductId == item.ProductId && x.Weight == item.Weight && 
                    x.LocationId == item.LocationId && (x.Flag == 7 || x.Flag == 8));
                if (qtyFlag != null) newItem.QtyAvailable = qtyFlag.Quantity;

                qtyFlag = response.FirstOrDefault(x => 
                    x.ProductId == item.ProductId && x.Weight == item.Weight &&
                    x.LocationId == item.LocationId && x.Flag >= 9);
                if (qtyFlag != null) newItem.QtyReserved = qtyFlag.Quantity;
                
                qtyFlag = response.FirstOrDefault(x => 
                    x.ProductId == item.ProductId && x.Weight == item.Weight &&
                    x.LocationId == item.LocationId && x.Flag == 6);
                if (qtyFlag != null) newItem.QtyInProduction = qtyFlag.Quantity;
                
                qtyFlag = response.FirstOrDefault(x => 
                    x.ProductId == item.ProductId && x.Weight == item.Weight &&
                    x.LocationId == item.LocationId && x.Flag == 5);
                if (qtyFlag != null) newItem.QtyToBeProduced = qtyFlag.Quantity;

                inventoryItems.Add(newItem);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Finished Product Inventories ...");
        }
        return inventoryItems;
    }

    public IEnumerable<InventoryLedger> GetInventoryLedger(InventoryLedgerRequest request)
    {
        IEnumerable<InventoryLedger> inventoryLedger = [];
        try
        {

            logger.LogInformation($"Getting Inventory Ledger: Get {httpClient.BaseAddress}/finishedProductInventoryLedger");
            //var response = await httpClient.PostAsJsonAsync("productCheckout", request);
            //return response.Content.ReadFromJsonAsync<Guid>().Result;
            var response = httpClient.PostAsJsonAsync($"finishedProductInventoryLedger", request);
            inventoryLedger = response.Result.Content.ReadFromJsonAsync<IEnumerable<InventoryLedger>>().Result ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error getting Inventory Ledger for Product ID: {request.ProductId} at Location ID: {request.LocationId}");
        }
        return inventoryLedger;
    }

    public async Task<IEnumerable<InventoryItem>> GetRawMaterialInventoriesAsync()
    {
        List<InventoryItem> inventoryItems = [];
        try
        {
            logger.LogInformation($"Getting Raw Material Inventory Items : Get {httpClient.BaseAddress}/RawMaterialInventories");
            IEnumerable<InventoryItem> response = await
                httpClient.GetFromJsonAsync<IEnumerable<InventoryItem>>($"RawMaterialInventories") ?? [];

            inventoryItems = response
                .GroupBy(x => new {
                    x.CategoryId,
                    x.CategoryName,
                    x.ProductId,
                    x.ProductNumber,
                    x.ProductName,
                    x.MeasurementUnitName,
                    x.Location,
                    x.LocationId
                })
                .Select(x => new InventoryItem
                {
                    CategoryId = x.Key.CategoryId,
                    CategoryName = x.Key.CategoryName,
                    ProductId = x.Key.ProductId,
                    ProductNumber = x.Key.ProductNumber,
                    ProductName = x.Key.ProductName,
                    Weight = x.Sum(y => y.Weight),
                    Quantity = x.Sum(y => y.Quantity),
                    MeasurementUnitName = x.Key.MeasurementUnitName,
                    Location = x.Key.Location,
                    LocationId = x.Key.LocationId
                }).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Finished Product Inventories ...");
        }
        return inventoryItems;
    }

    public IEnumerable<InventoryLedger> GetRawMaterialInventoryLedger(InventoryLedgerRequest request)
    {
        IEnumerable<InventoryLedger> inventoryLedger = [];
        try
        {

            logger.LogInformation($"Getting Inventory Ledger: Get {httpClient.BaseAddress}/finishedProcutInventoryLedger");
            //var response = await httpClient.PostAsJsonAsync("productCheckout", request);
            //return response.Content.ReadFromJsonAsync<Guid>().Result;
            var response = httpClient.PostAsJsonAsync($"rawMaterialInventoryLedger", request);
            inventoryLedger = response.Result.Content.ReadFromJsonAsync<IEnumerable<InventoryLedger>>().Result ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error getting Inventory Ledger for Product ID: {request.ProductId} at Location ID: {request.LocationId}");
        }
        return inventoryLedger;
    }

    public async Task<IEnumerable<RawMaterialTrackingResponse>> RawMaterialTracking(RawMaterialTrackingRequest request)
    {
        IEnumerable<RawMaterialTrackingResponse> results = [];
        try
        {
            var json = JsonSerializer.Serialize(request);
            logger.LogInformation(
                $"Getting Inventory Ledger: Get {httpClient.BaseAddress}/rawMaterialTracking{Environment.NewLine}{json}");

            var response = await httpClient.PostAsJsonAsync($"rawMaterialTracking", request);
            var tmpResults = await response.Content.ReadFromJsonAsync<IEnumerable<RawMaterialTrackingResponse>>() ?? [];

            results = tmpResults
              .GroupBy(x => new {
                  x.RawMaterialId,
                  x.InventoryId,
                  x.CartonId,
                  x.ProductName,
                  x.Weight,
                  x.MeasurementUnitName,
                  x.LocationName,
                  x.ColRow,
                  x.InitialWeight,
                  x.SupplierName,
                  x.ProductionDate,
                  x.ExpiryDate,
              })
              .Select(x => new RawMaterialTrackingResponse
              {
                  RawMaterialId = x.Key.RawMaterialId,
                  InventoryId = x.Key.InventoryId,
                  CartonId = x.Key.CartonId,
                  ProductName = x.Key.ProductName,
                  Weight = x.Key.Weight,
                  MeasurementUnitName = x.Key.MeasurementUnitName,
                  LocationName = x.Key.LocationName,
                  ColRow = x.Key.ColRow,
                  InitialWeight = x.Key.InitialWeight,
                  SupplierName = x.Key.SupplierName,
                  ProductionDate = x.Key.ProductionDate,
                  ExpiryDate = x.Key.ExpiryDate,
              }).ToList();

        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error tracking ");
        }
        return results;
    }

    public IEnumerable<InventoryLedger> GetRawMaterialInventoryLedgerById(Guid id)
    {
        IEnumerable<InventoryLedger> inventoryLedger = [];
        try
        {

            logger.LogInformation($"Getting Inventory Ledger: Get {httpClient.BaseAddress}/rawMaterialInventoryLedger");


            inventoryLedger = httpClient.GetFromJsonAsync<IEnumerable<InventoryLedger>>($"rawMaterialInventoryLedger/{id}").Result ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error getting Inventory Ledger for InventoryID: {id}");
        }
        return inventoryLedger;
    }

    public async Task<IEnumerable<InventoryItemDetail>> GetInventoryItemsByLocation()
    {
        IEnumerable<InventoryItemDetail> inventories = [];
        try
        {

            logger.LogInformation($"Getting Inventory items by location: Get {httpClient.BaseAddress}/inventoryItemsByLocation");
            inventories = await httpClient.GetFromJsonAsync<IEnumerable<InventoryItemDetail>>($"inventoryItemsByLocation") ?? [];

        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error getting Inventory Ledger ");
        }
        return inventories;
    }

    public async Task<Guid> CreateStockAdjustment(StockAdjustmentRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        logger.LogInformation($"CreateStockAdjustment:{httpClient.BaseAddress}/stockAdjustment/{json}");
        try
        {
            var response = await httpClient.PostAsJsonAsync("stockAdjustment", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"CreateStockAdjustment:{httpClient.BaseAddress}/stockAdjustment/{json}" +
                $"{Environment.NewLine}{ex.Message}{ex}");
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<StockAdjustmentReason>> GetStockAdjustmentReasonsAsync()
    {
        try
        {

            logger.LogInformation($"Getting stock adjustment reasons: Get {httpClient.BaseAddress}/StockAdjustmentReasons");
            return await httpClient.GetFromJsonAsync<IEnumerable<StockAdjustmentReason>>($"StockAdjustmentReasons") ?? [];

        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"CreateStockAdjustment:{httpClient.BaseAddress}/stockAdjustmentReasons" +
               $"{Environment.NewLine}{ex.Message}{ex}");
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<EndProductItemDetail>> GetPackagedProductsByLocationAsync()
    {
        try
        {
            logger.LogInformation("Getting Packaged Products by Location");
            return await httpClient.GetFromJsonAsync<IEnumerable<EndProductItemDetail>>("PackagedProductsByLocation") ?? [];
        } catch(Exception ex)
        {
            logger.LogError(ex, $"Error getting Packaged Products by Location");
            throw new Exception(ex.Message);
        }
    }

    public async Task<Guid> CreateEndProductStockAdjustment(EndProductStockAdjustmentRequest request)
    {
        var json = JsonSerializer.Serialize(request);
        logger.LogInformation($"CreateEndProductStockAdjustment:{httpClient.BaseAddress}/EndProductStockAdjustment/{json}");
        try
        {
            var response = await httpClient.PostAsJsonAsync("EndProductStockAdjustment", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"CreateEndProductStockAdjustment:{httpClient.BaseAddress}/EndProductStockAdjustment/{json}" +
                $"{Environment.NewLine}{ex.Message}{ex}");
            throw new Exception(ex.Message);
        }
    }

    public async Task<Guid> CreateEndProductCheckout(EndProductCheckOutRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            logger.LogInformation($"Creating End Product Checkout: Post {httpClient.BaseAddress}/EndProductInternalCheckOut/{json}");
            var response = await httpClient.PostAsJsonAsync("EndProductInternalCheckout", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating Raw Materials Checkout");
            return Guid.Empty;
        }
    }
}
