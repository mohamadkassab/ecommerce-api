using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class ProductMedium
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string MediaType { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string AltText { get; set; } = null!;

    public bool? IsPrimary { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
