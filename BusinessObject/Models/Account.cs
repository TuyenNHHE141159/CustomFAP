using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Account
{
    public string AccountId { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public virtual Student Student { get; set; }

    public virtual Teacher Teacher { get; set; }

    public virtual ICollection<Role> Roles { get; } = new List<Role>();
}
