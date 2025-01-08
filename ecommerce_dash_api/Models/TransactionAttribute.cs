using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class TransactionAttribute
{
    public int Id { get; set; }

    public int TransactionId { get; set; }

    public string Attribute { get; set; } = null!;

    public string AttributeOption { get; set; } = null!;

    public virtual Transaction Transaction { get; set; } = null!;
}
