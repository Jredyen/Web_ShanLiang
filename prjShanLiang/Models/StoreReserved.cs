using System;
using System.Collections.Generic;

namespace prjShanLiang.Models;

public partial class StoreReserved
{
    public int ReservationNo { get; set; }

    public int? StoreId { get; set; }

    public int? OrderId { get; set; }

    public DateTime? Date { get; set; }

    public int? Time { get; set; }

    public int? NumReserved { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Store? Store { get; set; }
}
