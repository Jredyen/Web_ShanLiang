using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace prjShanLiang.Models;

public partial class Account
{
    public int AccountId { get; set; }
    [DisplayName("帳號")]
    public string AccountName { get; set; } = null!;
    [DisplayName("密碼")]
    public string? AccountPassword { get; set; }

    public int? Identification { get; set; }

    public virtual Identification? IdentificationNavigation { get; set; }
}
