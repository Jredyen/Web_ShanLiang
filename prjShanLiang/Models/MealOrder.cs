using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class MealOrder
{
    public int OrderId { get; set; }

    public int? MemberId { get; set; }

    public int? StoreId { get; set; }

    public int? Total { get; set; }

    public int? OrderStatus { get; set; }

    public string? Remark { get; set; }

    public string? OrderDate { get; set; }
  
    public virtual ICollection<MealOrderDetail> MealOrderDetails { get; set; } = new List<MealOrderDetail>();

    public virtual Member? Member { get; set; }

    public virtual Status? OrderStatusNavigation { get; set; }

    public virtual Store? Store { get; set; }
}
