using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ecommerce_dash_api.Models;

public partial class EcommerceContext : DbContext
{
    public EcommerceContext()
    {
    }

    public EcommerceContext(DbContextOptions<EcommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApiLog> ApiLogs { get; set; }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<AttributeOption> AttributeOptions { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Chart> Charts { get; set; }

    public virtual DbSet<ChartProperty> ChartProperties { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerShippingAddress> CustomerShippingAddresses { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderPayment> OrderPayments { get; set; }

    public virtual DbSet<OrderShipping> OrderShippings { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductInfo> ProductInfos { get; set; }

    public virtual DbSet<ProductMedium> ProductMedia { get; set; }

    public virtual DbSet<ProductQuantity> ProductQuantities { get; set; }

    public virtual DbSet<ProductQuantityAttrbiute> ProductQuantityAttrbiutes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Season> Seasons { get; set; }

    public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionAttribute> TransactionAttributes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=127.0.0.1;database=ecommerce;user=root;password=123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<ApiLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("api_logs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FunctionName)
                .HasMaxLength(100)
                .HasColumnName("function_name");
            entity.Property(e => e.FunctionParameters)
                .HasColumnType("text")
                .HasColumnName("function_parameters");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.LogLevel)
                .HasMaxLength(50)
                .HasColumnName("log_level");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.StackTrace)
                .HasColumnType("text")
                .HasColumnName("stack_trace");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("timestamp");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("attribute");

            entity.HasIndex(e => e.UpdatedBy, "attribute_ibfk_1");

            entity.HasIndex(e => e.Name, "name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Attributes)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("attribute_ibfk_1");
        });

        modelBuilder.Entity<AttributeOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("attribute_option");

            entity.HasIndex(e => e.UpdatedBy, "attribute_option_ibfk_1");

            entity.HasIndex(e => e.AttributeId, "attribute_option_ibfk_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
            entity.Property(e => e.Option)
                .HasMaxLength(255)
                .HasColumnName("option");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeOptions)
                .HasForeignKey(d => d.AttributeId)
                .HasConstraintName("attribute_option_ibfk_2");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.AttributeOptions)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("attribute_option_ibfk_1");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("brand");

            entity.HasIndex(e => e.UpdatedBy, "brand_ibfk_1");

            entity.HasIndex(e => e.CountryId, "brand_ibfk_2");

            entity.HasIndex(e => e.Name, "name1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(255)
                .HasColumnName("logo_url");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("website");

            entity.HasOne(d => d.Country).WithMany(p => p.Brands)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("brand_ibfk_2");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Brands)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("brand_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("category");

            entity.HasIndex(e => e.UpdatedBy, "category_ibfk_1");

            entity.HasIndex(e => e.Name, "name2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Categories)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("category_ibfk_1");
        });

        modelBuilder.Entity<Chart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("chart");

            entity.HasIndex(e => e.UpdatedBy, "chart_ibfk_1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Label)
                .HasMaxLength(255)
                .HasColumnName("label");
            entity.Property(e => e.Query)
                .HasColumnType("text")
                .HasColumnName("query");
            entity.Property(e => e.Type)
                .HasColumnType("enum('VerticalBarChart','HorizontalBarChart','BiaxialLineChart','ArcDesign','TopN','BasicColorLegend')")
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Charts)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("chart_ibfk_1");
        });

        modelBuilder.Entity<ChartProperty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("chart_property");

            entity.HasIndex(e => e.UpdatedBy, "chart_property_ibfk_2");

            entity.HasIndex(e => new { e.ChartId, e.Name }, "unique_chart_id_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChartId).HasColumnName("chart_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Value)
                .HasMaxLength(255)
                .HasColumnName("value");

