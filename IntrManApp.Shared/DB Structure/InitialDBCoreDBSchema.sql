/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2022 (16.0.1110)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2014
    Target Database Engine Edition : Microsoft SQL Server Express Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [master]
GO
/****** Object:  Database [IntrManDB]    Script Date: 3/26/2024 2:31:51 PM ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'IntrManDB')
DROP DATABASE [IntrManDB]
GO
/****** Object:  Database [IntrManDB]    Script Date: 3/26/2024 2:31:51 PM ******/
CREATE DATABASE [IntrManDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IntrManDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BARTENDER\MSSQL\DATA\IntrManDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'IntrManDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BARTENDER\MSSQL\DATA\IntrManDB_log.ldf' , SIZE = 3136KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
/****** Object:  Schema [Person]    Script Date: 3/26/2024 2:31:51 PM ******/
CREATE SCHEMA [Person]
GO
/****** Object:  Schema [Production]    Script Date: 3/26/2024 2:31:51 PM ******/
CREATE SCHEMA [Production]
GO
/****** Object:  Schema [Purchasing]    Script Date: 3/26/2024 2:31:51 PM ******/
CREATE SCHEMA [Purchasing]
GO
/****** Object:  Schema [Sales]    Script Date: 3/26/2024 2:31:51 PM ******/
CREATE SCHEMA [Sales]
GO
/****** Object:  Table [Person].[BusinessEntity]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Person].[BusinessEntity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BusinessEntity_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Person].[BusinessEntityContact]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Person].[BusinessEntityContact](
	[BusinessEntityId] [int] NOT NULL,
	[PersonId] [int] NOT NULL,
	[ContactTypeId] [int] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BusinessEntityContact] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityId] ASC,
	[PersonId] ASC,
	[ContactTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Person].[ContactType]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Person].[ContactType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ContactType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Person].[Person]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Person].[Person](
	[BusinessEntityId] [int] NOT NULL,
	[PersonType] [smallint] NOT NULL,
	[Title] [nvarchar](8) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Suffix] [nvarchar](10) NOT NULL,
	[AdditionalContactInfo] [xml] NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Person_BusinessEntityId] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Production].[BillOfMaterials]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[BillOfMaterials](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[RawMaterialID] [int] NOT NULL,
	[RawMaterialUnitMeasureId] [int] NOT NULL,
	[RawMaterialQty] [decimal](8, 2) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BillOfMaterials_Id] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[Culture]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[Culture](
	[Id] [nchar](6) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [Pk_Culture_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[DiscrepantReason]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[DiscrepantReason](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Reason] [nvarchar](100) NOT NULL,
 CONSTRAINT [Pk_DiscrepantReason_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[Location]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductLocation_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[Product]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductNumber] [nvarchar](25) NOT NULL,
	[IsFinishedGood] [bit] NOT NULL,
	[IsSalable] [bit] NOT NULL,
	[IsUniqueBatchPerOrder] [bit] NOT NULL,
	[SafetyStockLevel] [decimal](18, 2) NOT NULL,
	[ReorderPoint] [decimal](18, 2) NOT NULL,
	[StandardCost] [money] NOT NULL,
	[ListPrice] [money] NOT NULL,
	[UnitMeasureGroupId] [int] NOT NULL,
	[OrderUnitMeasureId] [int] NOT NULL,
	[OrderQuantity] [decimal](8, 2) NULL,
	[DaysToManufacture] [int] NOT NULL,
	[DaysToExpire] [int] NOT NULL,
	[ProductSubcategoryID] [int] NULL,
	[ProductLocationID] [int] NULL,
	[ProductRackingPalletCol] [nvarchar](5) NULL,
	[ProductRackingPalletRow] [smallint] NULL,
	[AdditionalInfo] [xml] NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductCategory]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductCategory_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductCheckout]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductCheckout](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CheckOutDate] [datetime] NOT NULL,
	[RevisionNumber] [tinyint] NOT NULL,
	[ModifierDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductCheckOut_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductCheckOutLine]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductCheckOutLine](
	[CheckOutId] [int] NOT NULL,
	[LineId] [int] IDENTITY(1,1) NOT NULL,
	[rowcol] [uniqueidentifier] NULL,
	[ProductId] [int] NOT NULL,
	[TotalBatches] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[RackingPalletCol] [nvarchar](5) NULL,
	[RackingPalletRow] [smallint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductCheckOutLine_Id] PRIMARY KEY CLUSTERED 
(
	[CheckOutId] ASC,
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductCheckOutLineDetail]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductCheckOutLineDetail](
	[rowguid] [uniqueidentifier] NOT NULL,
	[LineId] [uniqueidentifier] NOT NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductCheckOutLineDetail_Id] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC,
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrder]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ScheduledDate] [datetime] NULL,
 CONSTRAINT [PK_ProductionOrder_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrderLine]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrderLine](
	[ProductionOrderId] [int] NOT NULL,
	[LineId] [int] IDENTITY(1,1) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NULL,
	[ProductID] [int] NOT NULL,
	[TotalBatches] [int] NOT NULL,
	[QuantityPerBatch] [decimal](18, 2) NOT NULL,
	[UnitMeasureId] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[TotalBatchesCompleted] [int] NULL,
	[TotalBatchesScrapped] [int] NULL,
 CONSTRAINT [PK_ProductionOrderLine] PRIMARY KEY CLUSTERED 
(
	[ProductionOrderId] ASC,
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrderLineDetail]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrderLineDetail](
	[LineId] [int] NOT NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
	[rowguid] [uniqueidentifier] NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductionOrderLineDetail_1] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC,
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrderLineDetailResource]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrderLineDetailResource](
	[LineId] [int] NOT NULL,
	[RawMaterialId] [int] NOT NULL,
	[rowguid] [uniqueidentifier] NULL,
	[ResourceId] [bigint] IDENTITY(1,1) NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UnitMeasureId] [int] NOT NULL,
	[ModifierDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductionOrderLineDetailSource_Id] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC,
	[RawMaterialId] ASC,
	[ResourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductionOrderLineDetailResourceAllocation]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductionOrderLineDetailResourceAllocation](
	[ResourceId] [int] NOT NULL,
	[ProductCheckoutId] [int] NOT NULL,
	[RawMaterialId] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UnitMeasureId] [int] NOT NULL,
	[ModifierDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductionOrderLineDetailSourceAllocation_Id] PRIMARY KEY CLUSTERED 
(
	[ResourceId] ASC,
	[RawMaterialId] ASC,
	[ProductCheckoutId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductNameAndDescriptionCulture]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductNameAndDescriptionCulture](
	[ProductId] [int] NOT NULL,
	[CultureId] [nchar](6) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_ProductNameAndDecription_ProductId_CultureId] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[CultureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductPhoto]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductPhoto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ThumbNailPhoto] [varbinary](max) NULL,
	[ThumbnailPhotoFileName] [nvarchar](50) NULL,
	[LargePhoto] [varbinary](max) NULL,
	[LargePhotoFileName] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductPhoto_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductProductPhoto]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductProductPhoto](
	[ProductID] [int] NOT NULL,
	[ProductPhotoID] [int] NOT NULL,
	[Primary] [bit] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductProductPhoto_ProductID_ProductPhotoID] PRIMARY KEY NONCLUSTERED 
(
	[ProductID] ASC,
	[ProductPhotoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductReadyCheckIn]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductReadyCheckIn](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CheckInDate] [datetime] NOT NULL,
	[RevisionNumber] [tinyint] NOT NULL,
	[ModifierDate] [datetime] NOT NULL,
	[CheckInType] [tinyint] NULL,
 CONSTRAINT [PK_ProductReadyCheckIn_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductReadyCheckInLine]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductReadyCheckInLine](
	[CheckInId] [int] NOT NULL,
	[LineId] [int] IDENTITY(1,1) NOT NULL,
	[rowguid] [uniqueidentifier] NOT NULL,
	[ProductId] [int] NOT NULL,
	[TotalBatches] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[RackingPalletCol] [nvarchar](5) NULL,
	[RackingPalletRow] [smallint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReadyCheckInLine_Id] PRIMARY KEY CLUSTERED 
(
	[CheckInId] ASC,
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductReadyCheckInLineDetail]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductReadyCheckInLineDetail](
	[rowguid] [uniqueidentifier] NOT NULL,
	[LineId] [uniqueidentifier] NOT NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReadyCheckInLineDetail_Id] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC,
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductReadyCheckout]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductReadyCheckout](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CheckOutDate] [datetime] NOT NULL,
	[RevisionNumber] [tinyint] NOT NULL,
	[ModifierDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReadyCheckOut_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductReadyCheckOutLine]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductReadyCheckOutLine](
	[CheckOutId] [int] NOT NULL,
	[LineId] [int] IDENTITY(1,1) NOT NULL,
	[rowguid] [uniqueidentifier] NULL,
	[ProductId] [int] NOT NULL,
	[TotalBatches] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[RackingPalletCol] [nvarchar](5) NULL,
	[RackingPalletRow] [smallint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReadyCheckOutLine_Id] PRIMARY KEY CLUSTERED 
(
	[CheckOutId] ASC,
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductReadyCheckOutLineDetail]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductReadyCheckOutLineDetail](
	[rowguid] [uniqueidentifier] NOT NULL,
	[LineId] [uniqueidentifier] NOT NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReadyCheckOutLineDetail_Id] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC,
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductReturn]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductReturn](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReturnDate] [datetime] NOT NULL,
	[RevisionNumber] [tinyint] NOT NULL,
	[ModifierDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReturn_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductReturnLine]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductReturnLine](
	[ReturnId] [int] NOT NULL,
	[LineId] [int] IDENTITY(1,1) NOT NULL,
	[rowguid] [uniqueidentifier] NOT NULL,
	[ProductId] [int] NOT NULL,
	[TotalBatches] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[RackingPalletCol] [nvarchar](5) NULL,
	[RackingPalletRow] [smallint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReturnLine_Id] PRIMARY KEY CLUSTERED 
(
	[ReturnId] ASC,
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductReturnLineDetail]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductReturnLineDetail](
	[rowguid] [uniqueidentifier] NOT NULL,
	[LineId] [uniqueidentifier] NOT NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UnitMeasureId] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReturnLineDetail_Id] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC,
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[ProductSubcategory]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[ProductSubcategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductSubcategory_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[StockAdjustMent]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[StockAdjustMent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdjustmentDate] [datetime] NOT NULL,
	[RevisionNumber] [tinyint] NOT NULL,
	[ModifierDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StockAdjustment_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[UnitMeasure]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[UnitMeasure](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ChildId] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UnitMeasureGroupId] [int] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UnitMeasure_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Production].[UnitMeasureGroup]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Production].[UnitMeasureGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UnitMeasureGroup_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Purchasing].[ProductCheckIn]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Purchasing].[ProductCheckIn](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CheckInDate] [datetime] NOT NULL,
	[SupplierId] [int] NOT NULL,
	[RevisionNumber] [tinyint] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductCheckIn_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Purchasing].[ProductCheckInLine]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Purchasing].[ProductCheckInLine](
	[CheckInId] [int] NOT NULL,
	[LineId] [int] IDENTITY(1,1) NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NULL,
	[ProductId] [int] NOT NULL,
	[TotalBatches] [int] NOT NULL,
	[QuantityPerBatch] [decimal](18, 2) NOT NULL,
	[UnitMeasureId] [int] NOT NULL,
	[ProductionDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[LocationId] [int] NULL,
	[RackingPalletCol] [nvarchar](5) NULL,
	[RackingPalletRow] [smallint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductCheckInLine] PRIMARY KEY CLUSTERED 
(
	[CheckInId] ASC,
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Purchasing].[ProductCheckInLineDetail]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Purchasing].[ProductCheckInLineDetail](
	[rowguid] [uniqueidentifier] NOT NULL,
	[LineId] [int] NOT NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductCheckInLineDetail_Id] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC,
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Purchasing].[Supplier]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Purchasing].[Supplier](
	[BusinessEntityId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Supplier_BusinessEntityId] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[Customer]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[Customer](
	[BusinessEntityId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Customer_BusinessEntityId] PRIMARY KEY CLUSTERED 
(
	[BusinessEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[SalesOrder]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[SalesOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Status] [tinyint] NULL,
	[RevisionNumber] [tinyint] NOT NULL,
	[rowguid] [uniqueidentifier] ROWGUIDCOL  NULL,
	[ModifierDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesOrder_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[SalesOrderLine]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[SalesOrderLine](
	[CheckOutId] [int] NOT NULL,
	[LineId] [int] IDENTITY(1,1) NOT NULL,
	[rowguid] [uniqueidentifier] NULL,
	[ProductId] [int] NOT NULL,
	[TotalBatches] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SalesOlrder_Id] PRIMARY KEY CLUSTERED 
(
	[CheckOutId] ASC,
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[SalesOrderLineDetail]    Script Date: 3/26/2024 2:31:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[SalesOrderLineDetail](
	[rowguid] [uniqueidentifier] NOT NULL,
	[LineId] [uniqueidentifier] NOT NULL,
	[BatchNumber] [nvarchar](15) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductReadyCheckOutLineDetail_Id] PRIMARY KEY CLUSTERED 
(
	[LineId] ASC,
	[BatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Production].[ProductionOrderLine] ADD  CONSTRAINT [DF_ProductionOrderLine_rowguid]  DEFAULT (newid()) FOR [rowguid]
GO
ALTER TABLE [Purchasing].[ProductCheckInLine] ADD  CONSTRAINT [DF_ProductCheckInLine_rowguid]  DEFAULT (newid()) FOR [rowguid]
GO
EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'Raw Materials check out for production processing' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductCheckout'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Finished Product CheckIn-Type: 0: New finished product 1: Move between locations (e.g from production to warehouse facilitiy)' , @level0type=N'SCHEMA',@level0name=N'Production', @level1type=N'TABLE',@level1name=N'ProductReadyCheckIn', @level2type=N'COLUMN',@level2name=N'CheckInType'
GO
USE [master]
GO
ALTER DATABASE [IntrManDB] SET  READ_WRITE 
GO
