using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Currency
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public decimal ExchangeRateUsd { get; set; }

    public int? CountryId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual User? UpdatedByNavigation { get; set; }
}
