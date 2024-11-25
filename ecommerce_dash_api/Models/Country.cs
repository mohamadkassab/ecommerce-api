using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

    public virtual ICollection<Currency> Currencies { get; set; } = new List<Currency>();

    public virtual ICollection<CustomerShippingAddress> CustomerShippingAddresses { get; set; } = new List<CustomerShippingAddress>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();

    public virtual User? UpdatedByNavigation { get; set; }
}
