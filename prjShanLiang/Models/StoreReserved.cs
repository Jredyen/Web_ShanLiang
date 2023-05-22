using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class StoreReserved
{
    public int ReservationId { get; set; }

    public int? StoreId { get; set; }

    public DateTime? Date { get; set; }

    public int? Time { get; set; }

    public int? MemberId { get; set; }

    public int? NumOfPeople { get; set; }

    public int? CouponId { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual Member? Member { get; set; }

    public virtual Store? Store { get; set; }
}
