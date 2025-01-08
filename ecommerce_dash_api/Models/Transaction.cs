using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ProductAttributeKey { get; set; } = null!;

    public int Quantity { get; set; }

    public string TransactionType { get; set; } = null!;

    public string? Note { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<TransactionAttribute> TransactionAttributes { get; set; } = new List<TransactionAttribute>();

    public virtual User? UpdatedByNavigation { get; set; }
}
