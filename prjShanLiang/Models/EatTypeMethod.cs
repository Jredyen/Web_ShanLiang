using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class EatTypeMethod
{
    public int EatTypeMethodId { get; set; }

    public string? EatTypeMethodName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
