DROP PROCEDURE IF EXISTS [Sales].[GetDispatchOrderDetailByDate]
GO
/****** Object:  StoredProcedure [Sales].[GetCustomerById]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Sales].[GetCustomerById]
GO
/****** Object:  StoredProcedure [Sales].[CustomerList]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Sales].[CustomerList]
GO
/****** Object:  StoredProcedure [Purchasing].[SupplierList]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Purchasing].[SupplierList]
GO
/****** Object:  StoredProcedure [Purchasing].[RawMaterialsForCheckin]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Purchasing].[RawMaterialsForCheckin]
GO
/****** Object:  StoredProcedure [Purchasing].[GetSupplierById]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Purchasing].[GetSupplierById]
GO
/****** Object:  StoredProcedure [Purchasing].[GetEndProducts]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Purchasing].[GetEndProducts]
GO
/****** Object:  StoredProcedure [Purchasing].[GetCheckinRawMaterials]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Purchasing].[GetCheckinRawMaterials]
GO
/****** Object:  StoredProcedure [Production].[RawMaterialTracking]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[RawMaterialTracking]
GO
/****** Object:  StoredProcedure [Production].[RawMaterialLedgerById]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[RawMaterialLedgerById]
GO
/****** Object:  StoredProcedure [Production].[RawMaterialLedger]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[RawMaterialLedger]
GO
/****** Object:  StoredProcedure [Production].[RawMaterialInventories]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[RawMaterialInventories]
GO
/****** Object:  StoredProcedure [Production].[RackingPalletList]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[RackingPalletList]
GO
/****** Object:  StoredProcedure [Production].[ProductList]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[ProductList]
GO
/****** Object:  StoredProcedure [Production].[ProductCategoryList]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[ProductCategoryList]
GO
/****** Object:  StoredProcedure [Production].[LocationList]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[LocationList]
GO
/****** Object:  StoredProcedure [Production].[GetRunningProductionItems]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[GetRunningProductionItems]
GO
/****** Object:  StoredProcedure [Production].[GetRawMaterialsForProduction]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[GetRawMaterialsForProduction]
GO
/****** Object:  StoredProcedure [Production].[GetRawMaterialsBasicInfo]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[GetRawMaterialsBasicInfo]
GO
/****** Object:  StoredProcedure [Production].[GetRackingPalletById]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[GetRackingPalletById]
GO
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailByStatus]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[GetProductionOrderDetailByStatus]
GO
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailById]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[GetProductionOrderDetailById]
GO
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailByDate]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[GetProductionOrderDetailByDate]
GO
/****** Object:  StoredProcedure [Production].[GetLocationById]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[GetLocationById]
GO
/****** Object:  StoredProcedure [Production].[GetInventoryItemsByLocation]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[GetInventoryItemsByLocation]
GO
/****** Object:  StoredProcedure [Production].[GetDispatchableProducts]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[GetDispatchableProducts]
GO
/****** Object:  StoredProcedure [Production].[GetBomAllocation]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[GetBomAllocation]
GO
/****** Object:  StoredProcedure [Production].[FinishedProductLedger]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[FinishedProductLedger]
GO
/****** Object:  StoredProcedure [Production].[FinishedProductInventories]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[FinishedProductInventories]
GO
/****** Object:  StoredProcedure [Production].[DistributeRawMaterialsCheckout]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[DistributeRawMaterialsCheckout]
GO
/****** Object:  StoredProcedure [Production].[DeleteRawMaterialAllocation]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[DeleteRawMaterialAllocation]
GO
/****** Object:  StoredProcedure [Production].[BomSpecification]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [Production].[BomSpecification]
GO
/****** Object:  StoredProcedure [dbo].[ResetTransactions]    Script Date: 9/10/2024 7:12:37 AM ******/
DROP PROCEDURE IF EXISTS [dbo].[ResetTransactions]
GO
DROP FUNCTION IF EXISTS Production.GetProductNames
GO
Create Function [Production].[GetProductNames]
(
	@ProductId uniqueidentifier
) returns nvarchar(103) 
begin
	declare @ret nvarchar(103) = '';
	declare @name nvarchar(50) = '';
	declare c cursor for 
		SELECT [Name] from Production.ProductNameAndDescriptionCulture 
		WHERE ProductId = @ProductId order by CultureId
	OPEN c
	fetch next from c into @name
	WHILE @@FETCH_STATUS=0
	BEGIN
		if len(@ret)>0 set @ret = @ret + ' / '
		SET @ret = @ret + @name
		fetch next from c into @name
	END
	close c
	deallocate c
	return @ret
