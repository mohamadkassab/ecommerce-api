using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class OrderShipping
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ShippingMethodId { get; set; }

    public string? TrackingNumber { get; set; }

    public DateTime? EstimatedDeliveryDate { get; set; }

    public DateTime? ActualDeliveryDate { get; set; }

    public string Destination { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ShippingMethod ShippingMethod { get; set; } = null!;
}
