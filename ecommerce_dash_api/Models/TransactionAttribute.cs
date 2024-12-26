using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class TransactionAttribute
{
    public int Id { get; set; }

    public int TanscationId { get; set; }

    public string Attribute { get; set; } = null!;

    public string AttributeOption { get; set; } = null!;

    public virtual Transaction Tanscation { get; set; } = null!;
}
