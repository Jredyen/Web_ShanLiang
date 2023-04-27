using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class MemberAction
{
    public int No { get; set; }

    public int? ActionId { get; set; }

    public int? MemberId { get; set; }

    public int? StoreId { get; set; }

    public string? MemberNotes { get; set; }

    public virtual Action? Action { get; set; }

    public virtual Member? Member { get; set; }

    public virtual Store? Store { get; set; }
}
