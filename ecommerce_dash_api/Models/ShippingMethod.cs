using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class ShippingMethod
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string IconUrl { get; set; } = null!;

    public bool Overseas { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<OrderShipping> OrderShippings { get; set; } = new List<OrderShipping>();

    public virtual User? UpdatedByNavigation { get; set; }
}
