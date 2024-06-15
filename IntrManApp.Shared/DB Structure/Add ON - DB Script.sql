USE [IntrManDB]
GO
if not exists(select * from sys.columns where Name='OutLocationId') ALTER Table Production.Product ADD OutLocationId Uniqueidentifier NULL
go
if not exists(select * from sys.columns where Name='OutRackingPalletId') ALTER Table Production.Product ADD	OutRackingPalletId uniqueidentifier NULL
GO
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (14, N'Excessive Raw Material')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (13, N'Unused for any productions')
GO
/****** Object:  StoredProcedure [Sales].[GetDispatchOrderDetailByDate]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Sales].[GetDispatchOrderDetailByDate]
GO
/****** Object:  StoredProcedure [Sales].[GetCustomerById]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Sales].[GetCustomerById]
GO
/****** Object:  StoredProcedure [Sales].[CustomerList]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Sales].[CustomerList]
GO
/****** Object:  StoredProcedure [Purchasing].[SupplierList]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Purchasing].[SupplierList]
GO
/****** Object:  StoredProcedure [Purchasing].[RawMaterialsForCheckin]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Purchasing].[RawMaterialsForCheckin]
GO
/****** Object:  StoredProcedure [Purchasing].[GetSupplierById]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Purchasing].[GetSupplierById]
GO
/****** Object:  StoredProcedure [Purchasing].[GetEndProducts]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Purchasing].[GetEndProducts]
GO
/****** Object:  StoredProcedure [Purchasing].[GetCheckinRawMaterials]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Purchasing].[GetCheckinRawMaterials]
GO
/****** Object:  StoredProcedure [Production].[RawMaterialTracking]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[RawMaterialTracking]
GO
/****** Object:  StoredProcedure [Production].[RawMaterialLedgerById]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[RawMaterialLedgerById]
GO
/****** Object:  StoredProcedure [Production].[RawMaterialLedger]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[RawMaterialLedger]
GO
/****** Object:  StoredProcedure [Production].[RawMaterialInventories]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[RawMaterialInventories]
GO
/****** Object:  StoredProcedure [Production].[RackingPalletList]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[RackingPalletList]
GO
/****** Object:  StoredProcedure [Production].[ProductList]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[ProductList]
GO
/****** Object:  StoredProcedure [Production].[ProductCategoryList]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[ProductCategoryList]
GO
/****** Object:  StoredProcedure [Production].[LocationList]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[LocationList]
GO
/****** Object:  StoredProcedure [Production].[GetRunningProductionItems]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[GetRunningProductionItems]
GO
/****** Object:  StoredProcedure [Production].[GetRawMaterialsForProduction]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[GetRawMaterialsForProduction]
GO
/****** Object:  StoredProcedure [Production].[GetRawMaterialsBasicInfo]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[GetRawMaterialsBasicInfo]
GO
/****** Object:  StoredProcedure [Production].[GetRackingPalletById]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[GetRackingPalletById]
GO
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailById]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[GetProductionOrderDetailById]
GO
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailByDate]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[GetProductionOrderDetailByDate]
GO
/****** Object:  StoredProcedure [Production].[GetLocationById]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[GetLocationById]
GO
/****** Object:  StoredProcedure [Production].[GetDispatchableProducts]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[GetDispatchableProducts]
GO
/****** Object:  StoredProcedure [Production].[FinishedProductLedger]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[FinishedProductLedger]
GO
/****** Object:  StoredProcedure [Production].[FinishedProductInventories]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[FinishedProductInventories]
GO
/****** Object:  StoredProcedure [Production].[DistributeRawMaterialsCheckout]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[DistributeRawMaterialsCheckout]
GO
/****** Object:  StoredProcedure [Production].[BomSpecification]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [Production].[BomSpecification]
GO
/****** Object:  StoredProcedure [dbo].[ResetTransactions]    Script Date: 6/15/2024 11:34:44 AM ******/
DROP PROCEDURE [dbo].[ResetTransactions]
GO
/****** Object:  StoredProcedure [dbo].[ResetTransactions]    Script Date: 6/15/2024 11:34:44 AM ******/
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
delete from Production.ProductInventory
GO
/****** Object:  StoredProcedure [Production].[BomSpecification]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Production].[DistributeRawMaterialsCheckout]    Script Date: 6/15/2024 11:34:44 AM ******/
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
			Production.ProductionOrderLine orl  
		ON 
			ord.LineId = orl.LineId 
		INNER JOIN 
			Production.ProductionOrder o 
		ON 
			orl.ProductionOrderId = o.Id 
		WHERE 
			ord.StartDate is null and rsc.RawMaterialId = @chkRawMaterialId 
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
/****** Object:  StoredProcedure [Production].[FinishedProductInventories]    Script Date: 6/15/2024 11:34:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
	isnull(i.Flag,0) < 12 
	AND
	p.IsFinishedGood = 1
GROUP BY
	p.CategoryId, c.Name, p.Id, p.ProductNumber, production.GetProductNames(p.Id), i.Quantity, mu.Name, 
	i.LocationId, l.Name, r.Col+'-'+cast(r.Row as nvarchar(5)), i.Flag, f.Name  
GO
/****** Object:  StoredProcedure [Production].[FinishedProductLedger]    Script Date: 6/15/2024 11:34:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [Production].[FinishedProductLedger]
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
		1, 0, 0,
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
	WHERE I.ProductId = @ProductId AND CL.LocationId=@LocationId 
UNION ALL 
	SELECT 
		c.CheckOutDate as TransDate, cl.InventoryId, i.BatchNumber, 
		Description =  'Move to ' + (select Name from Production.Location where id=cl.LocationId),
		0, 1, 0,
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
		SO.OrderDate, sl.InventoryId, I.BatchNumber, f.Name, 0, 1, 0, i.Flag 
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
	WHERE I.ProductId = @ProductId AND i.LocationId = @LocationId AND I.Flag BETWEEN 10 AND 12

	ORDER BY TransDate 

UPDATE @stockFlow set @balance = balance=@balance+StockIn-StockOut

SELECT * FROM @stockFlow
GO
/****** Object:  StoredProcedure [Production].[GetDispatchableProducts]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Production].[GetLocationById]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailByDate]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailById]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Production].[GetRackingPalletById]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Production].[GetRawMaterialsBasicInfo]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Production].[GetRawMaterialsForProduction]    Script Date: 6/15/2024 11:34:44 AM ******/
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
	p.OutLocationId as LocationId,
	loc.Name as LocationName,
	p.OutRackingPalletId as RackingPalletId, 
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
		p.OutLocationId = loc.Id 
