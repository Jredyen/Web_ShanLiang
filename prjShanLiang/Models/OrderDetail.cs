using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int MealsId { get; set; }

    public string? MealsName { get; set; }

    public decimal? MealsPrice { get; set; }

    public virtual Order Order { get; set; } = null!;
}
