using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class PaymentMethod
{
    public int PaymentMethodId { get; set; }

    public string? PaymentName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
