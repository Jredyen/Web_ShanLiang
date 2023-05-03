using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string? AccountName { get; set; }

    public string? Memberphone { get; set; }

    public string? MemberName { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? BrithDate { get; set; }

    public string? Address { get; set; }

    public int? CustomerLevel { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<MemberAction> MemberActions { get; set; } = new List<MemberAction>();

    public virtual ICollection<MemberCoupon> MemberCoupons { get; set; } = new List<MemberCoupon>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StoreEvaluate> StoreEvaluates { get; set; } = new List<StoreEvaluate>();
}
