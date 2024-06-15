USE [master]
GO
/****** Object:  Database [IntrManDB]    Script Date: 6/7/2024 7:24:21 PM ******/
CREATE DATABASE [IntrManDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IntrManDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\IntrManDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'IntrManDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\IntrManDB_log.ldf' , SIZE = 16576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [IntrManDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IntrManDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IntrManDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IntrManDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IntrManDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IntrManDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IntrManDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [IntrManDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IntrManDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IntrManDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IntrManDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IntrManDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IntrManDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IntrManDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IntrManDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IntrManDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IntrManDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IntrManDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IntrManDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IntrManDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IntrManDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IntrManDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IntrManDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IntrManDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IntrManDB] SET RECOVERY FULL 
GO
ALTER DATABASE [IntrManDB] SET  MULTI_USER 
GO
ALTER DATABASE [IntrManDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IntrManDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IntrManDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IntrManDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [IntrManDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'IntrManDB', N'ON'
GO
USE [IntrManDB]
GO
/****** Object:  Schema [Person]    Script Date: 6/7/2024 7:24:21 PM ******/
CREATE SCHEMA [Person]
GO
/****** Object:  Schema [Production]    Script Date: 6/7/2024 7:24:21 PM ******/
CREATE SCHEMA [Production]
GO
/****** Object:  Schema [Purchasing]    Script Date: 6/7/2024 7:24:21 PM ******/
CREATE SCHEMA [Purchasing]
GO
/****** Object:  Schema [Sales]    Script Date: 6/7/2024 7:24:21 PM ******/
CREATE SCHEMA [Sales]
GO
/****** Object:  UserDefinedFunction [Production].[GetProductNames]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
/****** Object:  Table [Person].[BusinessEntity]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Person].[BusinessEntity](
	[Id] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_BusinessEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Person].[BusinessEntityContact]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Person].[BusinessEntityContact](
	[BusinessEntityId] [uniqueidentifier] NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[ContactTypeId] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_BusinessEntityContact] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityId] ASC,
	[PersonId] ASC,
	[ContactTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Person].[ContactType]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Person].[ContactType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ContactType] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Person].[Person]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Person].[Person](
	[BusinessEntityId] [uniqueidentifier] NOT NULL,
	[PersonTypeId] [uniqueidentifier] NULL,
	[Title] [nvarchar](8) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Suffix] [nvarchar](10) NULL,
	[AdditionalContactInfo] [xml] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Person].[PersonType]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Person].[PersonType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PersonType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_PersonType] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[BillOfMaterials]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[BillOfMaterials](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[RawMaterialId] [uniqueidentifier] NOT NULL,
	[RawMaterialMeasurementUnitId] [uniqueidentifier] NOT NULL,
	[RawMaterialQuantity] [decimal](8, 2) NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_BillOfMaterials] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[Culture]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[Culture](
	[Id] [nchar](6) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [Pk_Culture_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[DiscrepantReason]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[DiscrepantReason](
	[Id] [uniqueidentifier] NOT NULL,
	[Reason] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_DiscrepantReason] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DiscrepantReason] UNIQUE NONCLUSTERED 
(
	[Reason] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[InventoryFlag]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[InventoryFlag](
	[Id] [tinyint] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_InventoryFlag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[Location]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[Location](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Location] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[MeasurementUnit]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[MeasurementUnit](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ChildId] [uniqueidentifier] NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[GroupId] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MeasurementUnit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_MeasurementUnit] UNIQUE NONCLUSTERED 
(
	[Name] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[MeasurementUnitGroup]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[MeasurementUnitGroup](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MeasurementUnitGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_MeasurementUnitGroup] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[Product]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[ProductNumber] [nvarchar](25) NOT NULL,
	[IsFinishedGood] [bit] NOT NULL,
	[IsSalable] [bit] NULL,
	[IsUniqueBatchPerOrder] [bit] NULL,
	[SafetyStockLevel] [decimal](18, 2) NULL,
	[ReorderPoint] [decimal](18, 2) NULL,
	[StandardCost] [money] NULL,
	[ListPrice] [money] NULL,
	[MeasurementUnitGroupId] [uniqueidentifier] NOT NULL,
	[MeasurementUnitOrderId] [uniqueidentifier] NOT NULL,
	[OrderQuantity] [decimal](8, 2) NULL,
	[DaysToManufacture] [int] NULL,
	[DaysToExpire] [int] NULL,
	[LocationId] [uniqueidentifier] NOT NULL,
	[RackingPalletId] [uniqueidentifier] NULL,
	[AdditionalInfo] [xml] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Product] UNIQUE NONCLUSTERED 
(
	[ProductNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductCategory]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductCategory](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductCategory] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductCheckout]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductCheckout](
	[Id] [uniqueidentifier] NOT NULL,
	[CheckOutDate] [datetime] NULL,
	[RevisionNumber] [tinyint] NULL,
	[ModifierDate] [datetime] NULL,
 CONSTRAINT [PK_ProductCheckout] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductCheckOutLine]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductCheckOutLine](
	[CheckOutId] [uniqueidentifier] NOT NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[LocationId] [uniqueidentifier] NULL,
	[RackingPalletId] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[SourceLocationId] [uniqueidentifier] NULL,
	[SourceRackingPalletId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProductCheckOutLine] PRIMARY KEY CLUSTERED 
(
	[CheckOutId] ASC,
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductInternalCheckIn]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductInternalCheckIn](
	[Id] [uniqueidentifier] NOT NULL,
	[CheckInDate] [datetime] NULL,
	[CheckInType] [tinyint] NULL,
	[RevisionNumber] [tinyint] NULL,
	[ModifierDate] [datetime] NULL,
 CONSTRAINT [PK_ProductReadyCheckIn] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductInternalCheckInLine]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductInternalCheckInLine](
	[CheckInId] [uniqueidentifier] NOT NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[LocationId] [uniqueidentifier] NULL,
	[RackingPalletId] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[SourceLocationId] [uniqueidentifier] NULL,
	[SourceRackingPalletId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProductReadyCheckInLine] PRIMARY KEY CLUSTERED 
(
	[CheckInId] ASC,
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductInternalCheckout]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductInternalCheckout](
	[Id] [uniqueidentifier] NOT NULL,
	[CheckOutDate] [datetime] NULL,
	[CheckOutType] [tinyint] NULL,
	[RevisionNumber] [tinyint] NULL,
	[ModifierDate] [datetime] NULL,
 CONSTRAINT [PK_ProductInternalCheckout] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductInternalCheckOutLine]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductInternalCheckOutLine](
	[CheckOutId] [uniqueidentifier] NOT NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[LocationId] [uniqueidentifier] NULL,
	[RackingPalletId] [uniqueidentifier] NULL,
	[SourceLocationId] [uniqueidentifier] NULL,
	[SourceRackingPalletId] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProductInternalCheckOutLine] PRIMARY KEY CLUSTERED 
(
	[CheckOutId] ASC,
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductInventory]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductInventory](
	[InventoryId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
	[LocationId] [uniqueidentifier] NULL,
	[RackingPalletId] [uniqueidentifier] NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[ProductionDate] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
	[Flag] [tinyint] NULL,
	[ModifiedDate] [datetime] NULL,
	[TransIdReference] [uniqueidentifier] NULL,
	[TotalBatches] [int] NULL,
 CONSTRAINT [PK_ProductInventory] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrder]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrder](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[ScheduledDate] [datetime] NULL,
 CONSTRAINT [PK_ProductionOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrderLine]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrderLine](
	[ProductionOrderId] [uniqueidentifier] NULL,
	[LineId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NULL,
	[TotalBatches] [int] NOT NULL,
	[QuantityPerBatch] [decimal](18, 2) NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[TotalBatchesCompleted] [int] NULL,
	[TotalBatchesScrapped] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProductionOrderLine] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrderLineDetail]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrderLineDetail](
	[LineId] [uniqueidentifier] NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProductionOrderLineDetail] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrderLineDetailResource]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrderLineDetailResource](
	[RawMaterialId] [uniqueidentifier] NULL,
	[ResourceId] [uniqueidentifier] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[ModifierDate] [datetime] NULL,
	[InventoryId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProductionOrderLineDetailResource] PRIMARY KEY CLUSTERED 
(
	[ResourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrderLineDetailResourceAllocation]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrderLineDetailResourceAllocation](
	[ResourceId] [uniqueidentifier] NULL,
	[ProductCheckoutId] [uniqueidentifier] NULL,
	[InventoryId] [uniqueidentifier] NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[ModifierDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductNameAndDescriptionCulture]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductNameAndDescriptionCulture](
	[ProductId] [uniqueidentifier] NOT NULL,
	[CultureId] [nchar](6) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_ProductNameAndDescriptionCulture] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[CultureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductPhoto]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductPhoto](
	[Id] [uniqueidentifier] NOT NULL,
	[ThumbNailPhoto] [varbinary](max) NULL,
	[ThumbnailPhotoFileName] [nvarchar](50) NULL,
	[LargePhoto] [varbinary](max) NULL,
	[LargePhotoFileName] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductPhoto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductProductPhoto]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductProductPhoto](
	[ProductId] [uniqueidentifier] NULL,
	[ProductPhoto] [uniqueidentifier] NULL,
	[Primary] [bit] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[RackingPallet]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[RackingPallet](
	[Id] [uniqueidentifier] NOT NULL,
	[Col] [nvarchar](35) NULL,
	[Row] [smallint] NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_RackingPallet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[StockAdjustMent]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[StockAdjustMent](
	[Id] [uniqueidentifier] NOT NULL,
	[AdjustmentDate] [datetime] NULL,
	[RevisionNumber] [tinyint] NULL,
	[ModifierDate] [datetime] NULL,
 CONSTRAINT [PK_StockAdjustMent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[StockAdjustmentLine]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[StockAdjustmentLine](
	[AdjustmentId] [uniqueidentifier] NOT NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[Adjustment] [decimal](18, 2) NULL,
	[ReasonId] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_StockAdjustmentLine] PRIMARY KEY CLUSTERED 
(
	[AdjustmentId] ASC,
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Purchasing].[ProductCheckIn]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Purchasing].[ProductCheckIn](
	[Id] [uniqueidentifier] NOT NULL,
	[CheckInDate] [datetime] NULL,
	[SupplierId] [uniqueidentifier] NULL,
	[RevisionNumber] [tinyint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProductCheckIn] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Purchasing].[ProductCheckInLine]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Purchasing].[ProductCheckInLine](
	[CheckInId] [uniqueidentifier] NULL,
	[LineId] [uniqueidentifier] NOT NULL,
	[LineIndex] [smallint] NULL,
	[ProductId] [uniqueidentifier] NULL,
	[TotalBatches] [int] NOT NULL,
	[QuantityPerBatch] [decimal](18, 2) NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[ProductionDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[LocationId] [uniqueidentifier] NULL,
	[RackingPalletId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ProductCheckInLine_1] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Purchasing].[ProductCheckInLineDetail]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Purchasing].[ProductCheckInLineDetail](
	[LineId] [uniqueidentifier] NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_ProductCheckInLineDetail_1] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductCheckInLineDetail] UNIQUE NONCLUSTERED 
(
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Purchasing].[Supplier]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Purchasing].[Supplier](
	[BusinessEntityId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Supplier] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[Customer]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[Customer](
	[BusinessEntityId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[SalesOrder]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[SalesOrder](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderDate] [datetime] NULL,
	[CustomerId] [uniqueidentifier] NULL,
	[Status] [tinyint] NULL,
	[RevisionNumber] [tinyint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_SalesOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[SalesOrderLine]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[SalesOrderLine](
	[OrderId] [uniqueidentifier] NOT NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[Quantity] [decimal](18, 2) NULL,
 CONSTRAINT [PK_SalesOrderLine] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductInventory]    Script Date: 6/7/2024 7:24:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductInventory] ON [Production].[ProductInventory]
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductInventory_2]    Script Date: 6/7/2024 7:24:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductInventory_2] ON [Production].[ProductInventory]
(
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RackingPallet]    Script Date: 6/7/2024 7:24:21 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_RackingPallet] ON [Production].[RackingPallet]
(
	[Col] ASC,
	[Row] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customer]    Script Date: 6/7/2024 7:24:21 PM ******/
CREATE NONCLUSTERED INDEX [IX_Customer] ON [Sales].[Customer]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [Person].[BusinessEntity] ADD  CONSTRAINT [DF_BusinessEntity_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Person].[BusinessEntity] ADD  CONSTRAINT [DF_BusinessEntity_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Person].[BusinessEntityContact] ADD  CONSTRAINT [DF_BusinessEntityContact_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Person].[ContactType] ADD  CONSTRAINT [DF_ContactType_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Person].[ContactType] ADD  CONSTRAINT [DF_ContactType_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Person].[Person] ADD  CONSTRAINT [DF_Person_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Person].[PersonType] ADD  CONSTRAINT [DF_PersonType_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Person].[PersonType] ADD  CONSTRAINT [DF_PersonType_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[BillOfMaterials] ADD  CONSTRAINT [DF_BillOfMaterials_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[BillOfMaterials] ADD  CONSTRAINT [DF_BillOfMaterials_RawMaterialQuantity]  DEFAULT ((1)) FOR [RawMaterialQuantity]
GO
ALTER TABLE [Production].[BillOfMaterials] ADD  CONSTRAINT [DF_BillOfMaterials_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[Culture] ADD  CONSTRAINT [DF_Culture_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[DiscrepantReason] ADD  CONSTRAINT [DF_DiscrepantReason_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[Location] ADD  CONSTRAINT [DF_Location_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[Location] ADD  CONSTRAINT [DF_Location_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[MeasurementUnit] ADD  CONSTRAINT [DF_MeasurementUnit_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[MeasurementUnit] ADD  CONSTRAINT [DF_MeasurementUnit_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [Production].[MeasurementUnit] ADD  CONSTRAINT [DF_MeasurementUnit_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[MeasurementUnitGroup] ADD  CONSTRAINT [DF_MeasurementUnitGroup_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[MeasurementUnitGroup] ADD  CONSTRAINT [DF_MeasurementUnitGroup_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_IsFinishedGood]  DEFAULT ((0)) FOR [IsFinishedGood]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_IsSalable]  DEFAULT ((0)) FOR [IsSalable]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_IsUniqueBatchPerOrder]  DEFAULT ((1)) FOR [IsUniqueBatchPerOrder]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_SafetyStockLevel]  DEFAULT ((0)) FOR [SafetyStockLevel]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_ReorderPoint]  DEFAULT ((0)) FOR [ReorderPoint]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_StandardCost]  DEFAULT ((0)) FOR [StandardCost]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_ListPrice]  DEFAULT ((0)) FOR [ListPrice]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_OrderQuantity]  DEFAULT ((0)) FOR [OrderQuantity]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_DaysToManufacture]  DEFAULT ((0)) FOR [DaysToManufacture]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_DaysToExpire]  DEFAULT ((30)) FOR [DaysToExpire]
GO
ALTER TABLE [Production].[Product] ADD  CONSTRAINT [DF_Product_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[ProductCategory] ADD  CONSTRAINT [DF_ProductCategory_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[ProductCategory] ADD  CONSTRAINT [DF_ProductCategory_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[ProductCheckout] ADD  CONSTRAINT [DF_ProductCheckout_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[ProductCheckout] ADD  CONSTRAINT [DF_ProductCheckout_CheckOutDate]  DEFAULT (getdate()) FOR [CheckOutDate]
GO
ALTER TABLE [Production].[ProductCheckout] ADD  CONSTRAINT [DF_ProductCheckout_RevisionNumber]  DEFAULT ((0)) FOR [RevisionNumber]
GO
ALTER TABLE [Production].[ProductCheckout] ADD  CONSTRAINT [DF_ProductCheckout_ModifierDate]  DEFAULT (getdate()) FOR [ModifierDate]
GO
ALTER TABLE [Production].[ProductCheckOutLine] ADD  CONSTRAINT [DF_ProductCheckOutLine_LineId]  DEFAULT (newsequentialid()) FOR [InventoryId]
GO
ALTER TABLE [Production].[ProductCheckOutLine] ADD  CONSTRAINT [DF_ProductCheckOutLine_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[ProductInternalCheckIn] ADD  CONSTRAINT [DF_ProductReadyCheckIn_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[ProductInternalCheckIn] ADD  CONSTRAINT [DF_ProductReadyCheckIn_RevisionNumber]  DEFAULT ((0)) FOR [RevisionNumber]
GO
ALTER TABLE [Production].[ProductInternalCheckIn] ADD  CONSTRAINT [DF_ProductInternalCheckIn_ModifierDate]  DEFAULT (getdate()) FOR [ModifierDate]
GO
ALTER TABLE [Production].[ProductInternalCheckout] ADD  CONSTRAINT [DF_ProductInternalCheckout_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[ProductInternalCheckout] ADD  CONSTRAINT [DF_ProductInternalCheckout_CheckOutDate]  DEFAULT (getdate()) FOR [CheckOutDate]
GO
ALTER TABLE [Production].[ProductInternalCheckout] ADD  CONSTRAINT [DF_ProductInternalCheckout_RevisionNumber]  DEFAULT ((0)) FOR [RevisionNumber]
GO
ALTER TABLE [Production].[ProductInternalCheckout] ADD  CONSTRAINT [DF_ProductInternalCheckout_ModifierDate]  DEFAULT (getdate()) FOR [ModifierDate]
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine] ADD  CONSTRAINT [DF_ProductInternalCheckOutLine_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[ProductInventory] ADD  CONSTRAINT [DF_ProductInventory_Flag]  DEFAULT ((1)) FOR [Flag]
GO
ALTER TABLE [Production].[ProductInventory] ADD  CONSTRAINT [DF_ProductInventory_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[ProductInventory] ADD  CONSTRAINT [DF_ProductInventory_TotalBatches]  DEFAULT ((1)) FOR [TotalBatches]
GO
ALTER TABLE [Production].[ProductionOrder] ADD  CONSTRAINT [DF_ProductionOrder_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[ProductionOrder] ADD  CONSTRAINT [DF_ProductionOrder_OrderDate]  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [Production].[ProductionOrder] ADD  CONSTRAINT [DF_ProductionOrder_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[ProductionOrder] ADD  CONSTRAINT [DF_ProductionOrder_ScheduledDate]  DEFAULT (getdate()) FOR [ScheduledDate]
GO
ALTER TABLE [Production].[ProductionOrderLine] ADD  CONSTRAINT [DF_ProductionOrderLine_LineId]  DEFAULT (newsequentialid()) FOR [LineId]
GO
ALTER TABLE [Production].[ProductionOrderLine] ADD  CONSTRAINT [DF_ProductionOrderLine_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Production].[ProductionOrderLineDetail] ADD  CONSTRAINT [DF_ProductionOrderLineDetail_InventoryId]  DEFAULT (newsequentialid()) FOR [InventoryId]
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResource] ADD  CONSTRAINT [DF_ProductionOrderLineDetailResource_ResourceId]  DEFAULT (newsequentialid()) FOR [ResourceId]
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResource] ADD  CONSTRAINT [DF_ProductionOrderLineDetailResource_ModifierDate]  DEFAULT (getdate()) FOR [ModifierDate]
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResourceAllocation] ADD  CONSTRAINT [DF_ProductionOrderLineDetailResourceAllocation_ModifierDate]  DEFAULT (getdate()) FOR [ModifierDate]
GO
ALTER TABLE [Production].[ProductPhoto] ADD  CONSTRAINT [DF_ProductPhoto_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[RackingPallet] ADD  CONSTRAINT [DF_RackingPallet_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[StockAdjustMent] ADD  CONSTRAINT [DF_StockAdjustMent_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Production].[StockAdjustMent] ADD  CONSTRAINT [DF_StockAdjustMent_AdjustmentDate]  DEFAULT (getdate()) FOR [AdjustmentDate]
GO
ALTER TABLE [Production].[StockAdjustMent] ADD  CONSTRAINT [DF_StockAdjustMent_RevisionNumber]  DEFAULT ((0)) FOR [RevisionNumber]
GO
ALTER TABLE [Production].[StockAdjustMent] ADD  CONSTRAINT [DF_StockAdjustMent_ModifierDate]  DEFAULT (getdate()) FOR [ModifierDate]
GO
ALTER TABLE [Production].[StockAdjustmentLine] ADD  CONSTRAINT [DF_StockAdjustmentLine_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Purchasing].[ProductCheckIn] ADD  CONSTRAINT [DF_ProductCheckIn_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Purchasing].[ProductCheckIn] ADD  CONSTRAINT [DF_ProductCheckIn_CheckInDate]  DEFAULT (getdate()) FOR [CheckInDate]
GO
ALTER TABLE [Purchasing].[ProductCheckIn] ADD  CONSTRAINT [DF_ProductCheckIn_RevisionNumber]  DEFAULT ((0)) FOR [RevisionNumber]
GO
ALTER TABLE [Purchasing].[ProductCheckIn] ADD  CONSTRAINT [DF_ProductCheckIn_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Purchasing].[ProductCheckInLine] ADD  CONSTRAINT [DF_ProductCheckInLine_LineId]  DEFAULT (newsequentialid()) FOR [LineId]
GO
ALTER TABLE [Purchasing].[ProductCheckInLineDetail] ADD  CONSTRAINT [DF_ProductCheckInLineDetail_InventoryId]  DEFAULT (newsequentialid()) FOR [InventoryId]
GO
ALTER TABLE [Purchasing].[Supplier] ADD  CONSTRAINT [DF_Supplier_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [Purchasing].[Supplier] ADD  CONSTRAINT [DF_Supplier_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Sales].[Customer] ADD  CONSTRAINT [DF_Customer_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [Sales].[Customer] ADD  CONSTRAINT [DF_Customer_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Sales].[SalesOrder] ADD  CONSTRAINT [DF_SalesOrder_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [Sales].[SalesOrder] ADD  CONSTRAINT [DF_SalesOrder_OrderDate]  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [Sales].[SalesOrder] ADD  CONSTRAINT [DF_SalesOrder_Status]  DEFAULT ((7)) FOR [Status]
GO
ALTER TABLE [Sales].[SalesOrder] ADD  CONSTRAINT [DF_SalesOrder_RevisionNumber]  DEFAULT ((0)) FOR [RevisionNumber]
GO
ALTER TABLE [Sales].[SalesOrder] ADD  CONSTRAINT [DF_SalesOrder_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [Person].[BusinessEntityContact]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityContact_BusinessEntity] FOREIGN KEY([BusinessEntityId])
REFERENCES [Person].[BusinessEntity] ([Id])
GO
ALTER TABLE [Person].[BusinessEntityContact] CHECK CONSTRAINT [FK_BusinessEntityContact_BusinessEntity]
GO
ALTER TABLE [Person].[BusinessEntityContact]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityContact_ContactType] FOREIGN KEY([ContactTypeId])
REFERENCES [Person].[ContactType] ([Id])
GO
ALTER TABLE [Person].[BusinessEntityContact] CHECK CONSTRAINT [FK_BusinessEntityContact_ContactType]
GO
ALTER TABLE [Person].[BusinessEntityContact]  WITH CHECK ADD  CONSTRAINT [FK_BusinessEntityContact_Person] FOREIGN KEY([PersonId])
REFERENCES [Person].[Person] ([BusinessEntityId])
GO
ALTER TABLE [Person].[BusinessEntityContact] CHECK CONSTRAINT [FK_BusinessEntityContact_Person]
GO
ALTER TABLE [Person].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_BusinessEntity] FOREIGN KEY([BusinessEntityId])
REFERENCES [Person].[BusinessEntity] ([Id])
GO
ALTER TABLE [Person].[Person] CHECK CONSTRAINT [FK_Person_BusinessEntity]
GO
ALTER TABLE [Person].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_PersonType] FOREIGN KEY([PersonTypeId])
REFERENCES [Person].[PersonType] ([Id])
GO
ALTER TABLE [Person].[Person] CHECK CONSTRAINT [FK_Person_PersonType]
GO
ALTER TABLE [Production].[BillOfMaterials]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterials_MeasurementUnit] FOREIGN KEY([RawMaterialMeasurementUnitId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Production].[BillOfMaterials] CHECK CONSTRAINT [FK_BillOfMaterials_MeasurementUnit]
GO
ALTER TABLE [Production].[MeasurementUnit]  WITH CHECK ADD  CONSTRAINT [FK_MeasurementUnit_MeasurementUnit] FOREIGN KEY([ChildId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Production].[MeasurementUnit] CHECK CONSTRAINT [FK_MeasurementUnit_MeasurementUnit]
GO
ALTER TABLE [Production].[MeasurementUnit]  WITH CHECK ADD  CONSTRAINT [FK_MeasurementUnit_MeasurementUnitGroup] FOREIGN KEY([GroupId])
REFERENCES [Production].[MeasurementUnitGroup] ([Id])
GO
ALTER TABLE [Production].[MeasurementUnit] CHECK CONSTRAINT [FK_MeasurementUnit_MeasurementUnitGroup]
GO
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Location] FOREIGN KEY([LocationId])
REFERENCES [Production].[Location] ([Id])
GO
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [FK_Product_Location]
GO
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_MeasurementUnit] FOREIGN KEY([MeasurementUnitOrderId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [FK_Product_MeasurementUnit]
GO
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_MeasurementUnitGroup] FOREIGN KEY([MeasurementUnitGroupId])
REFERENCES [Production].[MeasurementUnitGroup] ([Id])
GO
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [FK_Product_MeasurementUnitGroup]
GO
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductCategory] FOREIGN KEY([CategoryId])
REFERENCES [Production].[ProductCategory] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [FK_Product_ProductCategory]
GO
ALTER TABLE [Production].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_RackingPallet] FOREIGN KEY([RackingPalletId])
REFERENCES [Production].[RackingPallet] ([Id])
GO
ALTER TABLE [Production].[Product] CHECK CONSTRAINT [FK_Product_RackingPallet]
GO
ALTER TABLE [Production].[ProductCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckOutLine_Location] FOREIGN KEY([LocationId])
REFERENCES [Production].[Location] ([Id])
GO
ALTER TABLE [Production].[ProductCheckOutLine] CHECK CONSTRAINT [FK_ProductCheckOutLine_Location]
GO
ALTER TABLE [Production].[ProductCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckOutLine_MeasurementUnit] FOREIGN KEY([MeasurementUnitId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Production].[ProductCheckOutLine] CHECK CONSTRAINT [FK_ProductCheckOutLine_MeasurementUnit]
GO
ALTER TABLE [Production].[ProductCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckOutLine_ProductCheckout] FOREIGN KEY([CheckOutId])
REFERENCES [Production].[ProductCheckout] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Production].[ProductCheckOutLine] CHECK CONSTRAINT [FK_ProductCheckOutLine_ProductCheckout]
GO
ALTER TABLE [Production].[ProductCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckOutLine_ProductInventory] FOREIGN KEY([InventoryId])
REFERENCES [Production].[ProductInventory] ([InventoryId])
GO
ALTER TABLE [Production].[ProductCheckOutLine] CHECK CONSTRAINT [FK_ProductCheckOutLine_ProductInventory]
GO
ALTER TABLE [Production].[ProductCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckOutLine_RackingPallet] FOREIGN KEY([RackingPalletId])
REFERENCES [Production].[RackingPallet] ([Id])
GO
ALTER TABLE [Production].[ProductCheckOutLine] CHECK CONSTRAINT [FK_ProductCheckOutLine_RackingPallet]
GO
ALTER TABLE [Production].[ProductCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckOutLine_SourceLocation] FOREIGN KEY([SourceLocationId])
REFERENCES [Production].[Location] ([Id])
GO
ALTER TABLE [Production].[ProductCheckOutLine] CHECK CONSTRAINT [FK_ProductCheckOutLine_SourceLocation]
GO
ALTER TABLE [Production].[ProductCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckOutLine_SourceRackingPallet] FOREIGN KEY([SourceRackingPalletId])
REFERENCES [Production].[RackingPallet] ([Id])
GO
ALTER TABLE [Production].[ProductCheckOutLine] CHECK CONSTRAINT [FK_ProductCheckOutLine_SourceRackingPallet]
GO
ALTER TABLE [Production].[ProductInternalCheckIn]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckIn_InventoryFlag] FOREIGN KEY([CheckInType])
REFERENCES [Production].[InventoryFlag] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckIn] CHECK CONSTRAINT [FK_ProductInternalCheckIn_InventoryFlag]
GO
ALTER TABLE [Production].[ProductInternalCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckInLine_Location] FOREIGN KEY([LocationId])
REFERENCES [Production].[Location] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckInLine] CHECK CONSTRAINT [FK_ProductInternalCheckInLine_Location]
GO
ALTER TABLE [Production].[ProductInternalCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckInLine_RackingPallet] FOREIGN KEY([RackingPalletId])
REFERENCES [Production].[RackingPallet] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckInLine] CHECK CONSTRAINT [FK_ProductInternalCheckInLine_RackingPallet]
GO
ALTER TABLE [Production].[ProductInternalCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckInLine_SourceLocation] FOREIGN KEY([SourceLocationId])
REFERENCES [Production].[Location] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckInLine] CHECK CONSTRAINT [FK_ProductInternalCheckInLine_SourceLocation]
GO
ALTER TABLE [Production].[ProductInternalCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckInLine_SourceRackingPallet] FOREIGN KEY([SourceRackingPalletId])
REFERENCES [Production].[RackingPallet] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckInLine] CHECK CONSTRAINT [FK_ProductInternalCheckInLine_SourceRackingPallet]
GO
ALTER TABLE [Production].[ProductInternalCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductReadyCheckInLine_MeasurementUnit] FOREIGN KEY([MeasurementUnitId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckInLine] CHECK CONSTRAINT [FK_ProductReadyCheckInLine_MeasurementUnit]
GO
ALTER TABLE [Production].[ProductInternalCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductReadyCheckInLine_ProductInventory] FOREIGN KEY([InventoryId])
REFERENCES [Production].[ProductInventory] ([InventoryId])
GO
ALTER TABLE [Production].[ProductInternalCheckInLine] CHECK CONSTRAINT [FK_ProductReadyCheckInLine_ProductInventory]
GO
ALTER TABLE [Production].[ProductInternalCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductReadyCheckInLine_ProductReadyCheckIn] FOREIGN KEY([CheckInId])
REFERENCES [Production].[ProductInternalCheckIn] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckInLine] CHECK CONSTRAINT [FK_ProductReadyCheckInLine_ProductReadyCheckIn]
GO
ALTER TABLE [Production].[ProductInternalCheckout]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckout_InventoryFlag] FOREIGN KEY([CheckOutType])
REFERENCES [Production].[InventoryFlag] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckout] CHECK CONSTRAINT [FK_ProductInternalCheckout_InventoryFlag]
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckOutLine_Location] FOREIGN KEY([LocationId])
REFERENCES [Production].[Location] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine] CHECK CONSTRAINT [FK_ProductInternalCheckOutLine_Location]
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckOutLine_MeasurementUnit] FOREIGN KEY([MeasurementUnitId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine] CHECK CONSTRAINT [FK_ProductInternalCheckOutLine_MeasurementUnit]
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckOutLine_ProductInternalCheckout] FOREIGN KEY([CheckOutId])
REFERENCES [Production].[ProductInternalCheckout] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine] CHECK CONSTRAINT [FK_ProductInternalCheckOutLine_ProductInternalCheckout]
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckOutLine_ProductInventory] FOREIGN KEY([InventoryId])
REFERENCES [Production].[ProductInventory] ([InventoryId])
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine] CHECK CONSTRAINT [FK_ProductInternalCheckOutLine_ProductInventory]
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckOutLine_RackingPallet] FOREIGN KEY([RackingPalletId])
REFERENCES [Production].[RackingPallet] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine] CHECK CONSTRAINT [FK_ProductInternalCheckOutLine_RackingPallet]
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckOutLine_SourceLocation] FOREIGN KEY([SourceLocationId])
REFERENCES [Production].[Location] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine] CHECK CONSTRAINT [FK_ProductInternalCheckOutLine_SourceLocation]
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductInternalCheckOutLine_SourceRackingPallet] FOREIGN KEY([SourceRackingPalletId])
REFERENCES [Production].[RackingPallet] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckOutLine] CHECK CONSTRAINT [FK_ProductInternalCheckOutLine_SourceRackingPallet]
GO
ALTER TABLE [Production].[ProductInventory]  WITH CHECK ADD  CONSTRAINT [FK_ProductInventory_InventoryFlag] FOREIGN KEY([Flag])
REFERENCES [Production].[InventoryFlag] ([Id])
GO
ALTER TABLE [Production].[ProductInventory] CHECK CONSTRAINT [FK_ProductInventory_InventoryFlag]
GO
ALTER TABLE [Production].[ProductInventory]  WITH CHECK ADD  CONSTRAINT [FK_ProductInventory_Location] FOREIGN KEY([LocationId])
REFERENCES [Production].[Location] ([Id])
GO
ALTER TABLE [Production].[ProductInventory] CHECK CONSTRAINT [FK_ProductInventory_Location]
GO
ALTER TABLE [Production].[ProductInventory]  WITH CHECK ADD  CONSTRAINT [FK_ProductInventory_MeasurementUnit] FOREIGN KEY([MeasurementUnitId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Production].[ProductInventory] CHECK CONSTRAINT [FK_ProductInventory_MeasurementUnit]
GO
ALTER TABLE [Production].[ProductInventory]  WITH CHECK ADD  CONSTRAINT [FK_ProductInventory_Product] FOREIGN KEY([ProductId])
REFERENCES [Production].[Product] ([Id])
GO
ALTER TABLE [Production].[ProductInventory] CHECK CONSTRAINT [FK_ProductInventory_Product]
GO
ALTER TABLE [Production].[ProductInventory]  WITH CHECK ADD  CONSTRAINT [FK_ProductInventory_RackingPallet] FOREIGN KEY([RackingPalletId])
REFERENCES [Production].[RackingPallet] ([Id])
GO
ALTER TABLE [Production].[ProductInventory] CHECK CONSTRAINT [FK_ProductInventory_RackingPallet]
GO
ALTER TABLE [Production].[ProductionOrderLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderLine_MeasurementUnit] FOREIGN KEY([MeasurementUnitId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Production].[ProductionOrderLine] CHECK CONSTRAINT [FK_ProductionOrderLine_MeasurementUnit]
GO
ALTER TABLE [Production].[ProductionOrderLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderLine_Product] FOREIGN KEY([ProductId])
REFERENCES [Production].[Product] ([Id])
GO
ALTER TABLE [Production].[ProductionOrderLine] CHECK CONSTRAINT [FK_ProductionOrderLine_Product]
GO
ALTER TABLE [Production].[ProductionOrderLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderLine_ProductionOrder] FOREIGN KEY([ProductionOrderId])
REFERENCES [Production].[ProductionOrder] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Production].[ProductionOrderLine] CHECK CONSTRAINT [FK_ProductionOrderLine_ProductionOrder]
GO
ALTER TABLE [Production].[ProductionOrderLineDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderLineDetail_ProductionOrderLine] FOREIGN KEY([LineId])
REFERENCES [Production].[ProductionOrderLine] ([LineId])
ON DELETE CASCADE
GO
ALTER TABLE [Production].[ProductionOrderLineDetail] CHECK CONSTRAINT [FK_ProductionOrderLineDetail_ProductionOrderLine]
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResource]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderLineDetailResource_MeasurementUnit] FOREIGN KEY([MeasurementUnitId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResource] CHECK CONSTRAINT [FK_ProductionOrderLineDetailResource_MeasurementUnit]
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResource]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderLineDetailResource_Product] FOREIGN KEY([RawMaterialId])
REFERENCES [Production].[Product] ([Id])
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResource] CHECK CONSTRAINT [FK_ProductionOrderLineDetailResource_Product]
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResource]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderLineDetailResource_ProductionOrderLineDetail] FOREIGN KEY([InventoryId])
REFERENCES [Production].[ProductionOrderLineDetail] ([InventoryId])
ON DELETE CASCADE
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResource] CHECK CONSTRAINT [FK_ProductionOrderLineDetailResource_ProductionOrderLineDetail]
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResourceAllocation]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderLineDetailResourceAllocation_MeasurementUnit] FOREIGN KEY([MeasurementUnitId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResourceAllocation] CHECK CONSTRAINT [FK_ProductionOrderLineDetailResourceAllocation_MeasurementUnit]
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResourceAllocation]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderLineDetailResourceAllocation_ProductInventory] FOREIGN KEY([InventoryId])
REFERENCES [Production].[ProductInventory] ([InventoryId])
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResourceAllocation] CHECK CONSTRAINT [FK_ProductionOrderLineDetailResourceAllocation_ProductInventory]
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResourceAllocation]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderLineDetailResourceAllocation_ProductionOrderLineDetailResource] FOREIGN KEY([ResourceId])
REFERENCES [Production].[ProductionOrderLineDetailResource] ([ResourceId])
ON DELETE CASCADE
GO
ALTER TABLE [Production].[ProductionOrderLineDetailResourceAllocation] CHECK CONSTRAINT [FK_ProductionOrderLineDetailResourceAllocation_ProductionOrderLineDetailResource]
GO
ALTER TABLE [Production].[ProductNameAndDescriptionCulture]  WITH CHECK ADD  CONSTRAINT [FK_ProductNameAndDescriptionCulture_Culture] FOREIGN KEY([CultureId])
REFERENCES [Production].[Culture] ([Id])
GO
ALTER TABLE [Production].[ProductNameAndDescriptionCulture] CHECK CONSTRAINT [FK_ProductNameAndDescriptionCulture_Culture]
GO
ALTER TABLE [Production].[ProductNameAndDescriptionCulture]  WITH CHECK ADD  CONSTRAINT [FK_ProductNameAndDescriptionCulture_Product] FOREIGN KEY([ProductId])
REFERENCES [Production].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Production].[ProductNameAndDescriptionCulture] CHECK CONSTRAINT [FK_ProductNameAndDescriptionCulture_Product]
GO
ALTER TABLE [Production].[ProductProductPhoto]  WITH CHECK ADD  CONSTRAINT [FK_ProductProductPhoto_Product] FOREIGN KEY([ProductId])
REFERENCES [Production].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Production].[ProductProductPhoto] CHECK CONSTRAINT [FK_ProductProductPhoto_Product]
GO
ALTER TABLE [Production].[ProductProductPhoto]  WITH CHECK ADD  CONSTRAINT [FK_ProductProductPhoto_ProductPhoto] FOREIGN KEY([ProductPhoto])
REFERENCES [Production].[ProductPhoto] ([Id])
GO
ALTER TABLE [Production].[ProductProductPhoto] CHECK CONSTRAINT [FK_ProductProductPhoto_ProductPhoto]
GO
ALTER TABLE [Production].[StockAdjustmentLine]  WITH CHECK ADD  CONSTRAINT [FK_StockAdjustmentLine_DiscrepantReason] FOREIGN KEY([ReasonId])
REFERENCES [Production].[DiscrepantReason] ([Id])
GO
ALTER TABLE [Production].[StockAdjustmentLine] CHECK CONSTRAINT [FK_StockAdjustmentLine_DiscrepantReason]
GO
ALTER TABLE [Production].[StockAdjustmentLine]  WITH CHECK ADD  CONSTRAINT [FK_StockAdjustmentLine_MeasurementUnit] FOREIGN KEY([MeasurementUnitId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Production].[StockAdjustmentLine] CHECK CONSTRAINT [FK_StockAdjustmentLine_MeasurementUnit]
GO
ALTER TABLE [Production].[StockAdjustmentLine]  WITH CHECK ADD  CONSTRAINT [FK_StockAdjustmentLine_ProductInventory] FOREIGN KEY([InventoryId])
REFERENCES [Production].[ProductInventory] ([InventoryId])
GO
ALTER TABLE [Production].[StockAdjustmentLine] CHECK CONSTRAINT [FK_StockAdjustmentLine_ProductInventory]
GO
ALTER TABLE [Production].[StockAdjustmentLine]  WITH CHECK ADD  CONSTRAINT [FK_StockAdjustmentLine_StockAdjustMent] FOREIGN KEY([AdjustmentId])
REFERENCES [Production].[StockAdjustMent] ([Id])
GO
ALTER TABLE [Production].[StockAdjustmentLine] CHECK CONSTRAINT [FK_StockAdjustmentLine_StockAdjustMent]
GO
ALTER TABLE [Purchasing].[ProductCheckIn]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckIn_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [Purchasing].[Supplier] ([BusinessEntityId])
GO
ALTER TABLE [Purchasing].[ProductCheckIn] CHECK CONSTRAINT [FK_ProductCheckIn_Supplier]
GO
ALTER TABLE [Purchasing].[ProductCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckInLine_Location] FOREIGN KEY([LocationId])
REFERENCES [Production].[Location] ([Id])
GO
ALTER TABLE [Purchasing].[ProductCheckInLine] CHECK CONSTRAINT [FK_ProductCheckInLine_Location]
GO
ALTER TABLE [Purchasing].[ProductCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckInLine_MeasurementUnit] FOREIGN KEY([MeasurementUnitId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Purchasing].[ProductCheckInLine] CHECK CONSTRAINT [FK_ProductCheckInLine_MeasurementUnit]
GO
ALTER TABLE [Purchasing].[ProductCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckInLine_Product] FOREIGN KEY([ProductId])
REFERENCES [Production].[Product] ([Id])
GO
ALTER TABLE [Purchasing].[ProductCheckInLine] CHECK CONSTRAINT [FK_ProductCheckInLine_Product]
GO
ALTER TABLE [Purchasing].[ProductCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckInLine_ProductCheckIn] FOREIGN KEY([CheckInId])
REFERENCES [Purchasing].[ProductCheckIn] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Purchasing].[ProductCheckInLine] CHECK CONSTRAINT [FK_ProductCheckInLine_ProductCheckIn]
GO
ALTER TABLE [Purchasing].[ProductCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckInLine_RackingPallet] FOREIGN KEY([RackingPalletId])
REFERENCES [Production].[RackingPallet] ([Id])
GO
ALTER TABLE [Purchasing].[ProductCheckInLine] CHECK CONSTRAINT [FK_ProductCheckInLine_RackingPallet]
GO
ALTER TABLE [Purchasing].[ProductCheckInLineDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductCheckInLineDetail_ProductCheckInLine] FOREIGN KEY([LineId])
REFERENCES [Purchasing].[ProductCheckInLine] ([LineId])
ON DELETE CASCADE
GO
ALTER TABLE [Purchasing].[ProductCheckInLineDetail] CHECK CONSTRAINT [FK_ProductCheckInLineDetail_ProductCheckInLine]
GO
ALTER TABLE [Purchasing].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_BusinessEntity] FOREIGN KEY([BusinessEntityId])
REFERENCES [Person].[BusinessEntity] ([Id])
GO
ALTER TABLE [Purchasing].[Supplier] CHECK CONSTRAINT [FK_Supplier_BusinessEntity]
GO
ALTER TABLE [Sales].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_BusinessEntity] FOREIGN KEY([BusinessEntityId])
REFERENCES [Person].[BusinessEntity] ([Id])
GO
ALTER TABLE [Sales].[Customer] CHECK CONSTRAINT [FK_Customer_BusinessEntity]
GO
ALTER TABLE [Sales].[SalesOrder]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrder_Customer] FOREIGN KEY([CustomerId])
REFERENCES [Sales].[Customer] ([BusinessEntityId])
GO
ALTER TABLE [Sales].[SalesOrder] CHECK CONSTRAINT [FK_SalesOrder_Customer]
GO
ALTER TABLE [Sales].[SalesOrderLine]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLine_MeasurementUnit] FOREIGN KEY([MeasurementUnitId])
REFERENCES [Production].[MeasurementUnit] ([Id])
GO
ALTER TABLE [Sales].[SalesOrderLine] CHECK CONSTRAINT [FK_SalesOrderLine_MeasurementUnit]
GO
ALTER TABLE [Sales].[SalesOrderLine]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLine_ProductInventory] FOREIGN KEY([InventoryId])
REFERENCES [Production].[ProductInventory] ([InventoryId])
GO
ALTER TABLE [Sales].[SalesOrderLine] CHECK CONSTRAINT [FK_SalesOrderLine_ProductInventory]
GO
ALTER TABLE [Sales].[SalesOrderLine]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLine_SalesOrder] FOREIGN KEY([OrderId])
REFERENCES [Sales].[SalesOrder] ([Id])
GO
ALTER TABLE [Sales].[SalesOrderLine] CHECK CONSTRAINT [FK_SalesOrderLine_SalesOrder]
GO
/****** Object:  StoredProcedure [Production].[BomSpecification]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Production].[DistributeRawMaterialsCheckout]    Script Date: 6/7/2024 7:24:21 PM ******/
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
	update Production.ProductInventory set Quantity=@chkQuantity where InventoryId=@chkInventoryId

	close c1
	deallocate c1
		
		
			
	fetch next from c into @chkInventoryId, @chkQuantity, @chkRawMaterialId, @chkMeasurementUnitId 
