using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Chart
{
    public int Id { get; set; }

    public string Label { get; set; } = null!;

    public string? Query { get; set; }

    public string Type { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<ChartProperty> ChartProperties { get; set; } = new List<ChartProperty>();

    public virtual User? UpdatedByNavigation { get; set; }
}
