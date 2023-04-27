using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class StoreImage
{
    public int ImageNo { get; set; }

    public int? StoreId { get; set; }

    public byte[]? StoreImage1 { get; set; }

    public virtual Store? Store { get; set; }
}
