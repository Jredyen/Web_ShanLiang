using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public string? AdminName { get; set; }

    public string? Passwoed { get; set; }

    public int? IdentificationId { get; set; }

    public virtual Identification? Identification { get; set; }
}
