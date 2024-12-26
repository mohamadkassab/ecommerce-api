using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class ProductInfo
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ShortDescription { get; set; } = null!;

    public string LongDescription { get; set; } = null!;

    public decimal Weight { get; set; }

    public decimal ShippingWeight { get; set; }

    public short MinOrder { get; set; }

    public short MaxOrder { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
