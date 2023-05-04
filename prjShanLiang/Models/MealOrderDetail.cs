using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class MealOrderDetail
{
    public int Key { get; set; }

    public int? OrderId { get; set; }

    public int? MealId { get; set; }

    public int? Quantity { get; set; }

    public virtual MealMenu? Meal { get; set; }

    public virtual MealOrder? Order { get; set; }
}
