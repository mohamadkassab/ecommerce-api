using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class ProductQuantity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;
}
