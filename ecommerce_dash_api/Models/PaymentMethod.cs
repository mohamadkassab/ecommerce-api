using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class PaymentMethod
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string IconUrl { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<OrderPayment> OrderPayments { get; set; } = new List<OrderPayment>();

    public virtual User? UpdatedByNavigation { get; set; }
}
