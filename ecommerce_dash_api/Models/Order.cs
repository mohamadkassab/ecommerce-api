using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string Status { get; set; } = null!;

    public decimal Amount { get; set; }

    public decimal Discount { get; set; }

    public decimal FinalAmount { get; set; }

    public DateTime Timestamp { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderPayment? OrderPayment { get; set; }

    public virtual OrderShipping? OrderShipping { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }
}
