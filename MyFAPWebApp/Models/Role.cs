using System;
using System.Collections.Generic;

namespace MyFAPWebApp.Models;

public partial class Role
{
    public string RoleId { get; set; }

    public string RoleName { get; set; }

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
