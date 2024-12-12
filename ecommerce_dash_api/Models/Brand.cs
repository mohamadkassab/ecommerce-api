using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Website { get; set; }

    public string LogoUrl { get; set; } = null!;

    public int CountryId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User? UpdatedByNavigation { get; set; }
}