INNER JOIN 
	Production.RackingPallet rack 
	ON
		p.OutRackingPalletId = rack.Id 
WHERE 
	inv.Flag in (1,3) AND p.IsFinishedGood = 0
GO
/****** Object:  StoredProcedure [Production].[GetRunningProductionItems]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Production].[LocationList]    Script Date: 6/15/2024 11:34:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Production].[LocationList]
as
Select Id, [Name] from production.[Location]
GO
/****** Object:  StoredProcedure [Production].[ProductCategoryList]    Script Date: 6/15/2024 11:34:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [Production].[ProductCategoryList]
as
select Id, Name from Production.ProductCategory
GO
/****** Object:  StoredProcedure [Production].[ProductList]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Production].[RackingPalletList]    Script Date: 6/15/2024 11:34:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Production].[RackingPalletList]
as
Select Id, Col, Row, [Description], Col+'-'+cast(Row as nvarchar(5)) as ColRow from production.RackingPallet
GO
/****** Object:  StoredProcedure [Production].[RawMaterialInventories]    Script Date: 6/15/2024 11:34:44 AM ******/
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
	isnull(i.Flag,0) < 12 
	AND
	p.IsFinishedGood = 0
GROUP BY
	p.CategoryId, c.Name, p.Id, p.ProductNumber, production.GetProductNames(p.Id), i.Quantity, mu.Name, 
	i.LocationId, l.Name, r.Col+'-'+cast(r.Row as nvarchar(5)), i.Flag, f.Name  
