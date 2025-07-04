﻿using IntrManApp.Shared.Contract;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace IntrManHybridApp.UI.Services;


public class ProductionService(HttpClient httpClient, ILogger<ProductionService> logger) : IProductionService
{
    public async Task<Guid> CreateProductionOrderAsync(ProductionOrderRequest request)
    {
        try
        {
            logger.LogInformation($"Creating Production Order: Post {httpClient.BaseAddress}/ProductionOrders");
            var response = await httpClient.PostAsJsonAsync("ProductionOrders", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;

        } catch (Exception ex)
        {
            logger.LogError(ex, "Error creating Production Order");
            return Guid.Empty;
        }
    }

    public async Task<Guid> CreateRawMaterialsCheckout(ProductCheckOutRequest request)
    {
        try
        {
            logger.LogInformation($"Creating Raw Materials Checkout: Post {httpClient.BaseAddress}/RawMaterialCheckOut");
            var response = await httpClient.PostAsJsonAsync("rawMaterialCheckout", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        } catch(Exception ex)
        {
            logger.LogError(ex, "Error creating Raw Materials Checkout");
            return Guid.Empty;
        }
    }

    public Task<bool> DeleteProductionOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<EndProduct>> GetEndProductsAsync()
    {
        try
        {
            logger.LogInformation($"Getting End Products: Get {httpClient.BaseAddress}/endProducts");
            IEnumerable<EndProduct> response = await httpClient.GetFromJsonAsync<IEnumerable<EndProduct>>("EndProducts") ?? [];
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting End Products");
            return [];
        }
        
    }

    public Task<ProductionOrderRequest> GetProductionOrderAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductionItem>> GetProductionOrderItemsAsync(Guid id)
    {
        List<ProductionItem> productionItems = [];
        try
        {
            logger.LogInformation($"Getting Production Order Items: Get {httpClient.BaseAddress}/productionOrderDetails/{id}");
            IEnumerable<ProductionOrderDetailResponse> response = await 
                httpClient.GetFromJsonAsync<IEnumerable<ProductionOrderDetailResponse>>($"productionOrderDetails/{id}") ?? [];

            var groupedItems = response
                .GroupBy(x => new { x.ProductId, x.ProductName, x.ProductMeasurementUnitId, x.ProductMeasurementUnitName, 
                    x.QuantityPerBatch, x.TotalBatches, x.InventoryId, x.BatchNumber, x.Flag, x.LineId})
                .Select(x => new ProductionItem
                {
                    LineId = x.Key.LineId,
                    InventoryId = x.Key.InventoryId,
                    ProductId = x.Key.ProductId,
                    BatchNumber = x.Key.BatchNumber,
                    ProductName = x.Key.ProductName,
                    ProductMeasurementUnitId = x.Key.ProductMeasurementUnitId,
                    ProductMeasurementUnitName = x.Key.ProductMeasurementUnitName,
                    QuantityPerBatch = x.Key.QuantityPerBatch,
                    TotalBatches = x.Key.TotalBatches,
                    Flag = x.Key.Flag,
                    ResourceAllocated = x.Average(y => y.ResourceAllocated),

                    BomItems = x.Select(y => new BomSpecificationResponse
                    {
                        Id = x.Key.InventoryId,
                        RawMaterialId = y.RawMaterialId,
                        RawMaterialName = y.RawMaterialName,
                        RawMaterialMeasurementUnitId = y.RawMaterialMeasurementUnitId,
                        RawMaterialMeasurementUnitName = y.RawMaterialMeasurementUnitName,
                        RawMaterialQuantity = y.RawMaterialQuantity
                    }).ToList()
                }).ToList();

            foreach (var item in groupedItems)
            {
                ProductionItem newItem = new();
                //for (int i = 0; i < item.TotalBatches; i++)
                //{
                    newItem = item.Adapt<ProductionItem>();
                    newItem.BatchIndex = 1;
                    newItem.ProductionDate = response.Where(x => x.InventoryId == item.InventoryId).Select(x => x.ScheduleDate).FirstOrDefault();
                    newItem.ExpirationDate = response.Where(x => x.InventoryId == item.InventoryId).Select(x => x.ExpirationDate).FirstOrDefault();

                for (int j = 0; j < item.BomItems.Count; j++)
                    {

                        var propValue = newItem.GetType().GetProperty($"IngredientName_{j + 1}");
                        if (propValue != null)
                        {
                            logger.LogInformation($"Setting-up: {propValue.Name} => " +
                                $"{newItem.BomItems[j].RawMaterialName} {newItem.BomItems[j].RawMaterialQuantity:N0}");
                            propValue.SetValue(newItem, item.BomItems[j].RawMaterialName);
                            propValue = newItem.GetType().GetProperty($"IngredientQty_{j + 1}");
                            propValue?.SetValue(newItem, $"{item.BomItems[j].RawMaterialQuantity:N0}");
                        } else
                        {                                 
                            logger.LogWarning($"Property not found: IngredientName_{j + 1}");
                        }
                    }
                    productionItems.Add(newItem);
                //}
               logger.LogInformation($"Production Item: {newItem.ProductName}, {newItem.BomItems.Count} Ingredients");
            }

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Production Order Items");
        }
        return productionItems;
    }

    public async Task<IEnumerable<ProductionItem>> GetProductionOrderItemsByDateAsync(DateTime date)
    {
        List<ProductionItem> productionItems = [];
        try
        {
            logger.LogInformation($"Getting Production Order Items by Date: Get {httpClient.BaseAddress}/productionOrderDetailsByDate/{date}");
            IEnumerable<ProductionOrderDetailResponse> response = await
                httpClient.GetFromJsonAsync<IEnumerable<ProductionOrderDetailResponse>>($"productionOrderDetailsByDate/{date:yyyy-MM-dd}") ?? [];

            var groupedItems = response
                .GroupBy(x => new {
                    x.LineId,
                    x.ProductId,
                    x.ProductName,
                    x.ProductMeasurementUnitId,
                    x.ProductMeasurementUnitName,
                    x.QuantityPerBatch,
                    x.TotalBatches,
                    x.InventoryId,
                    x.BatchNumber,
                    x.Flag,
                })
                .Select(x => new ProductionItem
                {
                    LineId = x.Key.LineId,
                    InventoryId = x.Key.InventoryId,
                    ProductId = x.Key.ProductId,
                    BatchNumber = x.Key.BatchNumber,
                    ProductName = x.Key.ProductName,
                    ProductMeasurementUnitId = x.Key.ProductMeasurementUnitId,
                    ProductMeasurementUnitName = x.Key.ProductMeasurementUnitName,
                    QuantityPerBatch = x.Key.QuantityPerBatch,
                    TotalBatches = x.Key.TotalBatches,
                    Flag = x.Key.Flag,
                    ResourceAllocated = x.Average(y => y.ResourceAllocated),
                    BomItems = x.Select(y => new BomSpecificationResponse
                    {
                        Id = x.Key.InventoryId,
                        RawMaterialId = y.RawMaterialId,
                        RawMaterialName = y.RawMaterialName,
                        RawMaterialMeasurementUnitId = y.RawMaterialMeasurementUnitId,
                        RawMaterialMeasurementUnitName = y.RawMaterialMeasurementUnitName,
                        RawMaterialQuantity = y.RawMaterialQuantity
                    }).ToList()
                }).ToList();

            foreach (var item in groupedItems)
            {
                ProductionItem newItem = new();
                //for (int i = 0; i < item.TotalBatches; i++)
                //{
                    newItem = item.Adapt<ProductionItem>();
                    newItem.ProductionDate = response.Where(x=>x.InventoryId == item.InventoryId).Select(x=>x.ScheduleDate).FirstOrDefault();
                    newItem.ExpirationDate = response.Where(x => x.InventoryId == item.InventoryId).Select(x => x.ExpirationDate).FirstOrDefault();
                    newItem.BatchIndex = 1;


                    for (int j = 0; j < item.BomItems.Count; j++)
                    {

                        var propValue = newItem.GetType().GetProperty($"IngredientName_{j + 1}");
                        if (propValue != null)
                        {
                            logger.LogInformation($"Setting-up: {propValue.Name} => " +
                                $"{newItem.BomItems[j].RawMaterialName} {newItem.BomItems[j].RawMaterialQuantity:N0}");
                            propValue.SetValue(newItem, item.BomItems[j].RawMaterialName);
                            propValue = newItem.GetType().GetProperty($"IngredientQty_{j + 1}");
                            propValue?.SetValue(newItem, $"{item.BomItems[j].RawMaterialQuantity:N0}");
                        }
                        else
                        {
                            logger.LogWarning($"Property not found: IngredientName_{j + 1}");
                        }
                    }
                    productionItems.Add(newItem);
                //}
                logger.LogInformation($"Production Item: {newItem.ProductName}, {newItem.BomItems.Count} Ingredients");
            }

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Production Order Items");
        }
        return productionItems;
    }

    public async Task<IEnumerable<ProductionItem>> GetProductionOrderItemsByStatusAsync(int flag)
    {
        List<ProductionItem> productionItems = [];
        try
        {
            logger.LogInformation($"Getting Production Order Items by Status: Get {httpClient.BaseAddress}/productionOrderDetailsByStatus/{flag}");
            IEnumerable<ProductionOrderDetailResponse> response = await
                httpClient.GetFromJsonAsync<IEnumerable<ProductionOrderDetailResponse>>($"productionOrderDetailsByStatus/{flag}") ?? [];

            var groupedItems = response
                .GroupBy(x => new {
                    x.LineId,
                    x.ProductId,
                    x.ProductName,
                    x.ProductMeasurementUnitId,
                    x.ProductMeasurementUnitName,
                    x.QuantityPerBatch,
                    x.TotalBatches,
                    x.InventoryId,
                    x.BatchNumber,
                    x.Flag,
                })
                .Select(x => new ProductionItem
                {
                    LineId = x.Key.LineId,
                    InventoryId = x.Key.InventoryId,
                    ProductId = x.Key.ProductId,
                    BatchNumber = x.Key.BatchNumber,
                    ProductName = x.Key.ProductName,
                    ProductMeasurementUnitId = x.Key.ProductMeasurementUnitId,
                    ProductMeasurementUnitName = x.Key.ProductMeasurementUnitName,
                    QuantityPerBatch = x.Key.QuantityPerBatch,
                    TotalBatches = x.Key.TotalBatches,
                    Flag = x.Key.Flag,
                    ResourceAllocated = x.Average(y => y.ResourceAllocated),
                    BomItems = x.Select(y => new BomSpecificationResponse
                    {
                        Id = x.Key.InventoryId,
                        RawMaterialId = y.RawMaterialId,
                        RawMaterialName = y.RawMaterialName,
                        RawMaterialMeasurementUnitId = y.RawMaterialMeasurementUnitId,
                        RawMaterialMeasurementUnitName = y.RawMaterialMeasurementUnitName,
                        RawMaterialQuantity = y.RawMaterialQuantity
                    }).ToList()
                }).ToList();

            foreach (var item in groupedItems)
            {
                ProductionItem newItem = new();
                //for (int i = 0; i < item.TotalBatches; i++)
                //{
                newItem = item.Adapt<ProductionItem>();
                newItem.ProductionDate = response.Where(x => x.InventoryId == item.InventoryId).Select(x => x.ScheduleDate).FirstOrDefault();
                newItem.ExpirationDate = response.Where(x => x.InventoryId == item.InventoryId).Select(x => x.ExpirationDate).FirstOrDefault();
                newItem.BatchIndex = 1;


                for (int j = 0; j < item.BomItems.Count; j++)
                {

                    var propValue = newItem.GetType().GetProperty($"IngredientName_{j + 1}");
                    if (propValue != null)
                    {
                        logger.LogInformation($"Setting-up: {propValue.Name} => " +
                            $"{newItem.BomItems[j].RawMaterialName} {newItem.BomItems[j].RawMaterialQuantity:N0}");
                        propValue.SetValue(newItem, item.BomItems[j].RawMaterialName);
                        propValue = newItem.GetType().GetProperty($"IngredientQty_{j + 1}");
                        propValue?.SetValue(newItem, $"{item.BomItems[j].RawMaterialQuantity:N0}");
                    }
                    else
                    {
                        logger.LogWarning($"Property not found: IngredientName_{j + 1}");
                    }
                }
                productionItems.Add(newItem);
                //}
                logger.LogInformation($"Production Item: {newItem.ProductName}, {newItem.BomItems.Count} Ingredients");
            }

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting Production Order Items");
        }
        return productionItems;
    }

    public Task<IEnumerable<ProductionOrderRequest>> GetProductionOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<InventoryItemDetail>> GetRawMaterialsForProductionAsync()
    {
        IEnumerable<InventoryItemDetail> rawMaterials = [];
        try
        {
            logger.LogInformation("Calling api/rawMaterialsForProduction");
            rawMaterials = await httpClient.GetFromJsonAsync<IEnumerable<InventoryItemDetail>>("rawMaterialsForProduction") ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error getting Raw Materials for Production{Environment.NewLine}{ex}");
        }

        return rawMaterials;
    }

    public async Task<bool> StartItemProduction(Guid inventoryId)
    {
        bool result = false;
        try
        {
            result = await httpClient.GetFromJsonAsync<bool>($"startItemProduction/{inventoryId}");
        } catch (Exception ex)
        {
            logger.LogError(ex, $"Error starting Production for Inventory ID: {inventoryId}");
        }
        return result;
    }

    public async Task<bool> AbortItemProduction(Guid inventoryId)
    {
        bool result = false;
        try
        {
            result = await httpClient.DeleteFromJsonAsync<bool>($"abortItemProduction/{inventoryId}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error starting Production for Inventory ID: {inventoryId}");
        }
        return result;
    }

    public async Task<IEnumerable<InventoryItemDetail>> GetRunningProductionItemsAsync()
    {
        IEnumerable<InventoryItemDetail> endProductsInProgress = [];
        try
        {
            logger.LogInformation("Calling api/runningProductionItems");
            endProductsInProgress = await httpClient.GetFromJsonAsync<IEnumerable<InventoryItemDetail>>("runningProductionItems") ?? [];
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error getting Raw Materials for Production{Environment.NewLine}{ex}");
        }

        return endProductsInProgress;
    }

    public async Task<Guid> CreateFinishedProductCheckin(FinishedProductInternalCheckinRequest request)
    {
        try
        {
            var json = JsonSerializer.Serialize(request);
            logger.LogInformation($"Creating Finished Product Internal Checkin: Post {httpClient.BaseAddress}/CompleteProduction/\n" +
                $"---------------------------------------------------------------------------------------------------------------\n" +
                $"{json}\n");
            var response = await httpClient.PostAsJsonAsync("CompleteProduction", request);
            return response.Content.ReadFromJsonAsync<Guid>().Result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating Finished Product Internal Checkin");
            return Guid.Empty;
        }
    }

    public async Task<IEnumerable<BomAllocationResponse>> GetBomAllocationAsync(Guid inventoryId)
    {
        try
        {
            logger.LogInformation($"Getting BOM Allocation: Get {httpClient.BaseAddress}/GetBomAllocation/{inventoryId}");
            
            IEnumerable<BomAllocationResponse> bomAllocations =
                await httpClient.GetFromJsonAsync<IEnumerable<BomAllocationResponse>>($"GetBomAllocation/{inventoryId}") ?? [];

            return bomAllocations;
         
        } catch(Exception ex)
        {
            logger.LogError(ex, "Error getting BOM Allocation");
            return [];
        }
    }

    public IEnumerable<BomAllocationResponse> GetBomAllocation(Guid inventoryId)
    {
        try
        {
            logger.LogInformation($"Getting BOM Allocation: Get {httpClient.BaseAddress}/GetBomAllocation/{inventoryId}");

            return 
                httpClient.GetFromJsonAsync<IEnumerable<BomAllocationResponse>>($"GetBomAllocation/{inventoryId}").Result  ?? [];
            
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting BOM Allocation");
            return [];
        }
    }
}