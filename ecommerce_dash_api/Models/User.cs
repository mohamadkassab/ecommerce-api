using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Phone { get; set; } = null!;

    public string? Address { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public int? FailedLoginAttempts { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<AttributeOption> AttributeOptions { get; set; } = new List<AttributeOption>();

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<ChartProperty> ChartProperties { get; set; } = new List<ChartProperty>();

    public virtual ICollection<Chart> Charts { get; set; } = new List<Chart>();

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();

    public virtual ICollection<Currency> Currencies { get; set; } = new List<Currency>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public virtual ICollection<ProductAttributeOption> ProductAttributeOptions { get; set; } = new List<ProductAttributeOption>();

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    public virtual ICollection<ProductMedium> ProductMedia { get; set; } = new List<ProductMedium>();

    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<Season> Seasons { get; set; } = new List<Season>();

    public virtual ICollection<SectionCategory> SectionCategories { get; set; } = new List<SectionCategory>();

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

    public virtual ICollection<ShippingMethod> ShippingMethods { get; set; } = new List<ShippingMethod>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<UserRole> UserRoleUpdatedByNavigations { get; set; } = new List<UserRole>();

    public virtual ICollection<UserRole> UserRoleUsers { get; set; } = new List<UserRole>();

    public virtual ICollection<Year> Years { get; set; } = new List<Year>();
}
