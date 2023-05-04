using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? MemberId { get; set; }

    public int? StoreId { get; set; }

    public DateTime? ReservationTime { get; set; }

    public string? ReservationNumber { get; set; }

    public int? PaymentMethodId { get; set; }

    public int? EatTypeMethodId { get; set; }

    public int? CouponId { get; set; }

    public decimal? Amount { get; set; }

    public int? OrderStatus { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual EatTypeMethod? EatTypeMethod { get; set; }

    public virtual Member? Member { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Status? OrderStatusNavigation { get; set; }

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual Store? Store { get; set; }

    public virtual ICollection<StoreReserved> StoreReserveds { get; set; } = new List<StoreReserved>();
}
