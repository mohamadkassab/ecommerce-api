using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class ProductQuantityAttrbiute
{
    public int Id { get; set; }

    public int ProductQuantityId { get; set; }

    public string Attribute { get; set; } = null!;

    public string AttributeOption { get; set; } = null!;

    public virtual ProductQuantity ProductQuantity { get; set; } = null!;
}