end
GO
/****** Object:  StoredProcedure [dbo].[ResetTransactions]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[ResetTransactions]
as
delete from sales.SalesOrder

delete from Production.ProductInternalCheckout
delete from production.ProductInternalCheckIn
delete from Production.ProductionOrder
delete from production.ProductCheckout
delete from Purchasing.ProductCheckIn
delete from Production.StockAdjustMent
delete from Production.ProductInventory
GO
/****** Object:  StoredProcedure [Production].[BomSpecification]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [Production].[BomSpecification] 
(
	@ProductId uniqueidentifier
)
AS
SELECT bom.Id, bom.RawMaterialId, Production.GetProductNames(bom.RawMaterialId) as RawMaterialName, 
	bom.RawMaterialMeasurementUnitId, mu.Name as RawMaterialMeasurementUnitName, bom.RawMaterialQuantity 
FROM Production.BillOfMaterials bom 
INNER JOIN Production.Product p ON bom.RawMaterialId=p.id 
INNER JOIN Production.MeasurementUnit mu ON BOM.RawMaterialMeasurementUnitId = mu.Id 
WHERE bom.ProductId = @ProductId
GO
/****** Object:  StoredProcedure [Production].[DeleteRawMaterialAllocation]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[DeleteRawMaterialAllocation]
(
	@inventoryId uniqueidentifier
)
as
with allocation as 
(
	select 
		a.InventoryId, 
		sum(a.Quantity) as tQty 
	From 
		production.ProductionOrderLineDetailResourceAllocation a 
	inner join production.ProductionOrderLineDetailResource r 
	ON 
		a.ResourceId=r.ResourceId 
	WHERE 
		r.InventoryId = @inventoryId 
	GROUP BY 
		a.InventoryId
)
UPDATE Production.ProductInventory 
SET 
	Quantity=i.Quantity+a.tQty,
	Flag = 14 
FROM 
	production.ProductInventory i 
INNER JOIN
	allocation a 
	ON 
		i.InventoryId=a.InventoryId 

delete from production.ProductionOrderLineDetailResourceAllocation 
From production.ProductionOrderLineDetailResourceAllocation a 
inner join production.ProductionOrderLineDetailResource r 
ON a.ResourceId=r.ResourceId where r.InventoryId = @inventoryId
GO
/****** Object:  StoredProcedure [Production].[DistributeRawMaterialsCheckout]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[DistributeRawMaterialsCheckout]
(
	@CheckOutId uniqueidentifier
)
as

Declare c cursor for 
	SELECT 
		l.InventoryId,
		l.Quantity,
		i.ProductId,
		l.MeasurementUnitId 
	FROM
		Production.ProductCheckoutLine l 
	INNER JOIN
		Production.ProductInventory i
	ON
		l.InventoryId = i.InventoryId 
	WHERE
		l.CheckOutId = @CheckOutId

declare @chkInventoryId uniqueidentifier
declare @chkQuantity decimal
declare @chkRawMaterialId uniqueidentifier
declare @chkMeasurementUnitId uniqueidentifier
declare @chkInitialQty decimal
---------------------------------------------
declare @resourceId uniqueidentifier
declare @QtyRequired decimal 
declare @QtyAllocated decimal

open c
fetch next from c into @chkInventoryId, @chkQuantity, @chkRawMaterialId, @chkMeasurementUnitId 

while @@FETCH_STATUS=0
begin
	declare c1 cursor for 
		SELECT 
			rsc.ResourceId, 
			rsc.Quantity - isnull(
				(select 
					sum(Quantity) 
				 from 
					production.ProductionOrderLineDetailResourceAllocation 
				where 
					ResourceId=rsc.ResourceId),0) 
		FROM
			Production.ProductionOrderLineDetailResource rsc
		INNER JOIN 
			Production.ProductionOrderLineDetail ord 
		ON
			rsc.InventoryId = ord.InventoryId 
		INNER JOIN 
			Production.ProductInventory i 
			ON
				ord.InventoryId = i.InventoryId
		INNER JOIN 
			Production.ProductionOrderLine orl  
		ON 
			ord.LineId = orl.LineId 
		INNER JOIN 
			Production.ProductionOrder o 
		ON 
			orl.ProductionOrderId = o.Id 
		WHERE 
			ord.StartDate is null and rsc.RawMaterialId = @chkRawMaterialId and i.Flag<>4
		ORDER BY 
			o.ScheduledDate ASC

	open c1
	fetch next from c1 into @resourceId, @QtyRequired 
	set @chkInitialQty=@chkQuantity
	while @@FETCH_STATUS=0 and @chkQuantity>0
	begin
		if @QtyRequired>0
		begin
			if @chkQuantity>@QtyRequired
			begin
				set @QtyAllocated=@QtyRequired
				set @chkQuantity=@chkQuantity - @QtyAllocated
			end else
			begin
				set @QtyAllocated=@chkQuantity
				set @chkQuantity=0
			end
			insert into Production.ProductionOrderLineDetailResourceAllocation 
			select 
				@resourceId, 
				@CheckOutId,
				@chkInventoryId,
				@QtyAllocated,
				@chkMeasurementUnitId,
				getdate() 
		end
		fetch next from c1 into @resourceId, @QtyRequired 
	end
	UPDATE Production.ProductInventory set Quantity=@chkQuantity, flag=case 
		when @chkQuantity=@chkInitialQty then 15
		when @chkQuantity>0 and @chkQuantity<@chkInitialQty then 14 else flag end  
	WHERE InventoryId=@chkInventoryId

	close c1
	deallocate c1
		
		
			
	fetch next from c into @chkInventoryId, @chkQuantity, @chkRawMaterialId, @chkMeasurementUnitId 
end
close c
deallocate c
GO
CREATE PROCEDURE [Production].[FinishedProductInventories]
as
SELECT 
	p.CategoryId,
	c.Name as CategoryName,
	p.Id as ProductId,
	p.ProductNumber,
	production.GetProductNames(p.Id) as ProductName,
	isnull(i.Quantity,0)  as Weight,
	mu.Initial as MeasurementUnitName,
	i.LocationId,
	isnull(l.Name,'-') as Location,
	isnull(r.Col+'-'+cast(r.Row as nvarchar(5)),'-') as ColRow,
	isnull(i.Flag,0) as Flag,
	isnull(f.Name,'Unavailable') as Status,
	sum(i.TotalBatches) as Quantity
FROM
	Production.Product p 
LEFT OUTER JOIN 
	Production.ProductCategory c 
	ON
		p.CategoryId = c.Id
LEFT OUTER JOIN 
	Production.ProductInventory i 
	ON
		p.Id = i.ProductId 
LEFT OUTER JOIN 
	Production.MeasurementUnit mu 
	ON
		i.MeasurementUnitId = mu.Id 
LEFT OUTER JOIN 
	Production.Location l 
	ON
		i.LocationId = l.Id 
LEFT OUTER JOIN 
	Production.RackingPallet r 
	ON
		i.RackingPalletId = r.Id
LEFT OUTER JOIN 
	Production.InventoryFlag f 
	ON
		i.Flag = f.Id
WHERE 
	isnull(i.Flag,0) < 12 
	AND
	p.IsFinishedGood = 1
GROUP BY
	p.CategoryId, c.Name, p.Id, p.ProductNumber, production.GetProductNames(p.Id), i.Quantity, mu.Initial, 
	i.LocationId, l.Name, r.Col+'-'+cast(r.Row as nvarchar(5)), i.Flag, f.Name  
GO
/****** Object:  StoredProcedure [Production].[FinishedProductLedger]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[FinishedProductLedger]
(
	@ProductId uniqueidentifier,
	@LocationId uniqueidentifier,
	@weight decimal(18,2)
)
as
declare @stockFlow table
(
	TransDate datetime,
	InventoryId uniqueidentifier,
	BatchNumber nvarchar(15),
	Description nvarchar(50),
	StockIn float,
	StockOut Float,
	Balance float,
	Flag tinyInt
)
declare @balance float = 0
INSERT @stockFlow 
	SELECT 
		c.CheckInDate as TransDate, cl.InventoryId, i.BatchNumber, 
		Description = case 
			WHEN c.CheckInType=1 then 'Production' 
			WHEN c.CheckInType=2 then 'Move From ' + (select Name from Production.Location where id=cl.SourceLocationId) end,
		cl.Quantity, 0, 0,
		case 
			when c.CheckInType=1 then 7 
			when c.CheckInType=2 then 13 end 
	FROM 
		Production.ProductInternalCheckInLine l
	INNER JOIN 
		Production.ProductInternalCheckIn c 
		ON
			l.CheckInId = c.Id 
	INNER JOIN 
		Production.ProductInternalCheckInLinePackaging cl 
		ON
			l.LineId = cl.LineId 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			cl.InventoryId = i.InventoryId 
	WHERE I.ProductId = @ProductId AND CL.LocationId=@LocationId and cl.Weight=@weight 
UNION ALL
SELECT 
		c.CheckInDate as TransDate, cl.InventoryId, i.BatchNumber, 
		Description = case 
			WHEN c.CheckInType=1 then 'Production' 
			else 'Move From ' + (select Name from Production.Location where id=cl.SourceLocationId) end,
		cl.Quantity, 0, 0,
		case 
			when c.CheckInType=1 then 7 
			when c.CheckInType=2 then 13 end 
	FROM 
		Production.ProductInternalCheckInLine cl
	INNER JOIN 
		Production.ProductInternalCheckIn c 
		ON
			cl.CheckInId = c.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			cl.InventoryId = i.InventoryId 
	WHERE I.ProductId = @ProductId AND CL.LocationId=@LocationId and cl.Weight=@weight and isnull(c.CheckInType,2)<>1 
UNION ALL
SELECT 
		c.CheckInDate as TransDate, cl.InventoryId, i.BatchNumber, 
		Description = case 
			WHEN c.CheckInType=1 then 'Production' 
			else 'Move to ' + (select Name from Production.Location where id=cl.LocationId) end,
		0, cl.Quantity, 0,
		case 
			when c.CheckInType=1 then 7 
			when c.CheckInType=2 then 13 end 
	FROM 
		Production.ProductInternalCheckInLine cl
	INNER JOIN 
		Production.ProductInternalCheckIn c 
		ON
			cl.CheckInId = c.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			cl.InventoryId = i.InventoryId 
	WHERE I.ProductId = @ProductId AND CL.SourceLocationId=@LocationId and cl.Weight=@weight and isnull(c.CheckInType,2)<>1 

UNION ALL
	SELECT 
		SO.OrderDate, sl.InventoryId, I.BatchNumber, f.Name, 0, sl.Quantity , 0, i.Flag 
	FROM 
		Sales.SalesOrderLine sl 
	INNER JOIN 
		Sales.SalesOrder so 
		ON
			sl.OrderId = so.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			sl.InventoryId = i.InventoryId 
	INNER JOIN 
		Production.InventoryFlag F 
		ON
			i.Flag = f.Id 
	WHERE I.ProductId = @ProductId AND i.LocationId = @LocationId AND I.Flag BETWEEN 10 AND 12 AND i.Quantity=@weight 
ORDER BY TransDate

UPDATE @stockFlow set @balance = balance=@balance+StockIn-StockOut

SELECT * FROM @stockFlow order by TransDate
GO
/****** Object:  StoredProcedure [Production].[GetBomAllocation]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [Production].[GetBomAllocation]
(
	@InventoryId uniqueidentifier
)
AS
SELECT  
	rss.RawMaterialId,
	production.GetProductNames(rss.RawMaterialId) as RawMaterialNames,
	rss.Quantity as WeightRequired,
	rss.MeasurementUnitId as BomMeasurementUnitId,
	muRss.Initial as BomMeasurementUnitInitial,
	muRss.Name as BomMeasurementUnitName,
	rsAl.InventoryId,
	inv.BatchNumber,
	rsal.Quantity as WeightAllocated,
	rsal.MeasurementUnitId as WeightAllocatedMeasurementUnitId,
	mu.Initial as WeightAllocatedMeasurementUnitInitial,
	mu.Name as WeightAllocatedMeasurementUnitName,
	inv.ProductionDate,
	inv.ExpirationDate,
	ck.CheckOutDate

FROM 
	Production.ProductionOrderLineDetailResource rss 
LEFT OUTER JOIN 
	Production.ProductionOrderLineDetailResourceAllocation rsAl 	
	ON
		rsAl.ResourceId = rss.ResourceId 
LEFT OUTER JOIN
	Production.ProductInventory inv 
	ON 
		rsAl.InventoryId = inv.InventoryId 
LEFT OUTER JOIN 
	Production.MeasurementUnit mu 
	ON
		rsal.MeasurementUnitId = mu.id 
LEFT OUTER JOIN 
	Production.MeasurementUnit muRss 
	ON
		rss.MeasurementUnitId = muRss.Id 
LEFT OUTER JOIN 
	Production.ProductCheckout ck 
	ON 
		rsal.ProductCheckoutId = ck.Id 
WHERE
	rss.InventoryId= @InventoryId 
GO
/****** Object:  StoredProcedure [Production].[GetDispatchableProducts]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[GetDispatchableProducts]
as
SELECT 
	inv.ProductId,
	inv.InventoryId,
	inv.BatchNumber,
	production.GetProductNames(inv.ProductId) as Names,
	inv.Quantity,
	inv.MeasurementUnitId,
	mu.Name as MeasurementUnitName,
	inv.LocationId,
	loc.Name as LocationName,
	inv.RackingPalletId, 
	rack.Col+'-'+cast(rack.row as nvarchar(5)) as RackingPalletColRow,
	p.DaysToExpire,
	p.DaysToManufacture 
FROM 
	Production.ProductInventory inv 
INNER JOIN 
	Production.Product p 
	ON
		inv.ProductId = p.Id 
INNER JOIN 
	Production.MeasurementUnit mu 
	ON
		inv.MeasurementUnitId = mu.Id 
INNER JOIN 
	Production.Location loc 
	ON
		inv.LocationId = loc.Id 
INNER JOIN 
	Production.RackingPallet rack 
	ON
		inv.RackingPalletId = rack.Id 
WHERE 
	inv.Flag=7 AND p.IsFinishedGood = 1
GO
/****** Object:  StoredProcedure [Production].[GetInventoryItemsByLocation]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[GetInventoryItemsByLocation]

as
SELECT 
	inv.ProductId,
	inv.InventoryId,
	inv.BatchNumber,
	production.GetProductNames(inv.ProductId) as Names,
	case when p.IsFinishedGood=1 then inv.Quantity else inv.Quantity/1000 end as Quantity,
	inv.MeasurementUnitId,
	mu.Name as MeasurementUnitName,
	p.LocationId,
	loc.Name as LocationName,
	p.RackingPalletId, 
	rack.Col+'-'+cast(rack.row as nvarchar(5)) as RackingPalletColRow,
	p.DaysToExpire,
	p.DaysToManufacture,
	inv.ExpirationDate as ExpiryDate,
	inv.ProductionDate,
	isnull(p.OutLocationId, 
		isnull((select top 1 Id from production.location where name like '%product%'),
			isnull((select top 1 Id from production.Location where Id<>p.LocationId),p.locationId))) 
		as OutLocationId,
	loc2.Name as OutLocationName,
	isnull(p.OutRackingPalletId, p.rackingPalletId) as OutRackingPalletId,
	rack2.Col+'-'+cast(rack2.row as nvarchar(5)) as OutRackingPalletColRow,
	inv.LocationId as CurrentLocationId,
	loc3.Name as CurrentLocationName,
	inv.RackingPalletId as CurrentRackingPalletId,
	rack3.Col+'-'+cast(rack3.row as nvarchar(5)) as CurrentRackingPalletColRow,
	p.IsFinishedGood 
FROM 
	Production.ProductInventory inv 
INNER JOIN 
	Production.Product p 
	ON
		inv.ProductId = p.Id 
INNER JOIN 
	Production.MeasurementUnit mu 
	ON
		inv.MeasurementUnitId = mu.Id 
INNER JOIN 
	Production.Location loc 
	ON
		p.LocationId = loc.Id 
INNER JOIN 
	Production.RackingPallet rack 
	ON
		p.RackingPalletId = rack.Id
INNER JOIN 
	Production.Location loc2 
	ON
		isnull(p.OutLocationId, 
		isnull((select top 1 Id from production.location where name like '%product%'),
			isnull((select top 1 Id from production.Location where Id<>p.LocationId),p.locationId)))  = loc2.Id 
INNER JOIN 
	Production.RackingPallet rack2 
	ON
		isnull(p.OutRackingPalletId, p.RackingPalletId) = rack2.Id
INNER JOIN 
	Production.Location loc3 
	ON
		inv.LocationId = loc3.Id 
INNER JOIN 
	Production.RackingPallet rack3 
	ON
		inv.RackingPalletId = rack3.Id
WHERE 
	
	inv.Flag in (1,3,7,8,9,10,11,13,14,15) 
	AND 
	p.IsFinishedGood = 0
GO
/****** Object:  StoredProcedure [Production].[GetLocationById]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [Production].[GetLocationById]
(
	@Id uniqueidentifier
)
as
select Id, Name from Production.Location where Id = @Id
GO
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailByDate]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[GetProductionOrderDetailByDate]
(
	@date datetime
)
as
SELECT
	po.Id,	
	po.OrderDate,
	isnull(i.ProductionDate,po.ScheduledDate) as ScheduledDate,
	pl.LineId,
	pl.ProductId,
	production.GetProductNames(pl.ProductId) as ProductName,
	pd.BatchNumber,
	pl.MeasurementUnitId as ProductMeasurementUnitId,
	(select Name from production.MeasurementUnit where Id=pl.MeasurementUnitId) as ProductMeasurementUnitName,
	pl.QuantityPerBatch,
	pl.TotalBatches,
	pd.InventoryId,
	pr.RawMaterialId,
	production.GetProductNames(pr.RawMaterialId) as RawMaterialName, 
	pr.MeasurementUnitId as ProductMeasurementUnitId,
	(select Name from production.MeasurementUnit where Id=pr.MeasurementUnitId) as RawMaterialMeasurementUnitName,
	pr.Quantity as RawMaterialQuantity,
	i.Flag,
	isnull((select sum(Quantity) from Production.ProductionOrderLineDetailResourceAllocation 
		where ResourceId=pr.resourceId),0)/pr.Quantity * 100 as ResourceAllocated,
	isnull(i.ExpirationDate,dateadd(day, (select DaysToExpire from production.Product prod
		where Id = pl.ProductId), isnull(i.ProductionDate,po.OrderDate))) as ExpirationDate 
FROM
	Production.ProductionOrder po
	INNER JOIN 
	production.ProductionOrderLine pl 
		ON po.Id = pl.ProductionOrderId 
	INNER JOIN 
	production.ProductionOrderLineDetail pd
		ON pl.LineId = pd.LineId 
	INNER JOIN 
	Production.ProductionOrderLineDetailResource pr 
		ON pd.InventoryId = pr.InventoryId 
	INNER JOIN
	Production.ProductInventory i 
		ON pd.InventoryId = i.InventoryId 
WHERE  
	DATEDIFF(day,po.scheduledDate,@date)=0

GO
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailById]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[GetProductionOrderDetailById]
(
	@Id uniqueidentifier
)
as
SELECT
	po.Id,	
	po.OrderDate,
	isnull(i.ProductionDate,po.ScheduledDate) as ScheduledDate,
	pl.LineId,
	pl.ProductId,
	production.GetProductNames(pl.ProductId) as ProductName,
	pd.BatchNumber,
	pl.MeasurementUnitId as ProductMeasurementUnitId,
	(select Name from production.MeasurementUnit where Id=pl.MeasurementUnitId) as ProductMeasurementUnitName,
	pl.QuantityPerBatch,
	pl.TotalBatches,
	pd.InventoryId,
	pr.RawMaterialId,
	production.GetProductNames(pr.RawMaterialId) as RawMaterialName, 
	pr.MeasurementUnitId as ProductMeasurementUnitId,
	(select Name from production.MeasurementUnit where Id=pr.MeasurementUnitId) as RawMaterialMeasurementUnitName,
	pr.Quantity as RawMaterialQuantity, 
	i.Flag,
	isnull((select sum(Quantity) from Production.ProductionOrderLineDetailResourceAllocation 
		where ResourceId=pr.resourceId),0)/pr.Quantity * 100 as ResourceAllocated,
	isnull(i.ExpirationDate,dateadd(day, (select DaysToExpire from production.Product prod
		where Id = pl.ProductId), isnull(i.ProductionDate,po.OrderDate))) as ExpirationDate  
FROM
	Production.ProductionOrder po 
INNER JOIN 
	production.ProductionOrderLine pl 
ON 
	po.Id = pl.ProductionOrderId 
INNER JOIN 
	production.ProductionOrderLineDetail pd
ON 
	pl.LineId = pd.LineId 
INNER JOIN 
	Production.ProductionOrderLineDetailResource pr 
ON 
	pd.InventoryId = pr.InventoryId 
INNER JOIN 
	Production.ProductInventory i 
ON 
	pd.InventoryId = i.InventoryId 
WHERE  
	PO.Id = @Id

GO
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailByStatus]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Proc [Production].[GetProductionOrderDetailByStatus]
(
	@flag tinyint
)
as
SELECT
	po.Id,	
	po.OrderDate,
	isnull(i.ProductionDate,po.ScheduledDate) as ScheduledDate,
	pl.LineId,
	pl.ProductId,
	production.GetProductNames(pl.ProductId) as ProductName,
	pd.BatchNumber,
	pl.MeasurementUnitId as ProductMeasurementUnitId,
	(select Name from production.MeasurementUnit where Id=pl.MeasurementUnitId) as ProductMeasurementUnitName,
	pl.QuantityPerBatch,
	pl.TotalBatches,
	pd.InventoryId,
	pr.RawMaterialId,
	production.GetProductNames(pr.RawMaterialId) as RawMaterialName, 
	pr.MeasurementUnitId as ProductMeasurementUnitId,
	(select Name from production.MeasurementUnit where Id=pr.MeasurementUnitId) as RawMaterialMeasurementUnitName,
	pr.Quantity as RawMaterialQuantity,
	i.Flag,
	isnull((select sum(Quantity) from Production.ProductionOrderLineDetailResourceAllocation 
		where ResourceId=pr.resourceId),0)/pr.Quantity * 100 as ResourceAllocated,
	isnull(i.ExpirationDate,dateadd(day, (select DaysToExpire from production.Product prod
		where Id = pl.ProductId), isnull(i.ProductionDate,po.OrderDate))) as ExpirationDate 
FROM
	Production.ProductionOrder po
	INNER JOIN 
	production.ProductionOrderLine pl 
		ON po.Id = pl.ProductionOrderId 
	INNER JOIN 
	production.ProductionOrderLineDetail pd
		ON pl.LineId = pd.LineId 
	INNER JOIN 
	Production.ProductionOrderLineDetailResource pr 
		ON pd.InventoryId = pr.InventoryId 
	INNER JOIN
	Production.ProductInventory i 
		ON pd.InventoryId = i.InventoryId 
WHERE  
	i.flag = @flag

GO
/****** Object:  StoredProcedure [Production].[GetRackingPalletById]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [Production].[GetRackingPalletById]
(
	@Id uniqueidentifier
)
as
SELECT Id, Col, Row, Description, Col + '-' + cast(Row as nvarchar(50)) as ColRow 
FROM Production.RackingPallet 
WHERE Id=@Id
GO
/****** Object:  StoredProcedure [Production].[GetRawMaterialsBasicInfo]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[GetRawMaterialsBasicInfo]
as 
SELECT 
	p.Id, 
	production.GetProductNames(p.Id) as Names,
	p.MeasurementUnitOrderId as MeasurementUnitId,
	mu.[Name] as MeasurementUnitName 
FROM 
	Production.Product p 
	INNER JOIN production.MeasurementUnit mu 
		ON p.MeasurementUnitOrderId = mu.Id 
WHERE IsFinishedGood=0
GO
/****** Object:  StoredProcedure [Production].[GetRawMaterialsForProduction]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[GetRawMaterialsForProduction]
as
SELECT 
	inv.ProductId,
	inv.InventoryId,
	inv.BatchNumber,
	production.GetProductNames(inv.ProductId) as Names,
	inv.Quantity,
	inv.MeasurementUnitId,
	mu.Name as MeasurementUnitName,
	isnull(p.OutLocationId, 
		isnull((select top 1 Id from production.location where name like '%product%'),
			isnull((select top 1 Id from production.Location where Id<>p.LocationId),p.locationId))) as LocationId,
	loc.Name as LocationName,
	isnull(p.OutRackingPalletId, p.RackingPalletId) as RackingPalletId, 
	rack.Col+'-'+cast(rack.row as nvarchar(5)) as RackingPalletColRow,
	p.DaysToExpire,
	p.DaysToManufacture,
	inv.ProductionDate,
	inv.ExpirationDate as ExpiryDate
FROM 
	Production.ProductInventory inv 
INNER JOIN 
	Production.Product p 
	ON
		inv.ProductId = p.Id 
INNER JOIN 
	Production.MeasurementUnit mu 
	ON
		inv.MeasurementUnitId = mu.Id 
INNER JOIN 
	Production.Location loc 
	ON
		isnull(p.OutLocationId, 
		isnull((select top 1 Id from production.location where name like '%product%'),
			isnull((select top 1 Id from production.Location where Id<>p.LocationId),p.locationId))) = loc.Id 
INNER JOIN 
	Production.RackingPallet rack 
	ON
		isnull(p.OutRackingPalletId, p.RackingPalletId) = rack.Id 
WHERE 
	p.IsFinishedGood = 0 AND inv.Quantity>0 
ORDER BY
	inv.ExpirationDate desc
GO
/****** Object:  StoredProcedure [Production].[GetRunningProductionItems]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[GetRunningProductionItems]
as
SELECT 
	inv.ProductId,
	inv.InventoryId,
	inv.BatchNumber,
	production.GetProductNames(inv.ProductId) as Names,
	inv.Quantity,
	inv.MeasurementUnitId,
	mu.Name as MeasurementUnitName,
	p.LocationId,
	loc.Name as LocationName,
	p.RackingPalletId, 
	rack.Col+'-'+cast(rack.row as nvarchar(5)) as RackingPalletColRow,
	p.DaysToExpire,
	p.DaysToManufacture,
	inv.ProductionDate
FROM 
	Production.ProductInventory inv 
INNER JOIN 
	Production.Product p 
	ON
		inv.ProductId = p.Id 
INNER JOIN 
	Production.MeasurementUnit mu 
	ON
		inv.MeasurementUnitId = mu.Id 
left outer JOIN 
	Production.Location loc 
	ON
		p.LocationId = loc.Id 
left outer JOIN 
	Production.RackingPallet rack 
	ON
		p.RackingPalletId = rack.Id 
WHERE 
	inv.Flag=6 AND p.IsFinishedGood = 1
GO
/****** Object:  StoredProcedure [Production].[LocationList]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Production].[LocationList]
as
Select Id, [Name] from production.[Location]
GO
/****** Object:  StoredProcedure [Production].[ProductCategoryList]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [Production].[ProductCategoryList]
as
select Id, Name from Production.ProductCategory
GO
/****** Object:  StoredProcedure [Production].[ProductList]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [Production].[ProductList]
as
SELECT p.CategoryId, c.Name as CategoryName, p.Id, Production.GetProductNames(p.Id) as Names,
	p.ProductNumber, p.IsFinishedGood, p.IsSalable, p.IsUniqueBatchPerOrder, p.SafetyStockLevel,
	p.ReorderPoint, p.StandardCost, p.ListPrice, p.MeasurementUnitGroupId, mg.Name as MeasurementGroupName, 
	p.MeasurementUnitOrderId, mu.Name as MeasurementUnitOrderName, p.OrderQuantity, p.DaysToManufacture, 
	p.DaysToExpire, p.LocationId, l.Name as LocationName, p.RackingPalletId, 
	case when len(isnull(r.[Description],''))>0 
		then r.[Description] 
		else r.Col + '-' + cast(r.Row as nvarchar(5)) 
	end as RackingPalleteName, r.Col as RackingPalletCol, r.Row as RackingPalletRow, p.AdditionalInfo,
	case when p.IsFinishedGood=1 then
		(select count(*) from Production.BillOfMaterials where ProductId = p.Id) else 0 end as BomCount 
FROM Production.Product p 
INNER JOIN Production.ProductCategory c 
	ON p.CategoryId = c.Id 
left outer JOIN Production.Location l
	ON p.LocationId = l.Id
left outer JOIN Production.RackingPallet r 
	ON p.RackingPalletId = r.Id 
INNER JOIN Production.MeasurementUnitGroup mg 
	ON p.MeasurementUnitGroupId = mg.Id 
INNER JOIN Production.MeasurementUnit mu 
	ON p.MeasurementUnitOrderId = mu.Id
GO
/****** Object:  StoredProcedure [Production].[RackingPalletList]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Production].[RackingPalletList]
as
Select Id, Col, Row, [Description], Col+'-'+cast(Row as nvarchar(5)) as ColRow from production.RackingPallet
GO
/****** Object:  StoredProcedure [Production].[RawMaterialInventories]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Production].[RawMaterialInventories]
as
SELECT 
	p.CategoryId,
	c.Name as CategoryName,
	p.Id as ProductId,
	p.ProductNumber,
	production.GetProductNames(p.Id) as ProductName,
	sum(isnull(i.Quantity,0)) as Weight,
	mu.Name as MeasurementUnitName,
	i.LocationId,
	isnull(l.Name,'-') as Location,
	isnull(r.Col+'-'+cast(r.Row as nvarchar(5)),'-') as ColRow,
	isnull(i.Flag,0) as Flag,
	isnull(f.Name,'Unavailable') as Status,
	count(i.InventoryId) as Quantity
FROM
	Production.Product p 
LEFT OUTER JOIN 
	Production.ProductCategory c 
	ON
		p.CategoryId = c.Id
LEFT OUTER JOIN 
	Production.MeasurementUnit mu 
	ON
		p.MeasurementUnitOrderId = mu.Id 
LEFT OUTER JOIN 
	Production.ProductInventory i 
	ON
		p.Id = i.ProductId 
LEFT OUTER JOIN 
	Production.Location l 
	ON
		i.LocationId = l.Id 
LEFT OUTER JOIN 
	Production.RackingPallet r 
	ON
		i.RackingPalletId = r.Id
LEFT OUTER JOIN 
	Production.InventoryFlag f 
	ON
		i.Flag = f.Id
WHERE 
	--isnull(i.Flag,0) < 12 or isnull(i.flag
	--AND
	p.IsFinishedGood = 0
GROUP BY
	p.CategoryId, c.Name, p.Id, p.ProductNumber, production.GetProductNames(p.Id), i.Quantity, mu.Name, 
	i.LocationId, l.Name, r.Col+'-'+cast(r.Row as nvarchar(5)), i.Flag, f.Name  
GO
/****** Object:  StoredProcedure [Production].[RawMaterialLedger]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select id, production.GetProductNames(id) from production.Product
--select * from production.Location

--select month(getdate())
CREATE Proc [Production].[RawMaterialLedger]
(
	@ProductId uniqueidentifier,
	@LocationId uniqueidentifier
)
as
declare @stockFlow table
(
	TransDate datetime,
	InventoryId uniqueidentifier,
	BatchNumber nvarchar(15),
	Description nvarchar(250),
	StockIn float,
	StockOut Float,
	Balance float,
	Flag tinyInt
)
declare @balance float = 0
INSERT @stockFlow 
	SELECT 
		c.CheckInDate as TransDate, cl.InventoryId, i.BatchNumber, 
		'Move/Return From <strong>' + 
		(select col+'-'+cast(row as nvarchar(5)) from production.RackingPallet where id = cl.SourceRackingPalletId)+'@'+
		(select Name from Production.Location where id=cl.SourceLocationId)+'</strong>',
		cl.Quantity, 0, 0, 13 as flag
	FROM 
		Production.ProductInternalCheckInLine cl
	INNER JOIN 
		Production.ProductInternalCheckIn c 
		ON
			cl.CheckInId = c.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			cl.InventoryId = i.InventoryId 
	WHERE I.ProductId = @ProductId AND CL.LocationId=@LocationId 
UNION ALL 
	SELECT 
		c.CheckOutDate as TransDate, cl.InventoryId, i.BatchNumber, 
		Description =  'Move/Return to <strong>' + 
		(select col+'-'+cast(row as nvarchar(5)) from production.RackingPallet where id = cl.RackingPalletId)+'@'+
		(select Name from Production.Location where id=cl.LocationId) + '</strong>',
		0, cl.Quantity, 0,
		13 
	FROM 
		Production.ProductInternalCheckOutLine cl
	INNER JOIN 
		Production.ProductInternalCheckOut c 
		ON
			cl.CheckOutId = c.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			cl.InventoryId = i.InventoryId 
	WHERE I.ProductId = @ProductId AND CL.SourceLocationId=@LocationId 
UNION ALL
	SELECT 
		SO.CheckOutDate, sl.InventoryId, I.BatchNumber, 'Checkout to <strong>'+
		r.Col+'-'+cast(r.row as nvarchar(5))+'@'+
		l.Name+'</strong>', 0, sl.Quantity, 0, 2  
	FROM 
		Production.ProductCheckOutLine sl 
	INNER JOIN 
		Production.ProductCheckout so 
		ON
			sl.CheckOutId = so.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			sl.InventoryId = i.InventoryId 
	INNER JOIN 
		Production.Location l 
		ON 
			sl.LocationId = l.id 
	INNER JOIN 
		Production.RackingPallet r 
		ON
			sl.RackingPalletId = r.Id 
	WHERE I.ProductId = @ProductId AND sl.SourceLocationId = @LocationId 
UNION ALL
	SELECT 
		SO.CheckOutDate, sl.InventoryId, i.BatchNumber, 'Checked-out from <strong> ' + 
		isnull((select col+'-'+cast(row as nvarchar(5)) from production.RackingPallet where id = sl.SourceRackingPalletId),'')+'@'+
		isnull((select Name from Production.Location where id=sl.SourceLocationId),'')+'</strong>', sl.Quantity, 0, 0, 2  
	FROM 
		Production.ProductCheckOutLine sl 
	INNER JOIN 
		Production.ProductCheckout so 
		ON
			sl.CheckOutId = so.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			sl.InventoryId = i.InventoryId 
	WHERE I.ProductId = @ProductId AND sl.LocationId = @LocationId 
UNION ALL
	SELECT 
		SO.AdjustmentDate, sl.InventoryId, i.BatchNumber, 'Stock adjustment'+
		case when so.FromInventoryTransfer=1 then ' on inventory transfer' else '' end +', reason: <strong> ' + d.Reason + '</strong>', 
		case when sl.Adjustment>0 then sl.Adjustment else 0 end, 
		case when sl.Adjustment<0 then sl.Adjustment*-1 else 0 end, 0, 2  
	FROM 
		Production.StockAdjustmentLine sl 
	INNER JOIN 
		Production.StockAdjustMent so 
		ON
			sl.AdjustmentId = so.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			sl.InventoryId = i.InventoryId 
	INNER JOIN 
		Production.DiscrepantReason d 
		ON
			sl.ReasonId = d.Id 
	WHERE I.ProductId = @ProductId AND sl.LocationId = @LocationId 
UNION ALL
	SELECT 
		c.CheckInDate, cd.InventoryId, cd.BatchNumber, 'Check-in from <strong>'+ s.Name + '</strong> to '+
		(select col+'-'+cast(row as nvarchar(5)) from production.RackingPallet where id = cl.RackingPalletId)+'@'+
		(select Name from Production.Location where id=cl.LocationId) + '</strong>',
		cl.QuantityPerBatch, 0, 0, 1 
	FROM 
		Purchasing.ProductCheckInLineDetail cd 
	INNER JOIN 
		Purchasing.ProductCheckInLine cl 
		ON
			cd.LineId = cl.LineId 
	INNER JOIN 
		Purchasing.ProductCheckIn c 
		ON
			cl.CheckInId = c.Id 
	INNER JOIN 
		Purchasing.Supplier s 
		ON
			c.SupplierId = s.BusinessEntityId 
	WHERE cl.LocationId = @LocationId and cl.ProductId = @ProductId 
UNION ALL
	SELECT
	--+production.GetProductNames(l.ProductId) + '#' + d.BatchNumber
		C.CheckOutDate, a.InventoryId, i.BatchNumber, 'Allocated for <strong>'+ 
		(select production.GetProductNames(rsl.ProductId) + '</strong> production, #<strong>'+rsd.BatchNumber+'</strong> - <i>'+
		 cast(month(isnull(rsd.StartDate,po.ScheduledDate)) as nvarchar(2)) + '/' +
		 cast(day(isnull(rsd.StartDate,po.ScheduledDate)) as nvarchar(2)) + '/' +
		 cast(year(isnull(rsd.StartDate,po.ScheduledDate)) as nvarchar(4)) + '</i>'
		 from production.ProductionOrderLineDetailResource rss 
		 Inner join production.ProductionOrderLineDetail rsd 
		 ON rss.InventoryId=rsd.InventoryId 
		 inner join production.ProductionOrderLine rsl 
		 ON rsd.LineId = rsl.LineId 
		 inner join Production.ProductionOrder po 
		 ON rsl.ProductionOrderId = po.Id 
		 WHERE rss.ResourceId=a.ResourceId), 0, a.Quantity, 0, 6 
	FROM 
		Production.ProductionOrderLineDetailResourceAllocation a 
	INNER JOIN 
		Production.ProductCheckOutLine cl 
		ON
			a.InventoryId = cl.InventoryId AND A.ProductCheckoutId = CL.CheckOutId 
	INNER JOIN 
		Production.ProductCheckout c 
		ON
			CL.CheckOutId = C.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON 
			a.InventoryId = i.InventoryId 
	--INNER JOIN 
	--	production.ProductionOrderLineDetailResource r 
	--	ON
	--		a.ResourceId = r.ResourceId 
	--INNER JOIN 
	--	production.ProductionOrderLineDetail d 
	--	ON
	--		a.InventoryId = d.InventoryId 
	--INNER JOIN 
	--	Production.ProductionOrderLine l 
	--	ON
	--		d.LineId = l.LineId 
	WHERE i.ProductId = @ProductId and cl.LocationId = @LocationId 

	ORDER BY TransDate, Flag 

UPDATE @stockFlow set @balance = balance=@balance+StockIn-StockOut

SELECT * FROM @stockFlow

GO
/****** Object:  StoredProcedure [Production].[RawMaterialLedgerById]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Proc [Production].[RawMaterialLedgerById]
(
	@InventoryId uniqueidentifier 
)
as
declare @stockFlow table
(
	TransDate datetime,
	InventoryId uniqueidentifier,
	BatchNumber nvarchar(15),
	Description nvarchar(250),
	StockIn float,
	StockOut Float,
	Balance float,
	Flag tinyInt
)
declare @balance float = 0
INSERT @stockFlow 
--	SELECT 
--		c.CheckInDate as TransDate, cl.InventoryId, i.BatchNumber, 
--		'Move/Return From ' + (select Name from Production.Location where id=cl.SourceLocationId),
--		0, 0, 0, 13 
--	FROM 
--		Production.ProductInternalCheckInLine cl
--	INNER JOIN 
--		Production.ProductInternalCheckIn c 
--		ON
--			cl.CheckInId = c.Id 
--	INNER JOIN 
--		Production.ProductInventory i 
--		ON
--			cl.InventoryId = i.InventoryId 
--	WHERE i.InventoryId=@InventoryId 
--UNION ALL 
	SELECT 
		c.CheckOutDate as TransDate, cl.InventoryId, i.BatchNumber, 
		Description =  
		'Move/Return from <strong>'+
		(select col+':'+cast(row as nvarchar(5)) from production.rackingPallet where id=cl.SourceRackingPalletId)+'@'+
		(select Name from Production.Location where id=cl.SourceLocationId) + 
		'</strong> to <strong>' + 
		(select col + ':' + cast(row as nvarchar(5)) from production.RackingPallet where id=cl.RackingPalletId)+'@'+
		(select Name from Production.Location where id=cl.LocationId) + 
		'</strong> (no weight change)',
		cl.Quantity, cl.Quantity, 0,
		13 as flag
	FROM 
		Production.ProductInternalCheckOutLine cl
	INNER JOIN 
		Production.ProductInternalCheckOut c 
		ON
			cl.CheckOutId = c.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			cl.InventoryId = i.InventoryId 
	WHERE i.InventoryId=@InventoryId 
UNION ALL
	SELECT 
		SO.CheckOutDate as TransDate, sl.InventoryId, I.BatchNumber, 
		'Checkout from <strong>'+
			(select col+':'+cast(row as nvarchar(5)) from production.rackingPallet where id=sl.SourceRackingPalletId) + '@' +
			(select name from production.Location where id=sl.SourceLocationId) + 
			'</strong>  to <strong>'+
			(select col+':'+cast(row as nvarchar(5)) from production.rackingPallet where id=sl.RackingPalletId) + '@' +
			(select name from production.Location where id=sl.LocationId)+
			'</strong> (no weight change)', 
		sl.Quantity, sl.Quantity, 0, 2  
	FROM 
		Production.ProductCheckOutLine sl 
	INNER JOIN 
		Production.ProductCheckout so 
		ON
			sl.CheckOutId = so.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			sl.InventoryId = i.InventoryId 
	WHERE i.InventoryId=@InventoryId 
UNION ALL
	SELECT 
		c.CheckInDate as TransDate, cd.InventoryId, cd.BatchNumber, 'Check-in from <strong>'+ s.Name+'</strong> to <strong>'+
		(select col+':'+cast(row as nvarchar(5)) from production.RackingPallet where id=cl.RackingPalletId)+'@'+
		(select name from production.Location where id=cl.LocationId)+
		'</strong>', 
		cl.QuantityPerBatch, 0, 0, 1 
	FROM 
		Purchasing.ProductCheckInLineDetail cd 
	INNER JOIN 
		Purchasing.ProductCheckInLine cl 
		ON
			cd.LineId = cl.LineId 
	INNER JOIN 
		Purchasing.ProductCheckIn c 
		ON
			cl.CheckInId = c.Id 
	INNER JOIN 
		Purchasing.Supplier s 
		ON
			c.SupplierId = s.BusinessEntityId 
	WHERE cd.InventoryId=@InventoryId 
UNION ALL
	SELECT 
		SO.AdjustmentDate, sl.InventoryId, i.BatchNumber, 'Stock adjustment'+
		case when so.FromInventoryTransfer=1 then ' on inventory transfer' else '' end +', reason: <strong> ' + d.Reason + '</strong>', 
		case when sl.Adjustment>0 then sl.Adjustment else 0 end, 
		case when sl.Adjustment<0 then sl.Adjustment*-1 else 0 end, 0, 2  
	FROM 
		Production.StockAdjustmentLine sl 
	INNER JOIN 
		Production.StockAdjustMent so 
		ON
			sl.AdjustmentId = so.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON
			sl.InventoryId = i.InventoryId 
	INNER JOIN 
		Production.DiscrepantReason d 
		ON
			sl.ReasonId = d.Id 
	WHERE I.InventoryId = @InventoryId 
UNION ALL
	SELECT
	--+production.GetProductNames(l.ProductId) + '#' + d.BatchNumber
		C.CheckOutDate, a.InventoryId, i.BatchNumber, 'Allocated for <strong>'+ 
		isnull((select production.GetProductNames(rsl.ProductId) + '</strong> production, #<strong>'+rsd.BatchNumber+'</strong> - <i>'+
		 cast(month(isnull(rsd.StartDate,po.ScheduledDate)) as nvarchar(2)) + '/' +
		 cast(day(isnull(rsd.StartDate,po.ScheduledDate)) as nvarchar(2)) + '/' +
		 cast(year(isnull(rsd.StartDate,po.ScheduledDate)) as nvarchar(4)) + '</i>'
		 from production.ProductionOrderLineDetailResource rss 
		 Inner join production.ProductionOrderLineDetail rsd 
		 ON rss.InventoryId=rsd.InventoryId 
		 inner join production.ProductionOrderLine rsl 
		 ON rsd.LineId = rsl.LineId 
		 inner join Production.ProductionOrder po 
		 ON rsl.ProductionOrderId = po.Id 
		 WHERE rss.ResourceId=a.ResourceId),' Production</strong>'), 0, a.Quantity, 0, 6 
	FROM 
		Production.ProductionOrderLineDetailResourceAllocation a 
	INNER JOIN 
		Production.ProductCheckOutLine cl 
		ON
			a.InventoryId = cl.InventoryId AND A.ProductCheckoutId = CL.CheckOutId 
	INNER JOIN 
		Production.ProductCheckout c 
		ON
			CL.CheckOutId = C.Id 
	INNER JOIN 
		Production.ProductInventory i 
		ON 
			a.InventoryId = i.InventoryId 
	WHERE i.InventoryId=@InventoryId 

	ORDER BY TransDate, flag 

UPDATE @stockFlow set @balance = balance=@balance+StockIn-StockOut

SELECT * FROM @stockFlow
GO
/****** Object:  StoredProcedure [Production].[RawMaterialTracking]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[RawMaterialTracking]
(
	@RawMaterialId uniqueidentifier=null, 
	@CartonId nvarchar(15)=null,
	@SupplierId uniqueidentifier=NULL,
	@CheckInDate datetime=NULL,
	@CheckOutDate datetime=NULL,
	@ReturnDate datetime=NULL,
	@EndProductId uniqueidentifier=NULL,
	@EndProductBatchNumber nvarchar(15)=null,
	@EndProductionStartDate datetime=null
)
as
select 
	i.ProductId as RawMaterialId,
	i.InventoryId,
	i.BatchNumber as CartonId, 
	production.GetProductNames(i.ProductId) as ProductName,
	i.Quantity as Weight,
	i.MeasurementUnitId,
	mu.Name as MeasurementUnitName,
	i.ProductionDate,
	i.ExpirationDate as ExpiryDate,
	i.ModifiedDate,
	i.LocationId,
	l.Name as LocationName,
	i.RackingPalletId,
	r.Col+'-'+cast(r.Row as nvarchar(5)) as ColRow,
	ci.CheckInDate,
	ci.SupplierId,
	s.Name as SupplierName,
	cil.QuantityPerBatch as InitialWeight,
	co.CheckOutDate,
	col.Quantity as AllocatedForProductionWeight,
	pd.StartDate as UsedInItemProductionDate,
	pl.ProductId as EndProductId,
	pd.BatchNumber as EndProductBatchNumber,
	production.GetProductNames(pl.ProductId) as EndProductName,
	pl.QuantityPerBatch as EndProductWeight,
	mu2.Name as EndProductMeasurementUnitName,
	ici.CheckInDate as ReturnDate,
	icil.Quantity as ReturnWeight
FROM
	Production.ProductInventory i 
LEFT OUTER JOIN  
	Production.Product p
	ON
		i.ProductId = p.Id 
LEFT OUTER JOIN 
	PRODUCTION.Location l 
	ON
		i.LocationId = l.id 
LEFT OUTER JOIN 
	Production.RackingPallet r 
	ON
		i.RackingPalletId = r.Id
LEFT OUTER JOIN 
	Purchasing.ProductCheckInLineDetail cid 
	ON 
		i.InventoryId = cid.InventoryId 
LEFT OUTER JOIN 
	Purchasing.ProductCheckInLine cil 
	ON
		cid.LineId = cil.LineId 
LEFT OUTER JOIN 
	purchasing.ProductCheckIn ci 
	on
		cil.CheckInId = ci.Id 
LEFT OUTER JOIN 
	Purchasing.Supplier s 
	ON
		ci.SupplierId = s.BusinessEntityId 
LEFT OUTER JOIN 
	Production.ProductCheckOutLine col 
	ON
		i.InventoryId = col.InventoryId 
LEFT OUTER JOIN 
	Production.ProductCheckout  co 
	ON
		col.CheckOutId = co.Id 
LEFT OUTER JOIN 
	Production.ProductInternalCheckInLine icil 
	ON
		i.InventoryId = icil.InventoryId 
LEFT OUTER JOIN 
	Production.ProductInternalCheckIn ici 
	ON
		icil.CheckInId = ici.Id 
LEFT OUTER JOIN 
	Production.ProductionOrderLineDetailResourceAllocation pra 
	ON
		i.InventoryId = pra.InventoryId 
LEFT OUTER JOIN 
	Production.ProductionOrderLineDetailResource pr 
	ON
		pra.ResourceId = pr.ResourceId 
LEFT OUTER JOIN 
	Production.ProductionOrderLineDetail pd 
	ON
		pr.InventoryId = pd.InventoryId 
LEFT OUTER JOIN 
	Production.ProductionOrderLine pl 
	ON
		pd.LineId = pl.LineId 
LEFT OUTER JOIN 
	Production.Product p2 
	ON
		pl.ProductId = p2.Id 
LEFT OUTER JOIN 
	Production.MeasurementUnit mu 
	on i.MeasurementUnitId = mu.Id 
LEFT OUTER JOIN 
	PRODUCTION.MeasurementUnit mu2 
	ON
		pl.MeasurementUnitId = mu2.Id 
WHERE 
	i.ProductId=case when @RawMaterialId is null or @RawMaterialId=cast(cast(0 as binary) as uniqueidentifier) then i.ProductId else @RawMaterialId end 
	AND
	i.BatchNumber=case when LEN(isnull(@cartonId,''))=0 then i.BatchNumber else @CartonId end
	AND 
	ci.SupplierId=case when @SupplierId is null or @SupplierId=cast(cast(0 as binary) as uniqueidentifier) then ci.SupplierId else @SupplierId end 
	AND 
	datediff(day, ci.CheckInDate, case when isnull(@CheckInDate,'2023-12-31')='2023-12-31' then ci.CheckInDate else @CheckInDate end)=0  
	AND
	datediff(day, isnull(co.CheckOutDate,'2023-12-31'), case when isnull(@CheckOutDate,'2023-12-31')='2023-12-31' then isnull(co.CheckOutDate,'2023-12-31') else @CheckOutDate end)=0 
	and
	datediff(day, isnull(ici.CheckInDate,'2023-12-31'), case when isnull(@ReturnDate,'2023-12-31')='2023-12-31' then isnull(ici.CheckInDate,'2023-12-31') else @ReturnDate end)=0 
	and
	datediff(day, isnull(pd.StartDate,'2023-12-31'), case when isnull(@EndProductionStartDate,'2023-12-31')='2023-12-31' then isnull(PD.StartDate,'2023-12-31') else @EndProductionStartDate end)=0 
	and
	isnull(pl.ProductId,cast(cast(0 as binary) as uniqueidentifier)) = 
		case when @EndProductId is null or @EndProductId=cast(cast(0 as binary) as uniqueidentifier) then 
			isnull(pl.ProductId,cast(cast(0 as binary) as uniqueidentifier)) else @EndProductId end 
	and 
	isnull(pd.BatchNumber,'0C64BE54-A555-472C-8DF8-73A8FD76A845') = case when LEN(isnull(@EndProductBatchNumber,''))=0 then 
		isnull(pd.BatchNumber,'0C64BE54-A555-472C-8DF8-73A8FD76A845') else @EndProductBatchNumber end

GO
/****** Object:  StoredProcedure [Purchasing].[GetCheckinRawMaterials]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Purchasing].[GetCheckinRawMaterials]
(
	@Id uniqueidentifier
)
As
SELECT
	l.LineId,
	d.InventoryId,
	l.ProductId,
	Production.GetProductNames(l.ProductId) as ProductName,
	d.BatchNumber,
	l.QuantityPerBatch as Quantity,
	l.MeasurementUnitId,
	mu.[Name] as MeasurementUnitName,
	l.ProductionDate,
	l.ExpirationDate,
	l.LocationId,
	loc.[Name] as LocationName,
	l.RackingPalletId,
	r.Col+'-'+cast(r.[Row] as nvarchar(5)) as RackingPalletName,
	c.Id as CheckinId,
	c.CheckInDate as CheckinDate,
	l.TotalBatches
FROM 
	Purchasing.ProductCheckIn c 
	INNER JOIN Purchasing.ProductCheckInLine l 
		ON c.Id = l.CheckInId 
	INNER JOIN Purchasing.ProductCheckInLineDetail d 
		ON l.LineId = d.LineId 
	INNER JOIN Production.Product P 
		ON l.ProductId = p.Id 
	INNER JOIN Production.MeasurementUnit mu 
		ON l.MeasurementUnitId = mu.Id 
	INNER JOIN Production.[Location] loc 
		ON l.LocationId = loc.Id 
	INNER JOIN Production.RackingPallet r 
		ON l.RackingPalletId = r.Id
WHERE
	c.Id=@Id
GO
/****** Object:  StoredProcedure [Purchasing].[GetEndProducts]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Proc [Purchasing].[GetEndProducts]
as
SELECT 
	p.Id, 
	ProductNumber,
	production.GetProductNames(p.Id) as [Name],
	MeasurementUnitOrderId,
	mu.Name as MeasurementUnitOrderName,
	MeasurementUnitGroupId,
	mg.Name as MeasurementUnitGroupName, 
	LocationId,
	RackingPalletId,
	DaysToExpire,
	DaysToManufacture,
	OrderQuantity 
FROM 
	Production.Product p 
	INNER JOIN production.MeasurementUnit mu
		ON p.MeasurementUnitOrderId = mu.Id
	INNER JOIN production.MeasurementUnitGroup mg 
		ON p.MeasurementUnitGroupId = mg.Id
WHERE 
	IsFinishedGood = 1 
GO
/****** Object:  StoredProcedure [Purchasing].[GetSupplierById]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Purchasing].[GetSupplierById] 
(
	@BusinessEntityId uniqueidentifier
)
as
select BusinessEntityId, [Name], IsActive from Purchasing.Supplier where BusinessEntityId = @BusinessEntityId
GO
/****** Object:  StoredProcedure [Purchasing].[RawMaterialsForCheckin]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Purchasing].[RawMaterialsForCheckin]
as
SELECT 
	p.Id, 
	ProductNumber,
	production.GetProductNames(p.Id) as [Name],
	MeasurementUnitOrderId,
	mu.Name as MeasurementUnitOrderName,
	MeasurementUnitGroupId,
	mg.Name as MeasurementUnitGroupName, 
	LocationId,
	RackingPalletId,
	DaysToExpire,
	OrderQuantity 
FROM 
	Production.Product p 
	INNER JOIN production.MeasurementUnit mu
		ON p.MeasurementUnitOrderId = mu.Id
	INNER JOIN production.MeasurementUnitGroup mg 
		ON p.MeasurementUnitGroupId = mg.Id
WHERE 
	IsFinishedGood = 0 
GO
/****** Object:  StoredProcedure [Purchasing].[SupplierList]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Purchasing].[SupplierList]
as
Select BusinessEntityId, [Name], IsActive from Purchasing.Supplier
GO
/****** Object:  StoredProcedure [Sales].[CustomerList]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Sales].[CustomerList]
as
Select BusinessEntityId, [Name], IsActive from Sales.Customer
GO
/****** Object:  StoredProcedure [Sales].[GetCustomerById]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Sales].[GetCustomerById]
(
	@BusinessEntityId uniqueidentifier
)
as
select BusinessEntityId, Name, IsActive from Sales.Customer where BusinessEntityId = @BusinessEntityId

GO
/****** Object:  StoredProcedure [Sales].[GetDispatchOrderDetailByDate]    Script Date: 9/10/2024 7:12:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [Sales].[GetDispatchOrderDetailByDate]
(
	@date datetime
)
as
SELECT
	do.Id,	
	do.OrderDate,
	do.CustomerId,
	c.Name as CustomerName,
	i.ProductId,
	production.GetProductNames(i.ProductId) as ProductName,
	i.BatchNumber,
	dl.InventoryId,
	dl.MeasurementUnitId as MeasurementUnitId,
	(select Name from production.MeasurementUnit where Id=dl.MeasurementUnitId) as ProductMeasurementUnitName,
	dl.Quantity,
	i.Flag 
FROM
	Sales.SalesOrder do
INNER JOIN 
	Sales.Customer c 
	ON
		do.CustomerId = c.BusinessEntityId 
INNER JOIN 
	Sales.SalesOrderLine dl 
	ON 
		do.Id = dl.OrderId
INNER JOIN 
	Production.ProductInventory i 
	ON 
		dl.InventoryId = i.InventoryId 
WHERE  
	DATEDIFF(day,do.OrderDate,@date)=0
GO
DROP PROCEDURE IF EXISTS Production.GetPackagedProductsByLocation
GO
CREATE Proc [Production].[GetPackagedProductsByLocation]
as
SELECT 
	inv.ProductId,
	inv.InventoryId,
	inv.BatchNumber,
	production.GetProductNames(inv.ProductId) as Names,
	inv.Quantity as Weight,
	inv.MeasurementUnitId,
	isnull(mu.Initial, mu.Name) as MeasurementUnitName,
	inv.TotalBatches as Quantity,
	p.LocationId,
	loc.Name as LocationName,
	p.RackingPalletId, 
	rack.Col+'-'+cast(rack.row as nvarchar(5)) as RackingPalletColRow,
	p.DaysToExpire,
	p.DaysToManufacture,
	inv.ExpirationDate as ExpiryDate,
	inv.ProductionDate,
	isnull(p.OutLocationId, 
		isnull((select top 1 Id from production.location where name like '%product%'),
			isnull((select top 1 Id from production.Location where Id<>p.LocationId),p.locationId))) 
		as OutLocationId,
	loc2.Name as OutLocationName,
	isnull(p.OutRackingPalletId, p.rackingPalletId) as OutRackingPalletId,
	rack2.Col+'-'+cast(rack2.row as nvarchar(5)) as OutRackingPalletColRow,
	inv.LocationId as CurrentLocationId,
	loc3.Name as CurrentLocationName,
	inv.RackingPalletId as CurrentRackingPalletId,
	rack3.Col+'-'+cast(rack3.row as nvarchar(5)) as CurrentRackingPalletColRow,
	p.IsFinishedGood 
FROM 
	Production.ProductInventory inv 
INNER JOIN 
	Production.Product p 
	ON
		inv.ProductId = p.Id 
INNER JOIN 
	Production.MeasurementUnit mu 
	ON
		inv.MeasurementUnitId = mu.Id 
INNER JOIN 
	Production.Location loc 
	ON
		p.LocationId = loc.Id 
INNER JOIN 
	Production.RackingPallet rack 
	ON
		p.RackingPalletId = rack.Id
INNER JOIN 
	Production.Location loc2 
	ON
		isnull(p.OutLocationId, 
		isnull((select top 1 Id from production.location where name like '%product%'),
			isnull((select top 1 Id from production.Location where Id<>p.LocationId),p.locationId)))  = loc2.Id 
INNER JOIN 
	Production.RackingPallet rack2 
	ON
		isnull(p.OutRackingPalletId, p.RackingPalletId) = rack2.Id
INNER JOIN 
	Production.Location loc3 
	ON
		inv.LocationId = loc3.Id 
INNER JOIN 
	Production.RackingPallet rack3 
	ON
		inv.RackingPalletId = rack3.Id
WHERE 
	
	inv.Flag in (1,3,7,8,9,10,11,13,14,15) 
	AND 
	p.IsFinishedGood = 1
GO
if not exists(select * from Production.InventoryFlag where Id=1) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (1, N'Checkin (Purchased)')
GO
if not exists(select * from Production.InventoryFlag where Id=2) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (2, N'Checkout for Production')
GO
if not exists(select * from Production.InventoryFlag where Id=3) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (3, N'Return from Production')
GO
if not exists(select * from Production.InventoryFlag where Id=4) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (4, N'Production - Aborted')
GO
if not exists(select * from Production.InventoryFlag where Id=5)  
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (5, N'Production - Not Started')
GO
if not exists(select * from Production.InventoryFlag where Id=6) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (6, N'Production - In Progress')
GO
if not exists(select * from Production.InventoryFlag where Id=7) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (7, N'Production - Completed')
GO
if not exists(select * from Production.InventoryFlag where Id=8) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (8, N'Checkin from Production')
GO
if not exists(select * from Production.InventoryFlag where Id=9) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (9, N'New Delivery Order')
GO
if not exists(select * from Production.InventoryFlag where Id=10) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (10, N'Packing for delivery')
GO
if not exists(select * from Production.InventoryFlag where Id=11) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (11, N'Packed for delivery')
GO
if not exists(select * from Production.InventoryFlag where Id=12) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (12, N'Dispatched')
GO
if not exists(select * from Production.InventoryFlag where Id=13) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (13, N'Move location')
GO
if not exists(select * from Production.InventoryFlag where Id=14) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (14, N'Excessive Raw Material')
GO
if not exists(select * from Production.InventoryFlag where Id=15) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (15, N'Unused for any productions')
GO
if not exists(select * from Production.InventoryFlag where Id=16) 
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (16, N'Production Batch - Completed')
GO
if not exists (select * from dbo.userType where name='Administrator')
INSERT dbo.userType(name) values ('Administrator')
GO
if not exists (select * from dbo.Users where name='Admin')
begin 
	declare @adminId uniqueidentifier=(select id from UserType where name='Administrator');
	insert into dbo.Users(name,password,TypeId) values
		('Admin','7bebdf35690402ed85c461464de7c27b',@adminId)
end
GO
delete from dbo.Feature
GO
declare @parentId uniqueidentifier

insert into dbo.Feature(Name, Icon) values ('Basic Data','dataset')
select @parentId=id from dbo.Feature where Name='Basic Data'
insert into dbo.Feature(Name, ParentId, Path, Icon) values ('Product',@parentId,'/ProductList','category')
insert into dbo.Feature(Name, ParentId, Path, Icon) values ('Location',@parentId,'/LocationList','holiday_village')
insert into dbo.Feature(Name, ParentId, Path, Icon) values ('Supplier',@parentId,'/SupplierList','add_business')
insert into dbo.Feature(Name, ParentId, Path, Icon) values ('Customer',@parentId,'/CustomerList','storefront')

insert into dbo.Feature(Name, Icon) values ('Purchasing','shop_two')
select @parentId=id from dbo.Feature where Name='Purchasing'
insert into dbo.Feature( Name,ParentId, Path, Icon) values ('Raw Material Checkin',@parentId,'/Checkin','add_shopping_cart')
insert into dbo.Feature( Name,ParentId, Path, Icon) values ('Raw Material Checkin Reports',@parentId,'/UnderConstruction','insert_chart')

insert into dbo.Feature(Name, Icon) values ('Production','factory')
select @parentId=id from dbo.Feature where Name='Production'
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Production Order',@parentId,'/ProductionOrder','add_shopping_cart')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Daily Production',@parentId,'/ProductionStatus','event')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Raw Material Checkout',@parentId,'/RawMaterialCheckout','send_and_archive')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('End Product Checkin',@parentId,'/CompleteProductionEntry','add_task')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Production Reports',@parentId,'/UnderConstruction','insert_chart')

insert into dbo.Feature(Name, Icon) values ('Inventory','inventory')
select @parentId=id from dbo.Feature where Name='Inventory'
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Raw Material Transfer',@parentId,'/StockTransfer','move_up')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Raw Material Adjustment',@parentId,'/StockAdjustment','scale')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Raw Material Inventory',@parentId,'/rawmaterial-inventories','inventory')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Finished Product Transfer',@parentId,'/EndProductStockTransfer','trolley')
Insert into dbo.feature(Name, ParentId, Path, Icon) values ('Finished Product Adjustment',@parentId,'/EndProductStockAdjustment','published_with_changes')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Finished Product Inventory',@parentId,'/finished-product-inventories','inventory')


insert into dbo.Feature(Name, Icon) values ('Dispatching','local_shipping')
select @parentId=id from dbo.Feature where Name='Dispatching'
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Dispatch End Product',@parentId,'/DispatchEntry','shopping_cart_checkout')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Dispatch Status',@parentId,'/DispatchStatus','schedule_send')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Dispatch Reports',@parentId,'/UnderConstruction','insert_chart')

insert into dbo.Feature(Name, Icon) values ('Configuration','settings')
select @parentId=id from dbo.Feature where Name='Configuration'
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('User Management',@parentId,'/UserManagement','manage_accounts')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('System Configuration',@parentId,'/AppSettingPage','settings_applications')

insert into UserTypeFeature 
	select f.Id, t.Id, 0
	FROM dbo.Feature f, UserType t 

update UserTypeFeature set Accessible=1 
FROM UserTypeFeature f inner join UserType t 
ON f.UserTypeId=t.Id 
WHERE t.Name='Administrator'
GO
