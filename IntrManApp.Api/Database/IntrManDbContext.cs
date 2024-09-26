using System;
using System.Collections.Generic;
using IntrManApp.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntrManApp.Api.Database;

public partial class IntrManDbContext : DbContext
{
    public IntrManDbContext()
    {
    }

    public IntrManDbContext(DbContextOptions<IntrManDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BillOfMaterial> BillOfMaterials { get; set; }

    public virtual DbSet<BusinessEntity> BusinessEntities { get; set; }

    public virtual DbSet<BusinessEntityContact> BusinessEntityContacts { get; set; }

    public virtual DbSet<ContactType> ContactTypes { get; set; }

    public virtual DbSet<Culture> Cultures { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DiscrepantReason> DiscrepantReasons { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<InventoryFlag> InventoryFlags { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; }

    public virtual DbSet<MeasurementUnitGroup> MeasurementUnitGroups { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonType> PersonTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductCheckIn> ProductCheckIns { get; set; }

    public virtual DbSet<ProductCheckInLine> ProductCheckInLines { get; set; }

    public virtual DbSet<ProductCheckInLineDetail> ProductCheckInLineDetails { get; set; }

    public virtual DbSet<ProductCheckOutLine> ProductCheckOutLines { get; set; }

    public virtual DbSet<ProductCheckout> ProductCheckouts { get; set; }

    public virtual DbSet<ProductInternalCheckIn> ProductInternalCheckIns { get; set; }

    public virtual DbSet<ProductInternalCheckInLine> ProductInternalCheckInLines { get; set; }

    public virtual DbSet<ProductInternalCheckInLinePackaging> ProductInternalCheckInLinePackagings { get; set; }

    public virtual DbSet<ProductInternalCheckOutLine> ProductInternalCheckOutLines { get; set; }

    public virtual DbSet<ProductInternalCheckout> ProductInternalCheckouts { get; set; }

    public virtual DbSet<ProductInventory> ProductInventories { get; set; }

    public virtual DbSet<ProductNameAndDescriptionCulture> ProductNameAndDescriptionCultures { get; set; }

    public virtual DbSet<ProductPhoto> ProductPhotos { get; set; }

    public virtual DbSet<ProductProductPhoto> ProductProductPhotos { get; set; }

    public virtual DbSet<ProductVariant> ProductVariants { get; set; }

    public virtual DbSet<ProductionOrder> ProductionOrders { get; set; }

    public virtual DbSet<ProductionOrderLine> ProductionOrderLines { get; set; }

    public virtual DbSet<ProductionOrderLineDetail> ProductionOrderLineDetails { get; set; }

    public virtual DbSet<ProductionOrderLineDetailResource> ProductionOrderLineDetailResources { get; set; }

    public virtual DbSet<ProductionOrderLineDetailResourceAllocation> ProductionOrderLineDetailResourceAllocations { get; set; }

    public virtual DbSet<RackingPallet> RackingPallets { get; set; }

    public virtual DbSet<SalesOrder> SalesOrders { get; set; }

    public virtual DbSet<SalesOrderLine> SalesOrderLines { get; set; }

    public virtual DbSet<StockAdjustMent> StockAdjustMents { get; set; }

    public virtual DbSet<StockAdjustmentLine> StockAdjustmentLines { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<UserTypeFeature> UserTypeFeatures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Database");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<BillOfMaterial>(entity =>
        {
            entity.ToTable("BillOfMaterials", "Production");

            entity.HasIndex(e => e.RawMaterialMeasurementUnitId, "IX_BillOfMaterials_RawMaterialMeasurementUnitId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RawMaterialQuantity)
                .HasDefaultValue(1.0m)
                .HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.RawMaterialMeasurementUnit).WithMany(p => p.BillOfMaterials)
                .HasForeignKey(d => d.RawMaterialMeasurementUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BillOfMaterials_MeasurementUnit");
        });

        modelBuilder.Entity<BusinessEntity>(entity =>
        {
            entity.ToTable("BusinessEntity", "Person");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<BusinessEntityContact>(entity =>
        {
            entity.HasKey(e => new { e.BusinessEntityId, e.PersonId, e.ContactTypeId });

            entity.ToTable("BusinessEntityContact", "Person");

            entity.HasIndex(e => e.ContactTypeId, "IX_BusinessEntityContact_ContactTypeId");

            entity.HasIndex(e => e.PersonId, "IX_BusinessEntityContact_PersonId");

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.BusinessEntity).WithMany(p => p.BusinessEntityContacts)
                .HasForeignKey(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessEntityContact_BusinessEntity");

            entity.HasOne(d => d.ContactType).WithMany(p => p.BusinessEntityContacts)
                .HasForeignKey(d => d.ContactTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessEntityContact_ContactType");

            entity.HasOne(d => d.Person).WithMany(p => p.BusinessEntityContacts)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessEntityContact_Person");
        });

        modelBuilder.Entity<ContactType>(entity =>
        {
            entity.ToTable("ContactType", "Person");

            entity.HasIndex(e => e.Name, "IX_ContactType").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Culture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pk_Culture_Id");

            entity.ToTable("Culture", "Production");

            entity.Property(e => e.Id)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.BusinessEntityId);

            entity.ToTable("Customer", "Sales");

            entity.HasIndex(e => e.Name, "IX_Customer");

            entity.Property(e => e.BusinessEntityId).ValueGeneratedNever();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.BusinessEntity).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_BusinessEntity");
        });

        modelBuilder.Entity<DiscrepantReason>(entity =>
        {
            entity.ToTable("DiscrepantReason", "Production");

            entity.HasIndex(e => e.Reason, "IX_DiscrepantReason").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Reason).HasMaxLength(100);
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Table_1_1");

            entity.ToTable("Feature");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Path).HasMaxLength(50);
        });

        modelBuilder.Entity<InventoryFlag>(entity =>
        {
            entity.ToTable("InventoryFlag", "Production");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location", "Production");

            entity.HasIndex(e => e.Name, "IX_Location").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MeasurementUnit>(entity =>
        {
            entity.ToTable("MeasurementUnit", "Production");

            entity.HasIndex(e => new { e.Name, e.GroupId }, "IX_MeasurementUnit")
                .IsUnique()
                .HasFilter("([GroupId] IS NOT NULL)");

            entity.HasIndex(e => e.ChildId, "IX_MeasurementUnit_ChildId");

            entity.HasIndex(e => e.GroupId, "IX_MeasurementUnit_GroupId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Initial).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1.0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Child).WithMany(p => p.InverseChild)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK_MeasurementUnit_MeasurementUnit");

            entity.HasOne(d => d.Group).WithMany(p => p.MeasurementUnits)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_MeasurementUnit_MeasurementUnitGroup");
        });

        modelBuilder.Entity<MeasurementUnitGroup>(entity =>
        {
            entity.ToTable("MeasurementUnitGroup", "Production");

            entity.HasIndex(e => e.Name, "IX_MeasurementUnitGroup").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.BusinessEntityId);

            entity.ToTable("Person", "Person");

            entity.HasIndex(e => e.PersonTypeId, "IX_Person_PersonTypeId");

            entity.Property(e => e.BusinessEntityId).ValueGeneratedNever();
            entity.Property(e => e.AdditionalContactInfo).HasColumnType("xml");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Suffix).HasMaxLength(10);
            entity.Property(e => e.Title).HasMaxLength(8);

            entity.HasOne(d => d.BusinessEntity).WithOne(p => p.Person)
                .HasForeignKey<Person>(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Person_BusinessEntity");

            entity.HasOne(d => d.PersonType).WithMany(p => p.People)
                .HasForeignKey(d => d.PersonTypeId)
                .HasConstraintName("FK_Person_PersonType");
        });

        modelBuilder.Entity<PersonType>(entity =>
        {
            entity.ToTable("PersonType", "Person");

            entity.HasIndex(e => e.Name, "IX_PersonType").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product", "Production");

            entity.HasIndex(e => e.ProductNumber, "IX_Product").IsUnique();

            entity.HasIndex(e => e.CategoryId, "IX_Product_CategoryId");

            entity.HasIndex(e => e.LocationId, "IX_Product_LocationId");

            entity.HasIndex(e => e.MeasurementUnitGroupId, "IX_Product_MeasurementUnitGroupId");

            entity.HasIndex(e => e.MeasurementUnitOrderId, "IX_Product_MeasurementUnitOrderId");

            entity.HasIndex(e => e.RackingPalletId, "IX_Product_RackingPalletId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.AdditionalInfo).HasColumnType("xml");
            entity.Property(e => e.DaysToExpire).HasDefaultValue(30);
            entity.Property(e => e.DaysToManufacture).HasDefaultValue(0);
            entity.Property(e => e.IsSalable).HasDefaultValue(false);
            entity.Property(e => e.IsUniqueBatchPerOrder).HasDefaultValue(true);
            entity.Property(e => e.ListPrice)
                .HasDefaultValue(0.0m)
                .HasColumnType("money");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderQuantity)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(8, 2)");
            entity.Property(e => e.ProductNumber).HasMaxLength(25);
            entity.Property(e => e.ReorderPoint)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SafetyStockLevel)
                .HasDefaultValue(0.0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StandardCost)
                .HasDefaultValue(0.0m)
                .HasColumnType("money");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Product_ProductCategory");

            entity.HasOne(d => d.Location).WithMany(p => p.Products)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Location");

            entity.HasOne(d => d.MeasurementUnitGroup).WithMany(p => p.Products)
                .HasForeignKey(d => d.MeasurementUnitGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_MeasurementUnitGroup");

            entity.HasOne(d => d.MeasurementUnitOrder).WithMany(p => p.Products)
                .HasForeignKey(d => d.MeasurementUnitOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_MeasurementUnit");

            entity.HasOne(d => d.RackingPallet).WithMany(p => p.Products)
                .HasForeignKey(d => d.RackingPalletId)
                .HasConstraintName("FK_Product_RackingPallet");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("ProductCategory", "Production");

            entity.HasIndex(e => e.Name, "IX_ProductCategory").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductCheckIn>(entity =>
        {
            entity.ToTable("ProductCheckIn", "Purchasing");

            entity.HasIndex(e => e.SupplierId, "IX_ProductCheckIn_SupplierId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.CheckInDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RevisionNumber).HasDefaultValue((byte)0);

            entity.HasOne(d => d.Supplier).WithMany(p => p.ProductCheckIns)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_ProductCheckIn_Supplier");
        });

        modelBuilder.Entity<ProductCheckInLine>(entity =>
        {
            entity.HasKey(e => e.LineId).HasName("PK_ProductCheckInLine_1");

            entity.ToTable("ProductCheckInLine", "Purchasing");

            entity.HasIndex(e => e.CheckInId, "IX_ProductCheckInLine_CheckInId");

            entity.HasIndex(e => e.LocationId, "IX_ProductCheckInLine_LocationId");

            entity.HasIndex(e => e.MeasurementUnitId, "IX_ProductCheckInLine_MeasurementUnitId");

            entity.HasIndex(e => e.ProductId, "IX_ProductCheckInLine_ProductId");

            entity.HasIndex(e => e.RackingPalletId, "IX_ProductCheckInLine_RackingPalletId");

            entity.Property(e => e.LineId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.ProductionDate).HasColumnType("datetime");
            entity.Property(e => e.QuantityPerBatch).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalBatches).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CheckIn).WithMany(p => p.ProductCheckInLines)
                .HasForeignKey(d => d.CheckInId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductCheckInLine_ProductCheckIn");

            entity.HasOne(d => d.Location).WithMany(p => p.ProductCheckInLines)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_ProductCheckInLine_Location");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.ProductCheckInLines)
                .HasForeignKey(d => d.MeasurementUnitId)
                .HasConstraintName("FK_ProductCheckInLine_MeasurementUnit");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCheckInLines)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductCheckInLine_Product");

            entity.HasOne(d => d.RackingPallet).WithMany(p => p.ProductCheckInLines)
                .HasForeignKey(d => d.RackingPalletId)
                .HasConstraintName("FK_ProductCheckInLine_RackingPallet");
        });

        modelBuilder.Entity<ProductCheckInLineDetail>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK_ProductCheckInLineDetail_1");

            entity.ToTable("ProductCheckInLineDetail", "Purchasing");

            entity.HasIndex(e => e.BatchNumber, "IX_ProductCheckInLineDetail").IsUnique();

            entity.HasIndex(e => e.LineId, "IX_ProductCheckInLineDetail_LineId");

            entity.Property(e => e.InventoryId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.BatchNumber).HasMaxLength(15);

            entity.HasOne(d => d.Line).WithMany(p => p.ProductCheckInLineDetails)
                .HasForeignKey(d => d.LineId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductCheckInLineDetail_ProductCheckInLine");
        });

        modelBuilder.Entity<ProductCheckOutLine>(entity =>
        {
            entity.HasKey(e => new { e.CheckOutId, e.InventoryId });

            entity.ToTable("ProductCheckOutLine", "Production");

            entity.HasIndex(e => e.InventoryId, "IX_ProductCheckOutLine_InventoryId");

            entity.HasIndex(e => e.LocationId, "IX_ProductCheckOutLine_LocationId");

            entity.HasIndex(e => e.MeasurementUnitId, "IX_ProductCheckOutLine_MeasurementUnitId");

            entity.HasIndex(e => e.RackingPalletId, "IX_ProductCheckOutLine_RackingPalletId");

            entity.HasIndex(e => e.SourceLocationId, "IX_ProductCheckOutLine_SourceLocationId");

            entity.HasIndex(e => e.SourceRackingPalletId, "IX_ProductCheckOutLine_SourceRackingPalletId");

            entity.Property(e => e.InventoryId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CheckOut).WithMany(p => p.ProductCheckOutLines)
                .HasForeignKey(d => d.CheckOutId)
                .HasConstraintName("FK_ProductCheckOutLine_ProductCheckout");

            entity.HasOne(d => d.Inventory).WithMany(p => p.ProductCheckOutLines)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductCheckOutLine_ProductInventory");

            entity.HasOne(d => d.Location).WithMany(p => p.ProductCheckOutLineLocations)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_ProductCheckOutLine_Location");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.ProductCheckOutLines)
                .HasForeignKey(d => d.MeasurementUnitId)
                .HasConstraintName("FK_ProductCheckOutLine_MeasurementUnit");

            entity.HasOne(d => d.RackingPallet).WithMany(p => p.ProductCheckOutLineRackingPallets)
                .HasForeignKey(d => d.RackingPalletId)
                .HasConstraintName("FK_ProductCheckOutLine_RackingPallet");

            entity.HasOne(d => d.SourceLocation).WithMany(p => p.ProductCheckOutLineSourceLocations)
                .HasForeignKey(d => d.SourceLocationId)
                .HasConstraintName("FK_ProductCheckOutLine_SourceLocation");

            entity.HasOne(d => d.SourceRackingPallet).WithMany(p => p.ProductCheckOutLineSourceRackingPallets)
                .HasForeignKey(d => d.SourceRackingPalletId)
                .HasConstraintName("FK_ProductCheckOutLine_SourceRackingPallet");
        });

        modelBuilder.Entity<ProductCheckout>(entity =>
        {
            entity.ToTable("ProductCheckout", "Production");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.CheckOutDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifierDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RevisionNumber).HasDefaultValue((byte)0);
        });

        modelBuilder.Entity<ProductInternalCheckIn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProductReadyCheckIn");

            entity.ToTable("ProductInternalCheckIn", "Production");

            entity.HasIndex(e => e.CheckInType, "IX_ProductInternalCheckIn_CheckInType");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.CheckInDate).HasColumnType("datetime");
            entity.Property(e => e.ModifierDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RevisionNumber)
                .HasDefaultValue((byte)0)
                .HasComment("");

            entity.HasOne(d => d.CheckInTypeNavigation).WithMany(p => p.ProductInternalCheckIns)
                .HasForeignKey(d => d.CheckInType)
                .HasConstraintName("FK_ProductInternalCheckIn_InventoryFlag");
        });

        modelBuilder.Entity<ProductInternalCheckInLine>(entity =>
        {
            entity.HasKey(e => new { e.CheckInId, e.InventoryId });

            entity.ToTable("ProductInternalCheckInLine", "Production");

            entity.HasIndex(e => e.LineId, "IX_ProductInternalCheckInLine").IsUnique();

            entity.HasIndex(e => e.InventoryId, "IX_ProductInternalCheckInLine_InventoryId");

            entity.HasIndex(e => e.MeasurementUnitId, "IX_ProductInternalCheckInLine_MeasurementUnitId");

            entity.Property(e => e.LineId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasComment("Finished Product CheckIn-Type: 0: New finished product 1: Move between locations (e.g from production to warehouse facilitiy)")
                .HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CheckIn).WithMany(p => p.ProductInternalCheckInLines)
                .HasForeignKey(d => d.CheckInId)
                .HasConstraintName("FK_ProductReadyCheckInLine_ProductReadyCheckIn");

            entity.HasOne(d => d.Inventory).WithMany(p => p.ProductInternalCheckInLines)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductReadyCheckInLine_ProductInventory");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.ProductInternalCheckInLines)
                .HasForeignKey(d => d.MeasurementUnitId)
                .HasConstraintName("FK_ProductReadyCheckInLine_MeasurementUnit");
        });

        modelBuilder.Entity<ProductInternalCheckInLinePackaging>(entity =>
        {
            entity.HasKey(e => new { e.LineId, e.InventoryId });

            entity.ToTable("ProductInternalCheckInLinePackaging", "Production");

            entity.HasIndex(e => e.LocationId, "IX_ProductInternalCheckInLinePackaging_LocationId");

            entity.HasIndex(e => e.MeasurementUnitId, "IX_ProductInternalCheckInLinePackaging_MeasurementUnitId");

            entity.HasIndex(e => e.RackingPalletId, "IX_ProductInternalCheckInLinePackaging_RackingPalletId");

            entity.HasIndex(e => e.SourceLocationId, "IX_ProductInternalCheckInLinePackaging_SourceLocationId");

            entity.HasIndex(e => e.SourceRackingPalletId, "IX_ProductInternalCheckInLinePackaging_SourceRackingPalletId");

            entity.Property(e => e.InventoryId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Line).WithMany(p => p.ProductInternalCheckInLinePackagings)
                .HasPrincipalKey(p => p.LineId)
                .HasForeignKey(d => d.LineId)
                .HasConstraintName("FK_ProductInternalCheckInLinePackaging_ProductInternalCheckInLine");

            entity.HasOne(d => d.Location).WithMany(p => p.ProductInternalCheckInLinePackagingLocations)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_ProductInternalCheckInLinePackaging_Location");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.ProductInternalCheckInLinePackagings)
                .HasForeignKey(d => d.MeasurementUnitId)
                .HasConstraintName("FK_ProductInternalCheckInLinePackaging_MeasurementUnit");

            entity.HasOne(d => d.RackingPallet).WithMany(p => p.ProductInternalCheckInLinePackagingRackingPallets)
                .HasForeignKey(d => d.RackingPalletId)
                .HasConstraintName("FK_ProductInternalCheckInLinePackaging_RackingPallet");

            entity.HasOne(d => d.SourceLocation).WithMany(p => p.ProductInternalCheckInLinePackagingSourceLocations)
                .HasForeignKey(d => d.SourceLocationId)
                .HasConstraintName("FK_ProductInternalCheckInLinePackaging_Location1");

            entity.HasOne(d => d.SourceRackingPallet).WithMany(p => p.ProductInternalCheckInLinePackagingSourceRackingPallets)
                .HasForeignKey(d => d.SourceRackingPalletId)
                .HasConstraintName("FK_ProductInternalCheckInLinePackaging_RackingPallet1");
        });

        modelBuilder.Entity<ProductInternalCheckOutLine>(entity =>
        {
            entity.HasKey(e => new { e.CheckOutId, e.InventoryId });

            entity.ToTable("ProductInternalCheckOutLine", "Production");

            entity.HasIndex(e => e.InventoryId, "IX_ProductInternalCheckOutLine_InventoryId");

            entity.HasIndex(e => e.LocationId, "IX_ProductInternalCheckOutLine_LocationId");

            entity.HasIndex(e => e.MeasurementUnitId, "IX_ProductInternalCheckOutLine_MeasurementUnitId");

            entity.HasIndex(e => e.RackingPalletId, "IX_ProductInternalCheckOutLine_RackingPalletId");

            entity.HasIndex(e => e.SourceLocationId, "IX_ProductInternalCheckOutLine_SourceLocationId");

            entity.HasIndex(e => e.SourceRackingPalletId, "IX_ProductInternalCheckOutLine_SourceRackingPalletId");

            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductionDate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CheckOut).WithMany(p => p.ProductInternalCheckOutLines)
                .HasForeignKey(d => d.CheckOutId)
                .HasConstraintName("FK_ProductInternalCheckOutLine_ProductInternalCheckout");

            entity.HasOne(d => d.Inventory).WithMany(p => p.ProductInternalCheckOutLines)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductInternalCheckOutLine_ProductInventory");

            entity.HasOne(d => d.Location).WithMany(p => p.ProductInternalCheckOutLineLocations)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_ProductInternalCheckOutLine_Location");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.ProductInternalCheckOutLines)
                .HasForeignKey(d => d.MeasurementUnitId)
                .HasConstraintName("FK_ProductInternalCheckOutLine_MeasurementUnit");

            entity.HasOne(d => d.RackingPallet).WithMany(p => p.ProductInternalCheckOutLineRackingPallets)
                .HasForeignKey(d => d.RackingPalletId)
                .HasConstraintName("FK_ProductInternalCheckOutLine_RackingPallet");

            entity.HasOne(d => d.SourceLocation).WithMany(p => p.ProductInternalCheckOutLineSourceLocations)
                .HasForeignKey(d => d.SourceLocationId)
                .HasConstraintName("FK_ProductInternalCheckOutLine_SourceLocation");

            entity.HasOne(d => d.SourceRackingPallet).WithMany(p => p.ProductInternalCheckOutLineSourceRackingPallets)
                .HasForeignKey(d => d.SourceRackingPalletId)
                .HasConstraintName("FK_ProductInternalCheckOutLine_SourceRackingPallet");
        });

        modelBuilder.Entity<ProductInternalCheckout>(entity =>
        {
            entity.ToTable("ProductInternalCheckout", "Production");

            entity.HasIndex(e => e.CheckOutType, "IX_ProductInternalCheckout_CheckOutType");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.CheckOutDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifierDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RevisionNumber).HasDefaultValue((byte)0);

            entity.HasOne(d => d.CheckOutTypeNavigation).WithMany(p => p.ProductInternalCheckouts)
                .HasForeignKey(d => d.CheckOutType)
                .HasConstraintName("FK_ProductInternalCheckout_InventoryFlag");
        });

