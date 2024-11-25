using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Order
{
    public int Id { get; set; }

    public string OrderCode { get; set; } = null!;

    public int? CustomerId { get; set; }

    public int? ShippingAddressId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public decimal? TotalAmount { get; set; }

    public decimal? Discount { get; set; }

    public decimal? FinalAmount { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual OrderPayment? OrderPayment { get; set; }

    public virtual OrderShipping? OrderShipping { get; set; }

    public virtual CustomerShippingAddress? ShippingAddress { get; set; }
}
