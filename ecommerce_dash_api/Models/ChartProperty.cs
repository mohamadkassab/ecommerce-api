using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class ChartProperty
{
    public int Id { get; set; }

    public int? ChartId { get; set; }

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Chart? Chart { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }
}