        modelBuilder.Entity<ProductInventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId);

            entity.ToTable("ProductInventory", "Production");

            entity.HasIndex(e => e.InventoryId, "IX_ProductInventory");

            entity.HasIndex(e => e.BatchNumber, "IX_ProductInventory_2");

            entity.HasIndex(e => e.Flag, "IX_ProductInventory_Flag");

            entity.HasIndex(e => e.LocationId, "IX_ProductInventory_LocationId");

            entity.HasIndex(e => e.MeasurementUnitId, "IX_ProductInventory_MeasurementUnitId");

            entity.HasIndex(e => e.ProductId, "IX_ProductInventory_ProductId");

            entity.HasIndex(e => e.RackingPalletId, "IX_ProductInventory_RackingPalletId");

            entity.Property(e => e.InventoryId).ValueGeneratedNever();
            entity.Property(e => e.BatchNumber).HasMaxLength(15);
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.Flag)
                .HasDefaultValue((byte)1)
                .HasComment("1. CheckIn (purchase) 2. CheckOut for Production 3. Return from Production 4. Waiting for Production 5. In production 6. Check-In from production 7. New Delivery Order 8. Packing 9. Packed 10. Dispatched 11. Move location");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductionDate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Reserved)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TotalBatches)
                .HasDefaultValue(1.0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.FlagNavigation).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.Flag)
                .HasConstraintName("FK_ProductInventory_InventoryFlag");

            entity.HasOne(d => d.Location).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_ProductInventory_Location");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.MeasurementUnitId)
                .HasConstraintName("FK_ProductInventory_MeasurementUnit");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductInventory_Product");

            entity.HasOne(d => d.RackingPallet).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.RackingPalletId)
                .HasConstraintName("FK_ProductInventory_RackingPallet");
        });

        modelBuilder.Entity<ProductNameAndDescriptionCulture>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.CultureId });

            entity.ToTable("ProductNameAndDescriptionCulture", "Production");

            entity.HasIndex(e => e.CultureId, "IX_ProductNameAndDescriptionCulture_CultureId");

            entity.Property(e => e.CultureId)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Culture).WithMany(p => p.ProductNameAndDescriptionCultures)
                .HasForeignKey(d => d.CultureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductNameAndDescriptionCulture_Culture");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductNameAndDescriptionCultures)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductNameAndDescriptionCulture_Product");
        });

        modelBuilder.Entity<ProductPhoto>(entity =>
        {
            entity.ToTable("ProductPhoto", "Production");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.LargePhotoFileName).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ThumbnailPhotoFileName).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductProductPhoto>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductProductPhoto", "Production");

            entity.HasIndex(e => e.ProductId, "IX_ProductProductPhoto_ProductId");

            entity.HasIndex(e => e.ProductPhoto, "IX_ProductProductPhoto_ProductPhoto");

            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductProductPhoto_Product");

            entity.HasOne(d => d.ProductPhotoNavigation).WithMany()
                .HasForeignKey(d => d.ProductPhoto)
                .HasConstraintName("FK_ProductProductPhoto_ProductPhoto");
        });

        modelBuilder.Entity<ProductVariant>(entity =>
        {
            entity.ToTable("ProductVariant", "Production");

            entity.HasIndex(e => new { e.ProductId, e.MeasurementUnitId, e.Weight }, "IX_ProductVariant").IsUnique();

            entity.HasIndex(e => e.MeasurementUnitId, "IX_ProductVariant_MeasurementUnitId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.MeasurementUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductVariant_MeasurementUnit");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductVariants)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductVariant_Product");
        });

        modelBuilder.Entity<ProductionOrder>(entity =>
        {
            entity.ToTable("ProductionOrder", "Production");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ScheduledDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ProductionOrderLine>(entity =>
        {
            entity.HasKey(e => e.LineId);

            entity.ToTable("ProductionOrderLine", "Production");

            entity.HasIndex(e => e.MeasurementUnitId, "IX_ProductionOrderLine_MeasurementUnitId");

            entity.HasIndex(e => e.ProductId, "IX_ProductionOrderLine_ProductId");

            entity.HasIndex(e => e.ProductionOrderId, "IX_ProductionOrderLine_ProductionOrderId");

            entity.Property(e => e.LineId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.QuantityPerBatch).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalBatches).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalBatchesCompleted).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalBatchesScrapped).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.ProductionOrderLines)
                .HasForeignKey(d => d.MeasurementUnitId)
                .HasConstraintName("FK_ProductionOrderLine_MeasurementUnit");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductionOrderLines)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductionOrderLine_Product");

            entity.HasOne(d => d.ProductionOrder).WithMany(p => p.ProductionOrderLines)
                .HasForeignKey(d => d.ProductionOrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductionOrderLine_ProductionOrder");
        });

        modelBuilder.Entity<ProductionOrderLineDetail>(entity =>
        {
            entity.HasKey(e => e.InventoryId);

            entity.ToTable("ProductionOrderLineDetail", "Production");

            entity.HasIndex(e => e.LineId, "IX_ProductionOrderLineDetail_LineId");

            entity.Property(e => e.InventoryId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.BatchNumber).HasMaxLength(15);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Line).WithMany(p => p.ProductionOrderLineDetails)
                .HasForeignKey(d => d.LineId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductionOrderLineDetail_ProductionOrderLine");
        });

        modelBuilder.Entity<ProductionOrderLineDetailResource>(entity =>
        {
            entity.HasKey(e => e.ResourceId);

            entity.ToTable("ProductionOrderLineDetailResource", "Production");

            entity.HasIndex(e => e.InventoryId, "IX_ProductionOrderLineDetailResource_InventoryId");

            entity.HasIndex(e => e.MeasurementUnitId, "IX_ProductionOrderLineDetailResource_MeasurementUnitId");

            entity.HasIndex(e => e.RawMaterialId, "IX_ProductionOrderLineDetailResource_RawMaterialId");

            entity.Property(e => e.ResourceId).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifierDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Inventory).WithMany(p => p.ProductionOrderLineDetailResources)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductionOrderLineDetailResource_ProductionOrderLineDetail");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.ProductionOrderLineDetailResources)
                .HasForeignKey(d => d.MeasurementUnitId)
                .HasConstraintName("FK_ProductionOrderLineDetailResource_MeasurementUnit");

            entity.HasOne(d => d.RawMaterial).WithMany(p => p.ProductionOrderLineDetailResources)
                .HasForeignKey(d => d.RawMaterialId)
                .HasConstraintName("FK_ProductionOrderLineDetailResource_Product");
        });

        modelBuilder.Entity<ProductionOrderLineDetailResourceAllocation>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductionOrderLineDetailResourceAllocation", "Production");

            entity.HasIndex(e => e.InventoryId, "IX_ProductionOrderLineDetailResourceAllocation_InventoryId");

            entity.HasIndex(e => e.MeasurementUnitId, "IX_ProductionOrderLineDetailResourceAllocation_MeasurementUnitId");

            entity.HasIndex(e => e.ResourceId, "IX_ProductionOrderLineDetailResourceAllocation_ResourceId");

            entity.Property(e => e.ModifierDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Inventory).WithMany()
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK_ProductionOrderLineDetailResourceAllocation_ProductInventory");

            entity.HasOne(d => d.MeasurementUnit).WithMany()
                .HasForeignKey(d => d.MeasurementUnitId)
                .HasConstraintName("FK_ProductionOrderLineDetailResourceAllocation_MeasurementUnit");

            entity.HasOne(d => d.Resource).WithMany()
                .HasForeignKey(d => d.ResourceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductionOrderLineDetailResourceAllocation_ProductionOrderLineDetailResource");
        });

        modelBuilder.Entity<RackingPallet>(entity =>
        {
            entity.ToTable("RackingPallet", "Production");

            entity.HasIndex(e => new { e.Col, e.Row }, "IX_RackingPallet")
                .IsUnique()
                .HasFilter("([Col] IS NOT NULL AND [Row] IS NOT NULL)");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Col).HasMaxLength(35);
            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<SalesOrder>(entity =>
        {
            entity.ToTable("SalesOrder", "Sales");

            entity.HasIndex(e => e.CustomerId, "IX_SalesOrder_CustomerId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RevisionNumber).HasDefaultValue((byte)0);
            entity.Property(e => e.Status)
                .HasDefaultValue((byte)7)
                .HasComment("7. New Delivery Order 8. Packing 9. Packed 10. Dispatched");

            entity.HasOne(d => d.Customer).WithMany(p => p.SalesOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_SalesOrder_Customer");
        });

        modelBuilder.Entity<SalesOrderLine>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.InventoryId });

            entity.ToTable("SalesOrderLine", "Sales");

            entity.HasIndex(e => e.InventoryId, "IX_SalesOrderLine_InventoryId");

            entity.HasIndex(e => e.MeasurementUnitId, "IX_SalesOrderLine_MeasurementUnitId");

            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Inventory).WithMany(p => p.SalesOrderLines)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalesOrderLine_ProductInventory");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.SalesOrderLines)
                .HasForeignKey(d => d.MeasurementUnitId)
                .HasConstraintName("FK_SalesOrderLine_MeasurementUnit");

            entity.HasOne(d => d.Order).WithMany(p => p.SalesOrderLines)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_SalesOrderLine_SalesOrder");
        });

        modelBuilder.Entity<StockAdjustMent>(entity =>
        {
            entity.ToTable("StockAdjustMent", "Production");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.AdjustmentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FromInventoryTransfer).HasDefaultValue(false);
            entity.Property(e => e.ModifierDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RevisionNumber).HasDefaultValue((byte)0);
        });

        modelBuilder.Entity<StockAdjustmentLine>(entity =>
        {
            entity.HasKey(e => new { e.AdjustmentId, e.InventoryId });

            entity.ToTable("StockAdjustmentLine", "Production");

            entity.HasIndex(e => e.InventoryId, "IX_StockAdjustmentLine_InventoryId");

            entity.HasIndex(e => e.MeasurementUnitId, "IX_StockAdjustmentLine_MeasurementUnitId");

            entity.HasIndex(e => e.ReasonId, "IX_StockAdjustmentLine_ReasonId");

            entity.Property(e => e.Adjustment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.InitialQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductionDate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AdjustmentNavigation).WithMany(p => p.StockAdjustmentLines)
                .HasForeignKey(d => d.AdjustmentId)
                .HasConstraintName("FK_StockAdjustmentLine_StockAdjustMent");

            entity.HasOne(d => d.Inventory).WithMany(p => p.StockAdjustmentLines)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockAdjustmentLine_ProductInventory");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.StockAdjustmentLines)
                .HasForeignKey(d => d.MeasurementUnitId)
                .HasConstraintName("FK_StockAdjustmentLine_MeasurementUnit");

            entity.HasOne(d => d.Reason).WithMany(p => p.StockAdjustmentLines)
                .HasForeignKey(d => d.ReasonId)
                .HasConstraintName("FK_StockAdjustmentLine_DiscrepantReason");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.BusinessEntityId);

            entity.ToTable("Supplier", "Purchasing");

            entity.HasIndex(e => e.Name, "IX_Supplier").IsUnique();

            entity.Property(e => e.BusinessEntityId).ValueGeneratedNever();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.BusinessEntity).WithOne(p => p.Supplier)
                .HasForeignKey<Supplier>(d => d.BusinessEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplier_BusinessEntity");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_Users").IsUnique();

            entity.HasIndex(e => e.TypeId, "IX_Users_TypeId");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Name).HasMaxLength(35);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.Type).WithMany(p => p.Users)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Users_UserType");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Table_1");

            entity.ToTable("UserType");

            entity.HasIndex(e => e.Name, "IX_Table_1")
                .IsUnique()
                .HasFilter("([Name] IS NOT NULL)");

            entity.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UserTypeFeature>(entity =>
        {
            entity.HasKey(e => new { e.FeatureId, e.UserTypeId });

            entity.ToTable("UserTypeFeature");

            entity.Property(e => e.Accessible).HasDefaultValue(false);

            entity.HasOne(d => d.Feature).WithMany(p => p.UserTypeFeatures)
                .HasForeignKey(d => d.FeatureId)
                .HasConstraintName("FK_UserTypeFeature_Feature");

            entity.HasOne(d => d.UserType).WithMany(p => p.UserTypeFeatures)
                .HasForeignKey(d => d.UserTypeId)
                .HasConstraintName("FK_UserTypeFeature_UserType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
