using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class Section
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<SectionCategory> SectionCategoryCategories { get; set; } = new List<SectionCategory>();

    public virtual ICollection<SectionCategory> SectionCategorySections { get; set; } = new List<SectionCategory>();

    public virtual User? UpdatedByNavigation { get; set; }
}
