using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Cost { get; set; }

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public string? Note { get; set; }

    public int SupplierId { get; set; }

    public int BrandId { get; set; }

    public int Year { get; set; }

    public int SeasonId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    public virtual ProductInfo? ProductInfo { get; set; }

    public virtual ICollection<ProductMedium> ProductMedia { get; set; } = new List<ProductMedium>();

    public virtual ICollection<ProductQuantity> ProductQuantities { get; set; } = new List<ProductQuantity>();

    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

    public virtual Season Season { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User? UpdatedByNavigation { get; set; }
}