end

close c
deallocate c

GO
/****** Object:  StoredProcedure [Production].[GetDispatchableProducts]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Production].[GetLocationById]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailByDate]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Production].[GetProductionOrderDetailById]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Production].[GetRackingPalletById]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Production].[GetRawMaterialsBasicInfo]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Production].[GetRawMaterialsForProduction]    Script Date: 6/7/2024 7:24:21 PM ******/
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
	inv.Flag in (1,3) AND p.IsFinishedGood = 0
GO
/****** Object:  StoredProcedure [Production].[GetRunningProductionItems]    Script Date: 6/7/2024 7:24:21 PM ******/
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
left outer JOIN 
	Production.Location loc 
	ON
		inv.LocationId = loc.Id 
left outer JOIN 
	Production.RackingPallet rack 
	ON
		inv.RackingPalletId = rack.Id 
WHERE 
	inv.Flag=6 AND p.IsFinishedGood = 1
GO
/****** Object:  StoredProcedure [Production].[LocationList]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Production].[LocationList]
as
Select Id, [Name] from production.[Location]
GO
/****** Object:  StoredProcedure [Production].[ProductCategoryList]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [Production].[ProductCategoryList]
as
select Id, Name from Production.ProductCategory
GO
/****** Object:  StoredProcedure [Production].[ProductList]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Production].[RackingPalletList]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Production].[RackingPalletList]
as
Select Id, Col, Row, [Description], Col+'-'+cast(Row as nvarchar(5)) as ColRow from production.RackingPallet
GO
/****** Object:  StoredProcedure [Purchasing].[GetCheckinRawMaterials]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Purchasing].[GetEndProducts]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Purchasing].[GetSupplierById]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Purchasing].[RawMaterialsForCheckin]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Purchasing].[SupplierList]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Purchasing].[SupplierList]
as
Select BusinessEntityId, [Name], IsActive from Purchasing.Supplier
GO
/****** Object:  StoredProcedure [Sales].[CustomerList]    Script Date: 6/7/2024 7:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [Sales].[CustomerList]
as
Select BusinessEntityId, [Name], IsActive from Sales.Customer
GO
/****** Object:  StoredProcedure [Sales].[GetCustomerById]    Script Date: 6/7/2024 7:24:21 PM ******/
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
/****** Object:  StoredProcedure [Sales].[GetDispatchOrderDetailByDate]    Script Date: 6/7/2024 7:24:21 PM ******/
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
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Raw Materials check out for production processing' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductCheckout'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductInternalCheckIn', @level2type=N'COLUMN',@level2name=N'RevisionNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Products check-in from production or movement between different location or racking pallets' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductInternalCheckIn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Finished Product CheckIn-Type: 0: New finished product 1: Move between locations (e.g from production to warehouse facilitiy)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductInternalCheckInLine', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1. CheckIn (purchase) 2. CheckOut for Production 3. Return from Production 4. Waiting for Production 5. In production 6. Check-In from production 7. New Delivery Order 8. Packing 9. Packed 10. Dispatched 11. Move location' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductInventory', @level2type=N'COLUMN',@level2name=N'Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'7. New Delivery Order 8. Packing 9. Packed 10. Dispatched' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrder', @level2type=N'COLUMN',@level2name=N'Status'
GO
USE [master]
GO
ALTER DATABASE [IntrManDB] SET  READ_WRITE 
GO
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (1, N'Checkin (Purchased)')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (2, N'Checkout for Production')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (3, N'Return from Production')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (4, N'Production - Aborted')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (5, N'Production - Not Started')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (6, N'Production - In Progress')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (7, N'Production - Completed')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (8, N'Checkin from Production')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (9, N'New Delivery Order')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (10, N'Packing for delivery')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (11, N'Packed for delivery')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (12, N'Dispatched')
INSERT [Production].[InventoryFlag] ([Id], [Name]) VALUES (13, N'Move location')
go