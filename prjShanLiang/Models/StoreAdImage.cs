using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class StoreAdImage
{
    public int ImageNo { get; set; }

    public int? StoreId { get; set; }

    public string? Adimage { get; set; }

    public virtual Store? Store { get; set; }
}
