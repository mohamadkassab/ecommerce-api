using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string? BillingAddress { get; set; }

    public int? TotalOrders { get; set; }

    public decimal? TotalSpent { get; set; }

    public int? PreferredPaymentMethodId { get; set; }

    public int? PreferredCurrencyId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<CustomerShippingAddress> CustomerShippingAddresses { get; set; } = new List<CustomerShippingAddress>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Currency? PreferredCurrency { get; set; }

    public virtual PaymentMethod? PreferredPaymentMethod { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }
}
