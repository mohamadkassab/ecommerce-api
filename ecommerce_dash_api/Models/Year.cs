using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Year
{
    public int Id { get; set; }

    public int Name { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User? UpdatedByNavigation { get; set; }
}
