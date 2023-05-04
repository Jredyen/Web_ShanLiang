using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string? StatusName { get; set; }

    public virtual ICollection<MealOrder> MealOrders { get; set; } = new List<MealOrder>();

    public virtual ICollection<MemberCoupon> MemberCoupons { get; set; } = new List<MemberCoupon>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
