using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
