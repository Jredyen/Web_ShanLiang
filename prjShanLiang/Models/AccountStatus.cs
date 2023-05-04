using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class AccountStatus
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
