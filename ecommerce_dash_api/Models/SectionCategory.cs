using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class SectionCategory
{
    public int Id { get; set; }

    public int? SectionId { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Section? Category { get; set; }

    public virtual Section? Section { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }
}
