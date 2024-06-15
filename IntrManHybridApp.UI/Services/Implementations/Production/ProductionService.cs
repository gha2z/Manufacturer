using IntrManApp.Shared.Contract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using System.Text.Json;
using IntrManApp.Shared.Common;

namespace IntrManHybridApp.UI.Services
{
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
                logger.LogInformation($"Creating Raw Materials Checkout: Post {httpClient.BaseAddress}/ProductCheckOut");
                var response = await httpClient.PostAsJsonAsync("productCheckout", request);
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
                logger.LogInformation("Calling api/rawMaterialsForProduction");
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
                logger.LogInformation($"Creating Finished Product Internal Checkin: Post {httpClient.BaseAddress}/CompleteProduction");
                var response = await httpClient.PostAsJsonAsync("CompleteProduction", request);
                return response.Content.ReadFromJsonAsync<Guid>().Result;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating Finished Product Internal Checkin");
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
                        x.CategoryId, x.CategoryName, x.ProductId, x.ProductNumber, x.ProductName, x.Weight, 
                        x.MeasurementUnitName, x.Location, x.LocationId
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
                    var qtyFlag = response.FirstOrDefault(
                           x => x.ProductId == item.ProductId && x.Weight == item.Weight && (x.Flag == 7 || x.Flag == 8));
                    if (qtyFlag != null) newItem.QtyAvailable = qtyFlag.Quantity;
                    qtyFlag = response.FirstOrDefault(
                            x => x.ProductId == item.ProductId && x.Weight == item.Weight && x.Flag >= 9);
                    if (qtyFlag != null) newItem.QtyReserved = qtyFlag.Quantity;
                    qtyFlag = response.FirstOrDefault(
                            x => x.ProductId == item.ProductId && x.Weight == item.Weight && x.Flag == 6);
                    if (qtyFlag != null) newItem.QtyInProduction = qtyFlag.Quantity;
                    qtyFlag = response.FirstOrDefault(
                            x => x.ProductId == item.ProductId && x.Weight == item.Weight && x.Flag == 5);
                    if(qtyFlag!=null) newItem.QtyToBeProduced = qtyFlag.Quantity;

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
              
                logger.LogInformation($"Getting Inventory Ledger: Get {httpClient.BaseAddress}/finishedProcutInventoryLedger");
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
                logger.LogInformation($"Getting Finished Product Inventory Items : Get {httpClient.BaseAddress}/RawMaterialInventories");
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
                        Weight = x.Sum(y=>y.Weight),
                        Quantity = x.Sum(y=>y.Quantity),
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
          
              
                inventoryLedger = httpClient.GetFromJsonAsync<IEnumerable<InventoryLedger>>($"rawMaterialInventoryLedger/{id}").Result ?? [] ;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error getting Inventory Ledger for InventoryID: {id}");
            }
            return inventoryLedger;
        }
    }
}
