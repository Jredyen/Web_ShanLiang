using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class StoreDecorationImage
{
    public int ImageId { get; set; }

    public int? StoreId { get; set; }

    public string? ImagePath { get; set; }

    public string? ImageJudge { get; set; }
    public virtual Store? Store { get; set; }
}
