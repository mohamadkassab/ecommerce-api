using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Attribute
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<AttributeOption> AttributeOptions { get; set; } = new List<AttributeOption>();

    public virtual User? UpdatedByNavigation { get; set; }
}
