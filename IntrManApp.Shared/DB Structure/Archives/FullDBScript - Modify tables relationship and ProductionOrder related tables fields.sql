Use Master 
go
if exists(select * from sys.databases where name = 'Gha2zERPDB') drop database Gha2zERPDB
go
CREATE DATABASE [Gha2zERPDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Gha2zERPDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BARTENDER\MSSQL\DATA\Gha2zERPDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Gha2zERPDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BARTENDER\MSSQL\DATA\Gha2zERPDB_log.ldf' , SIZE = 3840KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Gha2zERPDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Gha2zERPDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Gha2zERPDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Gha2zERPDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Gha2zERPDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Gha2zERPDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Gha2zERPDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET RECOVERY FULL 
GO
ALTER DATABASE [Gha2zERPDB] SET  MULTI_USER 
GO
ALTER DATABASE [Gha2zERPDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Gha2zERPDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Gha2zERPDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Gha2zERPDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Gha2zERPDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Gha2zERPDB', N'ON'
GO
USE [Gha2zERPDB]
GO
/****** Object:  Schema [Person]    Script Date: 4/12/2024 9:16:14 AM ******/
CREATE SCHEMA [Person]
GO
/****** Object:  Schema [Production]    Script Date: 4/12/2024 9:16:14 AM ******/
CREATE SCHEMA [Production]
GO
/****** Object:  Schema [Purchasing]    Script Date: 4/12/2024 9:16:14 AM ******/
CREATE SCHEMA [Purchasing]
GO
/****** Object:  Schema [Sales]    Script Date: 4/12/2024 9:16:14 AM ******/
CREATE SCHEMA [Sales]
GO
/****** Object:  Table [Person].[BusinessEntity]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Person].[BusinessEntityContact]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Person].[ContactType]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Person].[Person]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Person].[PersonType]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[BillOfMaterials]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[Culture]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[DiscrepantReason]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[Location]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[MeasurementUnit]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[MeasurementUnitGroup]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[Product]    Script Date: 4/12/2024 9:16:14 AM ******/
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
	[ProductRackingPalletCol] [nvarchar](5) NULL,
	[ProductRackingPalletRow] [smallint] NULL,
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
/****** Object:  Table [Production].[ProductCategory]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[ProductCheckout]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[ProductCheckOutLine]    Script Date: 4/12/2024 9:16:14 AM ******/
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
	[RackingPalletCol] [nvarchar](5) NULL,
	[RackingPalletRow] [smallint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProductCheckOutLine] PRIMARY KEY CLUSTERED 
(
	[CheckOutId] ASC,
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductInternalCheckIn]    Script Date: 4/12/2024 9:16:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductInternalCheckIn](
	[Id] [uniqueidentifier] NOT NULL,
	[CheckInDate] [datetime] NULL,
	[RevisionNumber] [tinyint] NULL,
	[ModifierDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReadyCheckIn] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductInternalCheckInLine]    Script Date: 4/12/2024 9:16:14 AM ******/
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
	[RackingPalletCol] [nvarchar](5) NULL,
	[RackingPalletRow] [smallint] NULL,
	[CheckinType] [tinyint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReadyCheckInLine] PRIMARY KEY CLUSTERED 
(
	[CheckInId] ASC,
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductInternalCheckout]    Script Date: 4/12/2024 9:16:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductInternalCheckout](
	[Id] [uniqueidentifier] NOT NULL,
	[CheckOutDate] [datetime] NULL,
	[RevisionNumber] [tinyint] NULL,
	[ModifierDate] [datetime] NULL,
 CONSTRAINT [PK_ProductInternalCheckout] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductInternalCheckOutLine]    Script Date: 4/12/2024 9:16:14 AM ******/
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
	[RackingPalletCol] [nvarchar](5) NULL,
	[RackingPalletRow] [smallint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProductInternalCheckOutLine] PRIMARY KEY CLUSTERED 