GO
/****** Object:  StoredProcedure [Production].[RawMaterialLedger]    Script Date: 6/15/2024 11:34:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
		'Move From ' + (select Name from Production.Location where id=cl.SourceLocationId),
		cl.Quantity, 0, 0, 13 
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
		Description =  'Move to ' + (select Name from Production.Location where id=cl.LocationId),
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
		SO.CheckOutDate, sl.InventoryId, I.BatchNumber, 'Checkout for Production', 0, sl.Quantity, 0, 2  
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
	WHERE I.ProductId = @ProductId AND sl.SourceLocationId = @LocationId 
UNION ALL
	SELECT 
		SO.CheckOutDate, sl.InventoryId, i.BatchNumber, 'Check-in for Production', sl.Quantity, 0, 0, 2  
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
		c.CheckInDate, cd.InventoryId, cd.BatchNumber, 'Check-in from '+ s.Name, cl.QuantityPerBatch, 0, 0, 1 
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
		C.CheckOutDate, a.InventoryId, i.BatchNumber, 'Allocated for Production ', 0, a.Quantity, 0, 6 
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

	ORDER BY TransDate 

UPDATE @stockFlow set @balance = balance=@balance+StockIn-StockOut

SELECT * FROM @stockFlow

GO
/****** Object:  StoredProcedure [Production].[RawMaterialLedgerById]    Script Date: 6/15/2024 11:34:44 AM ******/
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
	Description nvarchar(50),
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
		Description =  'Return from '+(select Name from Production.Location where id=cl.SourceLocationId) +' to' + (select Name from Production.Location where id=cl.LocationId),
		0, 0, 0,
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
	WHERE i.InventoryId=@InventoryId 
UNION ALL
	SELECT 
		SO.CheckOutDate as TransDate, sl.InventoryId, I.BatchNumber, 'Checkout for Production', 0, 0, 0, 2  
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
--UNION ALL
--	SELECT 
--		SO.CheckOutDate, sl.InventoryId, i.BatchNumber, 'Check-in for Production', sl.Quantity, 0, 0, 2  
--	FROM 
--		Production.ProductCheckOutLine sl 
--	INNER JOIN 
--		Production.ProductCheckout so 
--		ON
--			sl.CheckOutId = so.Id 
--	INNER JOIN 
--		Production.ProductInventory i 
--		ON
--			sl.InventoryId = i.InventoryId 
--	WHERE I.ProductId = @ProductId AND sl.LocationId = @LocationId 
UNION ALL
	SELECT 
		c.CheckInDate as TransDate, cd.InventoryId, cd.BatchNumber, 'Check-in from '+ s.Name, cl.QuantityPerBatch, 0, 0, 1 
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
	--+production.GetProductNames(l.ProductId) + '#' + d.BatchNumber
		C.CheckOutDate, a.InventoryId, i.BatchNumber, 'Allocated for Production ', 0, a.Quantity, 0, 6 
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
	WHERE i.InventoryId=@InventoryId 

	ORDER BY TransDate 

UPDATE @stockFlow set @balance = balance=@balance+StockIn-StockOut

SELECT * FROM @stockFlow

GO
/****** Object:  StoredProcedure [Production].[RawMaterialTracking]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Purchasing].[GetCheckinRawMaterials]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Purchasing].[GetEndProducts]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Purchasing].[GetSupplierById]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Purchasing].[RawMaterialsForCheckin]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Purchasing].[SupplierList]    Script Date: 6/15/2024 11:34:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Purchasing].[SupplierList]
as
Select BusinessEntityId, [Name], IsActive from Purchasing.Supplier
GO
/****** Object:  StoredProcedure [Sales].[CustomerList]    Script Date: 6/15/2024 11:34:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Sales].[CustomerList]
as
Select BusinessEntityId, [Name], IsActive from Sales.Customer
GO
/****** Object:  StoredProcedure [Sales].[GetCustomerById]    Script Date: 6/15/2024 11:34:44 AM ******/
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
/****** Object:  StoredProcedure [Sales].[GetDispatchOrderDetailByDate]    Script Date: 6/15/2024 11:34:44 AM ******/
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
