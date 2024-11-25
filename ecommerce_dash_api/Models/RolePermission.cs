using System;
using System.Collections.Generic;

namespace ecommerce_dash_api.Models;

public partial class RolePermission
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }
}
