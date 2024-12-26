using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class AttributeOption
{
    public int Id { get; set; }

    public int AttributeId { get; set; }

    public string Option { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
