using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class MemberCoupon
{
    public int No { get; set; }

    public int? MemberId { get; set; }

    public int? CouponId { get; set; }

    public int? CouponStatus { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual Status? CouponStatusNavigation { get; set; }

    public virtual Member? Member { get; set; }
}