(
	[CheckOutId] ASC,
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductInventory]    Script Date: 4/12/2024 9:16:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductInventory](
	[InventoryId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
	[LocationId] [uniqueidentifier] NULL,
	[RackingPalletCol] [nvarchar](5) NULL,
	[RackingPalletRow] [smallint] NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[ProductionDate] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
	[Flag] [tinyint] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProductInventory] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrder]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[ProductionOrderLine]    Script Date: 4/12/2024 9:16:14 AM ******/
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
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_ProductionOrderLine] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrderLineDetail]    Script Date: 4/12/2024 9:16:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrderLineDetail](
	[LineId] [uniqueidentifier] NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_ProductionOrderLineDetail] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrderLineDetailResource]    Script Date: 4/12/2024 9:16:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrderLineDetailResource](
	[InventoryId] [uniqueidentifier] NULL,
	[RawMaterialId] [uniqueidentifier] NULL,
	[ResourceId] [uniqueidentifier] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[ModifierDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductionOrderLineDetailResource] PRIMARY KEY CLUSTERED 
(
	[ResourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrderLineDetailResourceAllocation]    Script Date: 4/12/2024 9:16:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrderLineDetailResourceAllocation](
	[ResourceId] [uniqueidentifier] NULL,
	[ProductCheckoutId] [uniqueidentifier] NULL,
	[RawMaterialId] [uniqueidentifier] NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[ModifierDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductNameAndDescriptionCulture]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[ProductPhoto]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[ProductProductPhoto]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[StockAdjustMent]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Production].[StockAdjustmentLine]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Purchasing].[ProductCheckIn]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Purchasing].[ProductCheckInLine]    Script Date: 4/12/2024 9:16:14 AM ******/
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
	[RackingPalletCol] [nvarchar](5) NULL,
	[RackingPalletRow] [smallint] NULL,
 CONSTRAINT [PK_ProductCheckInLine_1] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Purchasing].[ProductCheckInLineDetail]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Purchasing].[Supplier]    Script Date: 4/12/2024 9:16:14 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[Customer]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Sales].[SalesOrder]    Script Date: 4/12/2024 9:16:14 AM ******/
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
/****** Object:  Table [Sales].[SalesOrderLine]    Script Date: 4/12/2024 9:16:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[SalesOrderLine](
	[OrderId] [uniqueidentifier] NOT NULL,
	[InventoryId] [uniqueidentifier] NOT NULL,
	[MeasurementUnitId] [uniqueidentifier] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[LocationId] [uniqueidentifier] NULL,
	[RackingPalletCol] [nvarchar](25) NULL,
	[RackingPalletRow] [smallint] NULL,
 CONSTRAINT [PK_SalesOrderLine] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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
ALTER TABLE [Production].[BillOfMaterials]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterials_Product] FOREIGN KEY([ProductId])
REFERENCES [Production].[Product] ([Id])
GO
ALTER TABLE [Production].[BillOfMaterials] CHECK CONSTRAINT [FK_BillOfMaterials_Product]
GO
ALTER TABLE [Production].[BillOfMaterials]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterials_Product1] FOREIGN KEY([RawMaterialId])
REFERENCES [Production].[Product] ([Id])
GO
ALTER TABLE [Production].[BillOfMaterials] CHECK CONSTRAINT [FK_BillOfMaterials_Product1]
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
ALTER TABLE [Production].[ProductInternalCheckInLine]  WITH CHECK ADD  CONSTRAINT [FK_ProductReadyCheckInLine_Location] FOREIGN KEY([LocationId])
REFERENCES [Production].[Location] ([Id])
GO
ALTER TABLE [Production].[ProductInternalCheckInLine] CHECK CONSTRAINT [FK_ProductReadyCheckInLine_Location]
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
ALTER TABLE [Production].[ProductionOrderLineDetailResourceAllocation]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderLineDetailResourceAllocation_ProductInventory] FOREIGN KEY([RawMaterialId])
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
ALTER TABLE [Sales].[SalesOrderLine]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderLine_Location] FOREIGN KEY([LocationId])
REFERENCES [Production].[Location] ([Id])
GO
ALTER TABLE [Sales].[SalesOrderLine] CHECK CONSTRAINT [FK_SalesOrderLine_Location]
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
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Raw Materials check out for production processing' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductCheckout'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Finished Product CheckIn-Type: 0: New finished product 1: Move between locations (e.g from production to warehouse facilitiy)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductInternalCheckInLine', @level2type=N'COLUMN',@level2name=N'ModifiedDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1. CheckIn (purchase) 2. CheckOut for Production 3. Return from Production 4. Waiting for Production 5. In production 6. Check-In from production 7. New Delivery Order 8. Packing 9. Packed 10. Dispatched' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductInventory', @level2type=N'COLUMN',@level2name=N'Flag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'7. New Delivery Order 8. Packing 9. Packed 10. Dispatched' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'SalesOrder', @level2type=N'COLUMN',@level2name=N'Status'
GO
