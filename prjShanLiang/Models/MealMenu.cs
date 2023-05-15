using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class MealMenu
{
    public int MealId { get; set; }

    public int? StoreId { get; set; }

    public string? MealName { get; set; }

    public int? MealPrice { get; set; }

    public string? MealImagePath { get; set; }

    public string? Recommendation { get; set; }

    public virtual ICollection<MealOrderDetail> MealOrderDetails { get; set; } = new List<MealOrderDetail>();

    public virtual Store? Store { get; set; }
}
