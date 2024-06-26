USE [IntrManDB]
GO
ALTER TABLE [dbo].[UserTypeFeature] DROP CONSTRAINT [FK_UserTypeFeature_UserType]
GO
ALTER TABLE [dbo].[UserTypeFeature] DROP CONSTRAINT [FK_UserTypeFeature_Feature]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_UserType]
GO
ALTER TABLE [dbo].[UserTypeFeature] DROP CONSTRAINT [DF_UserTypeFeature_Accessible]
GO
ALTER TABLE [dbo].[UserType] DROP CONSTRAINT [DF_UserType_Id]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [DF_Users_Id]
GO
ALTER TABLE [dbo].[Feature] DROP CONSTRAINT [DF_Feature_Id]
GO
/****** Object:  Table [dbo].[UserTypeFeature]    Script Date: 6/27/2024 6:26:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTypeFeature]') AND type in (N'U'))
DROP TABLE [dbo].[UserTypeFeature]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 6/27/2024 6:26:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserType]') AND type in (N'U'))
DROP TABLE [dbo].[UserType]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/27/2024 6:26:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Feature]    Script Date: 6/27/2024 6:26:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Feature]') AND type in (N'U'))
DROP TABLE [dbo].[Feature]
GO
/****** Object:  Table [dbo].[Feature]    Script Date: 6/27/2024 6:26:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feature](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ParentId] [uniqueidentifier] NULL,
	[Path] [nvarchar](50) NULL,
	[Icon] [nvarchar](50) NULL,
 CONSTRAINT [PK_Table_1_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/27/2024 6:26:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](35) NULL,
	[Password] [nvarchar](50) NULL,
	[TypeId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Users] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 6/27/2024 6:26:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Table_1] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTypeFeature]    Script Date: 6/27/2024 6:26:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypeFeature](
	[FeatureId] [uniqueidentifier] NOT NULL,
	[UserTypeId] [uniqueidentifier] NOT NULL,
	[Accessible] [bit] NULL,
 CONSTRAINT [PK_UserTypeFeature] PRIMARY KEY CLUSTERED 
(
	[FeatureId] ASC,
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Feature] ADD  CONSTRAINT [DF_Feature_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[UserType] ADD  CONSTRAINT [DF_UserType_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO
ALTER TABLE [dbo].[UserTypeFeature] ADD  CONSTRAINT [DF_UserTypeFeature_Accessible]  DEFAULT ((0)) FOR [Accessible]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserType] FOREIGN KEY([TypeId])
REFERENCES [dbo].[UserType] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserType]
GO
ALTER TABLE [dbo].[UserTypeFeature]  WITH CHECK ADD  CONSTRAINT [FK_UserTypeFeature_Feature] FOREIGN KEY([FeatureId])
REFERENCES [dbo].[Feature] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTypeFeature] CHECK CONSTRAINT [FK_UserTypeFeature_Feature]
GO
ALTER TABLE [dbo].[UserTypeFeature]  WITH CHECK ADD  CONSTRAINT [FK_UserTypeFeature_UserType] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTypeFeature] CHECK CONSTRAINT [FK_UserTypeFeature_UserType]
GO


if not exists(select * from dbo.usertype) 
begin
	insert dbo.userType(name) values ('Administrator')
	insert dbo.UserType(name) values ('Production')
	insert dbo.UserType(name) values ('Purchasing')
end
go
if not exists(select * from dbo.users) 
begin
	declare @adminId uniqueidentifier=(select id from UserType where name='Administrator') 
	declare @productionId uniqueidentifier=(select id from UserType where name='Production') 
	declare @purchasingId uniqueidentifier=(select id from UserType where name='Purchasing') 

	insert into dbo.Users(name,password,TypeId) values
		('Admin','7bebdf35690402ed85c461464de7c27b',@adminId)

	insert into dbo.Users(name,password,TypeId) values
		('Tester1','628821f8d6626ec77947af7d619de3b3',@productionId)

	insert into dbo.Users(name,password,TypeId) values
		('Tester2','628821f8d6626ec77947af7d619de3b3',@purchasingId)
end
go

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
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Transfer Inventory Item',@parentId,'/StockTransfer','trolley')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Stock Adjustment',@parentId,'/StockAdjustment','scale')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Raw Material Inventory',@parentId,'/rawmaterial-inventories','inventory')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('End Product Inventory',@parentId,'/finished-product-inventories','inventory')

insert into dbo.Feature(Name, Icon) values ('Dispatching','local_shipping')
select @parentId=id from dbo.Feature where Name='Dispatching'
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Dispatch End Product',@parentId,'/DispatchEntry','shopping_cart_checkout')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Dispatch Status',@parentId,'/DispatchStatus','schedule_send')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('Dispatch Reports',@parentId,'/UnderConstruction','insert_chart')

insert into dbo.Feature(Name, Icon) values ('Configuration','settings')
select @parentId=id from dbo.Feature where Name='Configuration'
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('User Management',@parentId,'/UserManagement','manage_accounts')
insert into dbo.Feature( Name, ParentId, Path, Icon) values ('System Configuration',@parentId,'/UnderConstruction','settings_applications')

insert into UserTypeFeature 
	select f.Id, t.Id, 0
	FROM dbo.Feature f, UserType t 

update UserTypeFeature set Accessible=1 
FROM UserTypeFeature f inner join UserType t 
ON f.UserTypeId=t.Id 
WHERE t.Name='Administrator'

update UserTypeFeature set Accessible=1 
FROM UserTypeFeature tf inner join UserType t 
ON tf.UserTypeId=t.Id inner join dbo.Feature f 
ON tf.FeatureId = f.Id 
WHERE t.Name='Production' and (f.Name in ('Production','Dispatching','Inventory','Basic Data') OR 
f.ParentId in (select Id from Feature where name in ('Production','Dispatching', 'Inventory','Basic Data')))

update UserTypeFeature set Accessible=1 
FROM UserTypeFeature tf inner join UserType t 
ON tf.UserTypeId=t.Id inner join dbo.Feature f 
ON tf.FeatureId = f.Id 
WHERE t.Name='Purchasing' and (f.Name in ('Purchasing') OR 
f.ParentId in (select Id from Feature where name in ('Purchasing')))

go


















	