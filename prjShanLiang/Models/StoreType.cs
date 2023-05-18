using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class StoreType
{
    public int No { get; set; }

    public int? RestaurantTypeNum { get; set; }

    public int? StoreId { get; set; }

    public virtual RestaurantType? RestaurantTypeNumNavigation { get; set; }

    public virtual Store? Store { get; set; }
}
