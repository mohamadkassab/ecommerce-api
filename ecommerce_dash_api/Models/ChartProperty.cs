using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class ChartProperty
{
    public int Id { get; set; }

    public int? ChartId { get; set; }

    public string PropertyName { get; set; } = null!;

    public string PropertyValue { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Chart? Chart { get; set; }
}
