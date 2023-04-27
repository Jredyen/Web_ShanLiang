using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Identification
{
    public int IdentificationId { get; set; }

    public string? IdentificationName { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
