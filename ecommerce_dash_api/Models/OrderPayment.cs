using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class OrderPayment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int PaymentMethodId { get; set; }

    public decimal? PaymentAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateTime? PaymentDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
}