            entity.HasOne(d => d.Chart).WithMany(p => p.ChartProperties)
                .HasForeignKey(d => d.ChartId)
                .HasConstraintName("chart_property_ibfk_1");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ChartProperties)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("chart_property_ibfk_2");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("country");

            entity.HasIndex(e => e.Code, "code").IsUnique();

            entity.HasIndex(e => e.UpdatedBy, "country_ibfk_1");

            entity.HasIndex(e => e.Name, "name3").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Countries)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("country_ibfk_1");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("currency");

            entity.HasIndex(e => e.UpdatedBy, "currency_ibfk_1");

            entity.HasIndex(e => e.CountryId, "currency_ibfk_2");

            entity.HasIndex(e => e.IsActive, "currency_idx_1");

            entity.HasIndex(e => e.Name, "name4").IsUnique();

            entity.HasIndex(e => e.Symbol, "symbol").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.ExchangeRateUsd)
                .HasPrecision(20, 2)
                .HasColumnName("exchange_rate_USD");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Symbol).HasColumnName("symbol");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Country).WithMany(p => p.Currencies)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("currency_ibfk_2");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Currencies)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("currency_ibfk_1");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.HasIndex(e => e.UpdatedBy, "customer_ibfk_1");

            entity.HasIndex(e => e.IsActive, "customer_idx_1");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Customers)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("customer_ibfk_1");
        });

        modelBuilder.Entity<CustomerShippingAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("customer_shipping_address");

            entity.HasIndex(e => e.CustomerId, "customer_shipping_address_ibfk_1");

            entity.HasIndex(e => e.CountryId, "customer_shipping_address_ibfk_2");

            entity.HasIndex(e => e.IsPrimary, "customer_shipping_address_idx_1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressLine)
                .HasMaxLength(255)
                .HasColumnName("address_line");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.IsPrimary).HasColumnName("is_primary");
            entity.Property(e => e.PostalCode).HasColumnName("postal_code");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");

            entity.HasOne(d => d.Country).WithMany(p => p.CustomerShippingAddresses)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("customer_shipping_address_ibfk_2");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerShippingAddresses)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("customer_shipping_address_ibfk_1");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.CustomerId, "order_ibfk_1");

            entity.HasIndex(e => e.UpdatedBy, "order_ibfk_3");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(20, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Discount)
                .HasPrecision(20, 2)
                .HasColumnName("discount");
            entity.Property(e => e.FinalAmount)
                .HasPrecision(20, 2)
                .HasColumnName("final_amount");
            entity.Property(e => e.Status)
                .HasColumnType("enum('pending','processing','dispatched','returned')")
                .HasColumnName("status");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("order_ibfk_1");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Orders)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("order_ibfk_3");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_item");

            entity.HasIndex(e => new { e.OrderId, e.ProductId }, "order_id").IsUnique();

            entity.HasIndex(e => e.ProductId, "order_item_ibfk_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(20, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Discount)
                .HasPrecision(20, 2)
                .HasColumnName("discount");
            entity.Property(e => e.FinalAmount)
                .HasPrecision(20, 2)
                .HasColumnName("final_amount");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasPrecision(20, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_item_ibfk_1");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("order_item_ibfk_2");
        });

        modelBuilder.Entity<OrderPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_payment");

            entity.HasIndex(e => e.OrderId, "order_id1").IsUnique();

            entity.HasIndex(e => e.PaymentMethodId, "order_payment_ibfk_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(20, 2)
                .HasColumnName("amount");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.Status)
                .HasColumnType("enum('pending','paid','failed')")
                .HasColumnName("status");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Order).WithOne(p => p.OrderPayment)
                .HasForeignKey<OrderPayment>(d => d.OrderId)
                .HasConstraintName("order_payment_ibfk_1");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.OrderPayments)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("order_payment_ibfk_2");
        });

        modelBuilder.Entity<OrderShipping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_shipping");

            entity.HasIndex(e => e.OrderId, "order_id2").IsUnique();

            entity.HasIndex(e => e.ShippingMethodId, "order_shipping_ibfk_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualDeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("actual_delivery_date");
            entity.Property(e => e.Destination)
                .HasMaxLength(255)
                .HasColumnName("destination");
            entity.Property(e => e.EstimatedDeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("estimated_delivery_date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ShippingMethodId).HasColumnName("shipping_method_id");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(255)
                .HasColumnName("tracking_number");

            entity.HasOne(d => d.Order).WithOne(p => p.OrderShipping)
                .HasForeignKey<OrderShipping>(d => d.OrderId)
                .HasConstraintName("order_shipping_ibfk_1");

            entity.HasOne(d => d.ShippingMethod).WithMany(p => p.OrderShippings)
                .HasForeignKey(d => d.ShippingMethodId)
                .HasConstraintName("order_shipping_ibfk_2");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payment_method");

            entity.HasIndex(e => e.Name, "name5").IsUnique();

            entity.HasIndex(e => e.UpdatedBy, "payment_method_ibfk_1");

            entity.HasIndex(e => e.IsActive, "payment_method_idx_1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IconUrl)
                .HasMaxLength(255)
                .HasColumnName("icon_url");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PaymentMethods)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("payment_method_ibfk_1");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("permission");

            entity.HasIndex(e => e.Name, "name6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.Code, "code1").IsUnique();

            entity.HasIndex(e => e.IsActive, "idx_is_active");

            entity.HasIndex(e => e.UpdatedBy, "product_ibfk_1");

            entity.HasIndex(e => e.SupplierId, "product_ibfk_2");

            entity.HasIndex(e => e.BrandId, "product_ibfk_3");

            entity.HasIndex(e => e.Year, "product_ibfk_4");

            entity.HasIndex(e => e.SeasonId, "product_ibfk_5");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Cost)
                .HasPrecision(20, 2)
                .HasColumnName("cost");
            entity.Property(e => e.Discount)
                .HasPrecision(5, 2)
                .HasColumnName("discount");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("note");
            entity.Property(e => e.Price)
                .HasPrecision(20, 2)
                .HasColumnName("price");
            entity.Property(e => e.SeasonId).HasColumnName("season_id");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("product_ibfk_3");

            entity.HasOne(d => d.Season).WithMany(p => p.Products)
                .HasForeignKey(d => d.SeasonId)
                .HasConstraintName("product_ibfk_5");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("product_ibfk_2");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Products)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_ibfk_1");
        });

        modelBuilder.Entity<ProductAttribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_attribute");

            entity.HasIndex(e => e.UpdatedBy, "product_attribute_ibfk_1");

            entity.HasIndex(e => new { e.ProductId, e.Attribute }, "unique_product_attribute").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Attribute).HasColumnName("attribute");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_attribute_fk");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ProductAttributes)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_attribute_ibfk_1");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_category");

            entity.HasIndex(e => e.UpdatedBy, "product_category_ibfk_1");

            entity.HasIndex(e => e.CategoryId, "product_category_ibfk_3");

            entity.HasIndex(e => new { e.ProductId, e.CategoryId }, "product_id1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("product_category_ibfk_3");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_category_ibfk_2");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ProductCategories)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_category_ibfk_1");
        });

        modelBuilder.Entity<ProductInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_info");

            entity.HasIndex(e => e.ProductId, "product_id2").IsUnique();

            entity.HasIndex(e => e.UpdatedBy, "product_info_ibfk_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LongDescription)
                .HasColumnType("text")
                .HasColumnName("long_description");
            entity.Property(e => e.MaxOrder).HasColumnName("max_order");
            entity.Property(e => e.MinOrder).HasColumnName("min_order");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ShippingWeight)
                .HasPrecision(6, 3)
                .HasColumnName("shipping_weight");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(255)
                .HasColumnName("short_description");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Weight)
                .HasPrecision(6, 3)
                .HasColumnName("weight");

            entity.HasOne(d => d.Product).WithOne(p => p.ProductInfo)
                .HasForeignKey<ProductInfo>(d => d.ProductId)
                .HasConstraintName("product_info_ibfk_1");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ProductInfos)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_info_ibfk_2");
        });

        modelBuilder.Entity<ProductMedium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_media");

            entity.HasIndex(e => e.UpdatedBy, "product_media_ibfk_1");

            entity.HasIndex(e => e.ProductId, "product_media_ibfk_2");

            entity.HasIndex(e => e.Url, "url").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AltText)
                .HasMaxLength(255)
                .HasColumnName("alt_text");
            entity.Property(e => e.MediaType)
                .HasColumnType("enum('Image','Video','Unknown')")
                .HasColumnName("media_type");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Url).HasColumnName("url");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductMedia)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_media_ibfk_2");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ProductMedia)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_media_ibfk_1");
        });

        modelBuilder.Entity<ProductQuantity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_quantity");

            entity.HasIndex(e => e.ProductId, "product_quantity_ibfk_1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValueSql("'0'")
                .HasColumnName("quantity");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductQuantities)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_quantity_ibfk_1");
        });

        modelBuilder.Entity<ProductQuantityAttrbiute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_quantity_attrbiute");

            entity.HasIndex(e => new { e.ProductQuantityId, e.Attribute }, "product_quantity_id").IsUnique();

            entity.HasIndex(e => e.ProductQuantityId, "product_quantity_id_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Attribute).HasColumnName("attribute");
            entity.Property(e => e.AttributeOption)
                .HasMaxLength(255)
                .HasColumnName("attribute_option");
            entity.Property(e => e.ProductQuantityId).HasColumnName("product_quantity_id");

            entity.HasOne(d => d.ProductQuantity).WithMany(p => p.ProductQuantityAttrbiutes)
                .HasForeignKey(d => d.ProductQuantityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_quantity_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "name7").IsUnique();

            entity.HasIndex(e => e.UpdatedBy, "role_ibfk_1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Roles)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("role_ibfk_1");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role_permission");

            entity.HasIndex(e => e.UpdatedBy, "role_permission_ibfk_3");

            entity.HasIndex(e => e.PermissionId, "role_permissions_ibfk_2");

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }, "unique_role_permission").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("role_permission_ibfk_2");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("role_permission_ibfk_1");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.RolePermissions)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("role_permission_ibfk_3");
        });

        modelBuilder.Entity<Season>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("season");

            entity.HasIndex(e => e.Name, "name8").IsUnique();

            entity.HasIndex(e => e.UpdatedBy, "season_ibfk_1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Seasons)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("season_ibfk_1");
        });

        modelBuilder.Entity<ShippingMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("shipping_method");

            entity.HasIndex(e => e.Name, "name10").IsUnique();

            entity.HasIndex(e => e.UpdatedBy, "shipping_method_ibfk_1");

            entity.HasIndex(e => e.IsActive, "shipping_method_idx_1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IconUrl)
                .HasMaxLength(255)
                .HasColumnName("icon_url");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Overseas).HasColumnName("overseas");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ShippingMethods)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("shipping_method_ibfk_1");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("supplier");

            entity.HasIndex(e => e.Name, "name11").IsUnique();

            entity.HasIndex(e => e.UpdatedBy, "supplier_ibfk_1");

            entity.HasIndex(e => e.CountryId, "supplier_ibfk_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .HasColumnName("city");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .HasColumnName("website");

            entity.HasOne(d => d.Country).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("supplier_ibfk_2");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Suppliers)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("supplier_ibfk_1");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("transaction");

            entity.HasIndex(e => e.UpdatedBy, "inventory_ibfk_1");

            entity.HasIndex(e => e.ProductId, "inventory_ibfk_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("note");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TransactionType)
                .HasColumnType("enum('Sale','Return','Restock')")
                .HasColumnName("transaction_type");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Product).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("transaction_ibfk_2");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Transactions)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("transaction_ibfk_1");
        });

        modelBuilder.Entity<TransactionAttribute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("transaction_attribute");

            entity.HasIndex(e => new { e.TransactionId, e.Attribute }, "transaction_attribute_ibfk_2").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Attribute).HasColumnName("attribute");
            entity.Property(e => e.AttributeOption)
                .HasMaxLength(255)
                .HasColumnName("attribute_option");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");

            entity.HasOne(d => d.Transaction).WithMany(p => p.TransactionAttributes)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("transaction_attribute_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Username, "AK_user_username").IsUnique();

            entity.HasIndex(e => e.IsActive, "idx_is_active1");

            entity.HasIndex(e => e.UpdatedBy, "user_ibfk_1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FailedLoginAttempts).HasColumnName("failed_login_attempts");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("user_ibfk_1");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user_role");

            entity.HasIndex(e => new { e.UserId, e.RoleId }, "unique_user_role").IsUnique();

            entity.HasIndex(e => e.UpdatedBy, "user_role_ibfk_3");

            entity.HasIndex(e => e.RoleId, "user_roles_ibfk_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("user_role_ibfk_2");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.UserRoleUpdatedByNavigations)
                .HasPrincipalKey(p => p.Username)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("user_role_ibfk_3");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoleUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_role_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
