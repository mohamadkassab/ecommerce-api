using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class ProductAttributeOption
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? AttributeOptionId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual AttributeOption? AttributeOption { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }
}
