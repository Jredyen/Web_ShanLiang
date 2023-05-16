using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Coupon
{
    public int CouponId { get; set; }

    public string? CouponContent { get; set; }

    public byte[]? CouponImage { get; set; }

    public int? CustomerLevel { get; set; }

    public DateTime? CouponDeadline { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<MemberCoupon> MemberCoupons { get; set; } = new List<MemberCoupon>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StoreReserved> StoreReserveds { get; set; } = new List<StoreReserved>();
}
