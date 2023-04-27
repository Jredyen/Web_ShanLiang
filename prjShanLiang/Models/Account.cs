using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string AccountName { get; set; } = null!;

    public string? AccountPassword { get; set; }

    public int? Identification { get; set; }

    public virtual Identification? IdentificationNavigation { get; set; }
}
