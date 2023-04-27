using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class RestaurantType
{
    public int RestaurantTypeNum { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<StoreType> StoreTypes { get; set; } = new List<StoreType>();
}
