using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class CustomerShippingAddress
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int CountryId { get; set; }

    public string AddressLine { get; set; } = null!;

    public string? City { get; set; }

    public string? State { get; set; }

    public short? PostalCode { get; set; }

    public bool IsPrimary { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
