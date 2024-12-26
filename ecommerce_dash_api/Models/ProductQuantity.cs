using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class ProductQuantity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public string Attribute { get; set; } = null!;

    public string AttributeOption { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
