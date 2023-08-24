using System;
using System.Collections.Generic;

namespace UserService.Domain.Entities;

public partial class User
{
    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public virtual Role RoleNameNavigation { get; set; } = null!;
}
