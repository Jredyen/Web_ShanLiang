using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class StoreMealImage
{
    public int ImageId { get; set; }

    public int? StoreId { get; set; }

    public string? ImagePath { get; set; }
}
