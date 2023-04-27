using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Action
{
    public int ActionId { get; set; }

    public string? ActionName { get; set; }

    public virtual ICollection<MemberAction> MemberActions { get; set; } = new List<MemberAction>();
}
