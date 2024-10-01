using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Chart
{
    public int Id { get; set; }

    public string Label { get; set; } = null!;

    public string? Query { get; set; }

    public string ChartType { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ChartProperty> ChartProperties { get; set; } = new List<ChartProperty>();
}
