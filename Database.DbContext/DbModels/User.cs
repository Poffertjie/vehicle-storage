using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class User
{
    public string Id { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool EmailConfirmed { get; set; }

    public string? ResetPasswordToken { get; set; }

    public string? PasswordHash { get; set; }

    public bool AccountLocked { get; set; }

    public string SecurityStamp { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public bool SystemAdmin { get; set; }

    public int? CompanyId { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
